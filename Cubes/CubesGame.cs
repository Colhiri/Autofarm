using System.Text.Json;

using Autofarm.Сommon;
using Autofarm.Сommon.DataBase;
using Autofarm.Cubes.Responses;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace Autofarm.Cubes
{
    /*
    ### Запуск майнера возможен без какого-то ни было запуска самого окна
    ### нужен только токен самого пользователя в игре
    */
    public class CubesGame : IGame
    {
        private static readonly HttpClient client = new HttpClient();
        private List<BaseHeader> headers;
        private List<BaseToken> tokens;
        private List<BaseUrl> urls;
        private GameContext gameDB;
        private string nameGame;
        public List<CheckTokenTasks> checkTokenTasksComplete { get; set; }
        public CheckTokenTasks currentCheck { get; set; } = null;

        public CubesGame(string nameGame, GameContext gameDB)
        {
            this.nameGame = nameGame;
            this.gameDB = gameDB;
            headers = new List<BaseHeader>();
            tokens = new List<BaseToken>();
            checkTokenTasksComplete = new List<CheckTokenTasks>();

            LoadRecources();
        }

        public async Task PlayGame(int waitMilliSeconds)
        {
            await Task.Delay(waitMilliSeconds);
            // Console.WriteLine("PocketFI 10 sec");
        }

        public void GetCurrentToken(BaseToken token)
        {
            // Добавляем в проверку, если ключа нет
            if (!checkTokenTasksComplete.Select(tok => tok.ID_TOKEN).Contains(token.Data))
            {
                currentCheck = new CheckTokenTasks(token.Data);
                checkTokenTasksComplete.Add(currentCheck);
            }
            else
            {
                currentCheck = checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First();
            }
        }

        public async Task<int> GetInfo(string url, BaseToken token, BaseHeader header)
        {
            // Создаем необходимый header
            client.DefaultRequestHeaders.Clear();
            // Сколько нужно секунд для повторного запроса
            int waitMilliSeconds = new Random().Next(900, 1100);
            // Ошибки при работе сериализатора
            HeaderGame currentHeader = JsonSerializer.Deserialize<HeaderGame>(header.Data);
            if (currentHeader == null)
            {
                throw new Exception("Не назначены заголовки!");
            }
            foreach (KeyValuePair<string, string> head in currentHeader)
            {
                client.DefaultRequestHeaders.Add(head.Key, head.Value);
            }
            // Создаем необходимый токен
            var values = new Dictionary<string, string>
            {
                { "token", token.Data.ToString() }
            };
            JsonContent content = JsonContent.Create(values);
            // Отсылаем запрос
            HttpResponseMessage response = null;
            // Отсылаем запрос
            try
            {
                response = await client.PostAsync(url, content);
            }
            catch (TaskCanceledException ex)
            {
                Logger.GetInstance().Log(LogLevel.Critical, nameGame, $"Timeout 100 seconds -- {ex.Task}");
                waitMilliSeconds = new Random().Next(900, 1100);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log(LogLevel.Critical, nameGame, $"Неуправляемая ошибка -- {ex.GetType()}");
                waitMilliSeconds = new Random().Next(30000, 60000);
            }

            // Проверка ответа
            if (response == null)
            {
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"response us null!");
                return waitMilliSeconds;
            }
            // Проверка ответа
            if (response.IsSuccessStatusCode)
            {
                PlayGameResponse? data = await response.Content.ReadFromJsonAsync<PlayGameResponse>();

                if (data != null && data.mystery_ids.Length == 0)
                {
                    waitMilliSeconds = new Random().Next(400, 700);
                }
                if (data != null)
                {
                    Logger.GetInstance().Log(LogLevel.Information, nameGame, @$"user:{token.Data} energy:{data.energy} cubes_mined:{data.boxes_amount}");
                }
                else
                {
                    Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} Data response is null!");
                    waitMilliSeconds = new Random().Next(600, 900);
                }
            }
            else
            {
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} statusCode:{response.StatusCode}");
                waitMilliSeconds = new Random().Next(2000, 3200);
            }
            return waitMilliSeconds;
        }

        public BaseToken RefreshToken(string url, BaseHeader header)
        {
            throw new NotImplementedException();
        }

        public BaseToken GetToken()
        {
            throw new NotImplementedException();
        }

        public void LoadRecources()
        {
            // Грузим все токены
            tokens = gameDB.tokens.Where(token => token.BaseGameInfo.NameGame == nameGame).ToList();
            // Грузим все действия (URL и идентификатор заголовка)
            urls = gameDB.urls.Where(url => url.BaseGameInfo.NameGame == nameGame).ToList();
            // Грузим заголовки
            headers = urls.Select(header => header.BaseHeader).ToList();
        }

        public async Task<List<TaskWithTimers>> MainLoop()
        {
            string URL = "";

            List<TaskWithTimers> loopTasks = new List<TaskWithTimers>();

            for (int accountIndex = 0; accountIndex < tokens.Count; accountIndex++)
            {
                URL = @"https://server.questioncube.xyz/game/mined";
                BaseToken token = tokens[accountIndex];
                BaseUrl url = gameDB.urls.Where(url => url.URL == URL).First();
                BaseHeader header = url.BaseHeader;

                GetCurrentToken(token);

                if (currentCheck.TIME_END <= DateTime.Now)
                {
                    int resultWait = await GetInfo(URL, token, header);
                    currentCheck.UpdateTimeStamps(resultWait);
                    Task task = PlayGame(resultWait);
                    loopTasks.Add(new TaskWithTimers(task, currentCheck.TIME_END));
                }
            }

            return loopTasks;
        }
    }
}

using System.Text.Json;

using Autofarm.Сommon;
using Autofarm.Сommon.DataBase;
using Autofarm.Cubes.Responses;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Net;

namespace Autofarm.TimeTon
{
    /*
    ### Запуск майнера возможен без какого-то ни было запуска самого окна
    ### нужен только токен самого пользователя в игре
    */
    public class CFGame : IGame
    {
        private static readonly HttpClient client = new HttpClient();
        private List<BaseHeader> headers;
        private List<BaseToken> tokens;
        private List<BaseUrl> urls;
        private GameContext gameDB;
        private string nameGame;
        public List<CheckTokenTasks> checkTokenTasksComplete { get; set; }
        public CheckTokenTasks currentCheck { get; set; } = null;

        public CFGame(string nameGame, GameContext gameDB)
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
            int waitMilliSeconds = new Random().Next(350000, 400000);
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

            // Особенность CF
            // Особенность CF
            // Особенность CF
            // Особенность CF
            client.DefaultRequestHeaders.Add("Authorization", token.Data);

            // Отсылаем запрос
            HttpResponseMessage response = null;
            // Отсылаем запрос
            try
            {
                response = await client.GetAsync(url);
            }
            catch (TaskCanceledException ex)
            {
                Logger.GetInstance().Log(LogLevel.Critical, nameGame, $"Timeout 100 seconds -- {ex.Task}");
                waitMilliSeconds = new Random().Next(55000, 70000);
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
                PlayGameResponse? data = null;

                try
                {
                    data = await response.Content.ReadFromJsonAsync<PlayGameResponse>();

                }
                catch
                {
                    Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} Ошибка в десериализации респонса");
                    waitMilliSeconds = new Random().Next(350000, 400000);
                    return waitMilliSeconds;
                }

                if (data != null)
                {
                    Logger.GetInstance().Log(LogLevel.Information, nameGame, @$"user:{token.Data} energy:{data.energy} cubes_mined:{data.mined_count}");
                }
                else
                {
                    Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} Data response is null!");
                    waitMilliSeconds = new Random().Next(55000, 70000);
                }
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
                URL = @"https://api.cyberfin.xyz/api/v1/mining/claim";
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

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

        public CubesGame(string nameGame, GameContext gameDB)
        {
            this.nameGame = nameGame;
            this.gameDB = gameDB;
            headers = new List<BaseHeader>();
            tokens = new List<BaseToken>();
            checkTokenTasksComplete = new List<CheckTokenTasks>();

            LoadRecources();
        }

        public async Task PlayGame(string url, BaseToken token, BaseHeader header)
        {
            // Добавляем в проверку
            // Добавляем в проверку
            // Добавляем в проверку
            checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First().NO_LOCK_TOKEN = false;

            // Создаем необходимый header
            client.DefaultRequestHeaders.Clear();
            // Сколько нужно секунд для повторного запроса
            int waitSeconds = new Random().Next(900, 1100);
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
                waitSeconds = new Random().Next(900, 1100);
            }

            // Проверка ответа
            if (response == null)
            {
                await Task.Delay(waitSeconds);
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"response us null!");
            }
            // Проверка ответа
            if (response.IsSuccessStatusCode)
            {
                PlayGameResponse? data = await response.Content.ReadFromJsonAsync<PlayGameResponse>();
            
                if (data != null && data.mystery_ids.Length == 0)
                {
                    waitSeconds = new Random().Next(400, 700);
                }
                if (data != null)
                {
                    Logger.GetInstance().Log(LogLevel.Information, nameGame, @$"user:{token.Data} energy:{data.energy} cubes_mined:{data.mined_count}");
                }
                else
                {
                    Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} Data response is null!");
                    waitSeconds = new Random().Next(600, 900);
                }
            }
            else
            {
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} statusCode:{response.StatusCode} phrase:{response.ReasonPhrase}");
                waitSeconds = new Random().Next(900, 1100);
            }

            checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First().COUNT_TASKS++;

            await Task.Delay(waitSeconds);
            // Console.WriteLine("Cubes 1 sec");

            // Удаляем из проверки
            // Удаляем из проверки
            // Удаляем из проверки
            checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First().NO_LOCK_TOKEN = true;
        }

        public async Task GetInfo(string url, BaseToken token, BaseHeader header)
        {
            throw new NotImplementedException();
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
            // Грузим заголовки
            headers = gameDB.headers.ToList();

            // Грузим все токены
            tokens = gameDB.tokens.Where(token => token.BaseGameInfo.NameGame == nameGame).ToList();

            // Грузим все действия (URL и идентификатор заголовка)
            urls = gameDB.urls.ToList();
        }

        public void MainLoop(List<Task> MainTasks)
        {
            string URL = "";

            for (int accountIndex = 0; accountIndex < tokens.Count; accountIndex++)
            {
                // Майнинг кубов
                URL = @"https://server.questioncube.xyz/game/mined";
                BaseToken token = tokens[accountIndex];

                // Добавляем в проверку, если ключа нет
                if (!checkTokenTasksComplete.Select(tok => tok.ID_TOKEN).Contains(token.Data))
                {
                    checkTokenTasksComplete.Add(new CheckTokenTasks(token.Data, 0, true));
                }

                if (checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First().NO_LOCK_TOKEN)
                {
                    BaseUrl url = gameDB.urls.Where(url => url.URL == URL).First();
                    BaseHeader header = url.BaseHeader;
                    Task task = PlayGame(URL, token, header);
                    MainTasks.Add(task);
                }
            }
        }
    }
}

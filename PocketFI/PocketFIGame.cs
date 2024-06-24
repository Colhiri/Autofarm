using System.Net.Http.Json;
using System.Text.Json;
using Autofarm.PocketFI.Responses;
using Autofarm.Сommon;
using Autofarm.Сommon.DataBase;

namespace Autofarm.PocketFI
{
    /*
    ### Запуск майнера возможен без какого-то ни было запуска самого окна
    ### нужен только токен самого пользователя в игре
    ### В pocketFI в каждый запрос передаются метаданные о пользователе телеграмма, которые гораздо более приоритетные
    ### чем передаваемый ID USER
    ### В качестве токена в POCKET FI выступают заголовки с передаваемыми данными
    ### Смысл в том, что при заходе в pocketFI формируется auth_time, query_id, hash
    ### по-хорошему они должны меняться со временем
    */

    public class PocketFIGame : IGame
    {
        private static readonly HttpClient client = new HttpClient();
        private List<BaseHeader> headers;
        private List<BaseToken> tokens;
        private List<BaseUrl> urls;
        private GameContext gameDB;
        private string nameGame;
        public List<CheckTokenTasks> checkTokenTasksComplete { get; set; }

        public PocketFIGame(string nameGame, GameContext gameDB)
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
            CheckTokenTasks currentCheck = checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First();

            // Создаем необходимый header
            client.DefaultRequestHeaders.Clear();
            // Сколько нужно секунд для повторного запроса
            int waitSeconds = new Random().Next(120000, 160000);
            // Особенность POCKET FI
            // Особенность POCKET FI
            // Особенность POCKET FI
            client.DefaultRequestHeaders.Add("Telegramrawdata", token.Data.ToString());
            // Создаем необходимый токен
            var values = new Dictionary<string, string>
            {
                { "", "" }
            };
            var content = new FormUrlEncodedContent(values);
            
            HttpResponseMessage response = null;
            // Отсылаем запрос
            try
            {
                response = await client.PostAsync(url, content);
            }
            catch (TaskCanceledException ex)
            {
                Logger.GetInstance().Log(LogLevel.Critical, nameGame, $"Timeout 100 seconds -- {ex.Task}");
                waitSeconds = new Random().Next(30000, 60000);
            }

            // Проверка ответа
            if (response == null)
            {
                waitSeconds = new Random().Next(30000, 60000);
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"response us null!");
            }
            if (response.IsSuccessStatusCode)
            {
                PlayGameResponse? data = await response.Content.ReadFromJsonAsync<PlayGameResponse>();
            
                if (data != null)
                {
                    UserMining? result = data.userMining as UserMining;
                    Logger.GetInstance().Log(LogLevel.Information, nameGame, @$"user:{result.miningAmount} total:{result.gotAmount}");
                }
                else
                {
                    Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} Data response is null!");
                    waitSeconds = new Random().Next(60000, 120000);
                }
            }
            else
            {
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} statusCode:{response.StatusCode} phrase:{response.ReasonPhrase}");
                waitSeconds = new Random().Next(9000, 11000); ;
            }

            currentCheck.COUNT_TASKS++;

            await Task.Delay(waitSeconds);
            // Console.WriteLine("PocketFI 10 sec");
            currentCheck.COUNT_TASKS--;
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
                URL = @"https://bot.pocketfi.org/mining/claimMining";
                BaseToken token = tokens[accountIndex];

                // Добавляем в проверку, если ключа нет
                if (!checkTokenTasksComplete.Select(tok => tok.ID_TOKEN).Contains(token.Data))
                {
                    checkTokenTasksComplete.Add(new CheckTokenTasks(token.Data, 0, true));
                }

                if (checkTokenTasksComplete.Where(tok => tok.ID_TOKEN == token.Data).First())
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

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

    public class EngineGame : IGame
    {
        private static readonly HttpClient client = new HttpClient();
        private List<BaseHeader> headers;
        private List<BaseToken> tokens;
        private List<BaseUrl> urls;
        private GameContext gameDB;
        private string nameGame;

        public EngineGame(string nameGame, GameContext gameDB)
        {
            this.nameGame = nameGame;
            this.gameDB = gameDB;
            headers = new List<BaseHeader>();
            tokens = new List<BaseToken>();

            LoadRecources();
        }

        public async Task PlayGame(string url, BaseToken token, BaseHeader header)
        {
            // Создаем необходимый header
            client.DefaultRequestHeaders.Clear();
            // Сколько нужно секунд для повторного запроса
            int waitSeconds = new Random().Next(5, 10); // 120000, 160000);
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
            // Отсылаем запрос
            var response = await client.PostAsync(url, content);
            // Проверка ответа
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
                }
            }
            else
            {
                Logger.GetInstance().Log(LogLevel.Error, nameGame, @$"user:{token.Data} statusCode:{response.StatusCode} phrase:{response.ReasonPhrase}");
            }
            await Task.Delay(waitSeconds);
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

        public async Task MainLoop()
        {
            string URL = "";

            List<Task> tasks = new List<Task>();

            for (int accountIndex = 0; accountIndex < tokens.Count; accountIndex++)
            {
                URL = @"https://bot.pocketfi.org/mining/claimMining";

                BaseToken token = tokens[accountIndex];
                BaseUrl url = gameDB.urls.Where(url => url.URL == URL).First();
                BaseHeader header = url.BaseHeader;

                Task task = PlayGame(URL, token, header);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            tasks.Clear();

        }
    }
}

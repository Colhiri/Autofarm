using System.Text.Json;
using System.Reflection;

using Autofarm.Сommon;
using Autofarm.Сommon.DataBase;
using Autofarm.Cubes.Responses;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Autofarm.Cubes
{
    /*
    ### Запуск майнера возможен без какого-то ни было запуска самого окна
    ### нужен только токен самого пользователя в игре
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
            var response = await client.PostAsync(url, content);
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
                // Майнинг кубов
                URL = @"https://server.questioncube.xyz/game/mined";
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

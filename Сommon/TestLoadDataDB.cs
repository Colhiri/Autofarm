using Autofarm.Сommon.DataBase;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Autofarm.Сommon
{
    public class TestLoadDataDB
    {
        public GameContext db { get; set; }

        public TestLoadDataDB(GameContext db)
        {
            this.db = db;
        }

        public void LoadData()
        {
            // Игры
            BaseGameInfo game1 = new BaseGameInfo { NameGame = "Cubes" };
            BaseGameInfo game2 = new BaseGameInfo { NameGame = "PocketFI" };
            db.gamesInfo.AddRange(game1, game2);

            // Заголовки
            BaseHeader header1 = new BaseHeader { Data = @"{
                                                                ""Accept"": ""application/json"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Linux; Android 14) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.6422.113 Mobile Safari/537.36""
                                                            }" };
            BaseHeader header2 = new BaseHeader { Data = @"{
                                                               ""Accept"": ""/"",
                                                               ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                               ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                               ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0"",
                                                               ""Connection"": ""keep-alive"",
                                                               ""Host"": ""bot.pocketfi.org"",
                                                               ""Origin"": ""https://pocketfi.app"",
                                                               ""Referer"": ""https://pocketfi.app/"",
                                                               ""Sec-Ch-Ua"": """"Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                               ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                               ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                               ""Sec-Fetch-Dest"": ""empty"",
                                                               ""Sec-Fetch-Mode"": ""cors"",
                                                               ""Sec-Fetch-Site"": ""cross-site""
                                                           }" };
            BaseHeader header3 = new BaseHeader { Data = @"{
                                                               ""Accept"": ""/"",
                                                               ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                               ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                               ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0"",
                                                               ""Connection"": ""keep-alive"",
                                                               ""Host"": ""bot.pocketfi.org"",
                                                               ""Origin"": ""https://pocketfi.app"",
                                                               ""Referer"": ""https://pocketfi.app/"",
                                                               ""Sec-Ch-Ua"": """"Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                               ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                               ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                               ""Sec-Fetch-Dest"": ""empty"",
                                                               ""Sec-Fetch-Mode"": ""cors"",
                                                               ""Sec-Fetch-Site"": ""cross-site""
                                                           }" };
            db.headers.AddRange(header1, header2, header3);

            // Ссылки
            BaseUrl url1 = new BaseUrl { URL = "https://server.questioncube.xyz/game/mined", BaseGameInfo = game1, BaseHeader = header1 };
            BaseUrl url2 = new BaseUrl { URL = "https://bot.pocketfi.org/mining/getUserMining", BaseGameInfo = game2, BaseHeader = header2 };
            BaseUrl url3 = new BaseUrl { URL = "https://bot.pocketfi.org/mining/claimMining", BaseGameInfo = game2, BaseHeader = header3 };
            db.urls.AddRange(url1, url2, url3);

            // Токены
            BaseToken token1 = new BaseToken { Data = "ce0427410b069b52ae382b30738befbce56a4cb4d73bda60bc8f7ef8efc780f7", BaseGameInfo = game1 };
            BaseToken token2 = new BaseToken { Data = "b6c0d717144fce4066610e309763ce47044d4450c64c4fa3df3ba8cea89efeeb", BaseGameInfo = game1 };
            BaseToken token3 = new BaseToken { Data = "b5ef0c737964c8c12fecccc0a09f7ffef9972b9063f73a94805928586dab1ddd", BaseGameInfo = game1 };
            BaseToken token4 = new BaseToken { Data = "query_id=AAEL7T0VAAAAAAvtPRUDL6NZ&user=%7B%22id%22%3A356379915%2C%22first_name%22%3A%22Colhiru%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22colhiru%22%2C%22language_code%22%3A%22ru%22%2C%22is_premium%22%3Atrue%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1718982968&hash=4f6f7845ed81452b4c53220b1129220500eba8d7e583e36530a860ce5370fad0", BaseGameInfo = game2 };
            BaseToken token5 = new BaseToken { Data = "query_id=AAHYqLQxAAAAANiotDG_xTOf&user=%7B%22id%22%3A833923288%2C%22first_name%22%3A%22Yutizo%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22yutizo%22%2C%22language_code%22%3A%22ru%22%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1719050763&hash=d6ea91ecb058f54329f42baf4474622dd31c16d2be9d0d6cf32fe72268f8569a", BaseGameInfo = game2 };
            BaseToken token6 = new BaseToken { Data = "query_id=AAHNQeo0AAAAAM1B6jTx97sO&user=%7B%22id%22%3A887767501%2C%22first_name%22%3A%22%D0%90%D0%BD%D0%B0%D1%81%D1%82%D0%B0%D1%81%D0%B8%D1%8F%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22lakshmiiana%22%2C%22language_code%22%3A%22ru%22%2C%22is_premium%22%3Atrue%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1719050893&hash=02ebda93d3aa7d5c7c27b1355f9227f93c8cd76144e2a3bf0a4e1da8c26e77f6", BaseGameInfo = game2 };
            db.tokens.AddRange(token1, token2, token3, token4, token5, token6);

            // Сохранить
            db.SaveChanges();
        }
    }
}

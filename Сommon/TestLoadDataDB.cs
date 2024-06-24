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
            BaseGameInfo cubes = new BaseGameInfo { NameGame = "Cubes" };
            BaseGameInfo pocketfi = new BaseGameInfo { NameGame = "PocketFI" };
            BaseGameInfo timeton = new BaseGameInfo { NameGame = "TimeTon" };
            BaseGameInfo cyberfinancies = new BaseGameInfo { NameGame = "CyberFinancies" };
            BaseGameInfo fuelmining = new BaseGameInfo { NameGame = "FuelMining" };

            db.gamesInfo.AddRange(cubes, pocketfi, timeton, cyberfinancies, fuelmining);

            // Заголовки
            BaseHeader cubesHeader1 = new BaseHeader { Data = @"{
                                                                ""Accept"": ""application/json"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Linux; Android 14) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.6422.113 Mobile Safari/537.36""
                                                            }" };
            BaseHeader pocketfiHeader1 = new BaseHeader { Data = @"{
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
            BaseHeader timetonHeader1 = new BaseHeader { Data = @"{
                                                               ""Accept"": ""*/*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Cookie"": ""access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjEwMDM0OTUsImlhdCI6MTcxOTE3MjYwNX0.lr1xGu_bpV2ShXKBm_TcjAnB5asuIsqrpQZYewenbC8"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://timeton.io/main"",
                                                                ""Sec-Ch-Ua"": ""Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""same-origin"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                           }" };
            BaseHeader timetonHeader2 = new BaseHeader { Data = @"{
                                                               ""Accept"": ""*/*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Cookie"": ""access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjEwMDM0OTUsImlhdCI6MTcxOTE3MjYwNX0.lr1xGu_bpV2ShXKBm_TcjAnB5asuIsqrpQZYewenbC8"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://timeton.io/upgrade"",
                                                                ""Sec-Ch-Ua"": ""Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""same-origin"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                           }" };

            BaseHeader timetonHeader3 = new BaseHeader { Data = @"{
                                                               ""Accept"": ""*/*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Content-Length"": ""414"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Cookie"": ""access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjEwMDM0OTUsImlhdCI6MTcxOTE3MjYwNX0.lr1xGu_bpV2ShXKBm_TcjAnB5asuIsqrpQZYewenbC8"",
                                                                ""Origin"": ""https://timeton.io"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://timeton.io/main"",
                                                                ""Sec-Ch-Ua"": ""Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""same-origin"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                           }" };

            BaseHeader fuelminingHeader1 = new BaseHeader { Data = @"{
                                                                ""Accept"": "",*/*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",,
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Bugsnag-Api-Key"": ""919a364fea0580f1024860dc4d979a83"",
                                                                ""Bugsnag-Payload-Version"": ""1"",
                                                                ""Bugsnag-Sent-At"": ""2024-06-23T20:48:36.210Z"",
                                                                ""Content-Length"": ""489"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Origin"": ""https://mining.fueljetton.com"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://mining.fueljetton.com/"",
                                                                ""Sec-Ch-Ua"": """"Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""cross-site"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                                }" };

            BaseHeader fuelminingHeader2 = new BaseHeader { Data = @"{
                                                                ""Accept"": ""application/json, text/plain, */*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Content-Length"": ""402"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Origin"": ""https://mining.fueljetton.com"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://mining.fueljetton.com/"",
                                                                ""Sec-Ch-Ua"": """"Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""same-origin"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                                }" };

            BaseHeader fuelminingHeader3 = new BaseHeader { Data = @"{
                                                                ""Accept"": ""application/json, text/plain, */*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Content-Length"": ""375"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Origin"": ""https://mining.fueljetton.com"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://mining.fueljetton.com/recycle"",
                                                                ""Sec-Ch-Ua"": """"Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""same-origin"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                                }" };

            BaseHeader fuelminingHeader4 = new BaseHeader { Data = @"{
                                                                ""Accept"": ""application/json, text/plain, */*"",
                                                                ""Accept-Encoding"": ""gzip, deflate, br, zstd"",
                                                                ""Accept-Language"": ""ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7"",
                                                                ""Content-Length"": ""375"",
                                                                ""Content-Type"": ""application/json"",
                                                                ""Origin"": ""https://mining.fueljetton.com"",
                                                                ""Priority"": ""u=1, i"",
                                                                ""Referer"": ""https://mining.fueljetton.com/upgrades"",
                                                                ""Sec-Ch-Ua"": """"Not/A)Brand"";v=""8"", ""Chromium"";v=""126"", ""Microsoft Edge"";v=""126"", ""Microsoft Edge WebView2"";v=""126"""",
                                                                ""Sec-Ch-Ua-Mobile"": ""?0"",
                                                                ""Sec-Ch-Ua-Platform"": ""Windows"",
                                                                ""Sec-Fetch-Dest"": ""empty"",
                                                                ""Sec-Fetch-Mode"": ""cors"",
                                                                ""Sec-Fetch-Site"": ""same-origin"",
                                                                ""User-Agent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0""
                                                                }" };

            db.headers.AddRange(cubesHeader1, 
                pocketfiHeader1, 
                timetonHeader1, timetonHeader2 , timetonHeader3,
                fuelminingHeader1, fuelminingHeader2, fuelminingHeader3, fuelminingHeader4
                );

            // Ссылки
            BaseUrl cubesUrl1 = new BaseUrl { URL = "https://server.questioncube.xyz/game/mined", BaseGameInfo = cubes, BaseHeader = cubesHeader1 }; // POST

            BaseUrl pocketfiUrl1 = new BaseUrl { URL = "https://bot.pocketfi.org/mining/getUserMining", BaseGameInfo = pocketfi, BaseHeader = pocketfiHeader1 }; // GET
            BaseUrl pocketfiUrl2 = new BaseUrl { URL = "https://bot.pocketfi.org/mining/claimMining", BaseGameInfo = pocketfi, BaseHeader = pocketfiHeader1 }; // POST

            BaseUrl timetonUrl1 = new BaseUrl { URL = "https://timeton.io/api/farm/activate", BaseGameInfo = timeton, BaseHeader = timetonHeader1 }; // GET
            BaseUrl timetonUrl2 = new BaseUrl { URL = "https://timeton.io/api/staking/claim", BaseGameInfo = timeton, BaseHeader = timetonHeader2 }; // GET
            BaseUrl timetonUrl3 = new BaseUrl { URL = "https://timeton.io/api/auth", BaseGameInfo = timeton, BaseHeader = timetonHeader3 }; // POST
            BaseUrl timetonUrl4 = new BaseUrl { URL = "https://timeton.io/api/bonus/claim", BaseGameInfo = timeton, BaseHeader = timetonHeader1 }; // GET
            BaseUrl timetonUrl5 = new BaseUrl { URL = "https://timeton.io/api/farm/claim", BaseGameInfo = timeton, BaseHeader = timetonHeader1 }; // GET

            BaseUrl fuelminingUrl1 = new BaseUrl { URL = "https://sessions.bugsnag.com/", BaseGameInfo = fuelmining, BaseHeader = fuelminingHeader1 }; // POST
            BaseUrl fuelminingUrl2 = new BaseUrl { URL = "https://mining.fueljetton.com/api/v2/init", BaseGameInfo = fuelmining, BaseHeader = fuelminingHeader2 }; // POST
            BaseUrl fuelminingUrl3 = new BaseUrl { URL = "https://mining.fueljetton.com/api/v2/claim-oil", BaseGameInfo = fuelmining, BaseHeader = fuelminingHeader2 }; // POST
            BaseUrl fuelminingUrl4 = new BaseUrl { URL = "https://mining.fueljetton.com/api/v2/recycle", BaseGameInfo = fuelmining, BaseHeader = fuelminingHeader3 }; // POST
            BaseUrl fuelminingUrl5 = new BaseUrl { URL = "https://mining.fueljetton.com/api/v2/ad", BaseGameInfo = fuelmining, BaseHeader = fuelminingHeader4 }; // POST


            db.urls.AddRange(cubesUrl1, 
                pocketfiUrl1, pocketfiUrl2, 
                timetonUrl1, timetonUrl2, timetonUrl3, timetonUrl4, timetonUrl5,
                fuelminingUrl1, fuelminingUrl2, fuelminingUrl3, fuelminingUrl4, fuelminingUrl5
                );

            // Токены
            BaseToken cubesToken1 = new BaseToken { Data = "ce0427410b069b52ae382b30738befbce56a4cb4d73bda60bc8f7ef8efc780f7", BaseGameInfo = cubes };
            BaseToken cubesToken2 = new BaseToken { Data = "43a0d1aa226ca9b8544e4d140a8f6891429f4311b63d035e87fdc07436a5cb9c", BaseGameInfo = cubes };
            BaseToken cubesToken3 = new BaseToken { Data = "a6d7c196c3162af0821358720892d060bb19c20eac943d850771a68b5d2472f0", BaseGameInfo = cubes };
            
            BaseToken pocketfiToken1 = new BaseToken { Data = "query_id=AAEL7T0VAAAAAAvtPRUDL6NZ&user=%7B%22id%22%3A356379915%2C%22first_name%22%3A%22Colhiru%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22colhiru%22%2C%22language_code%22%3A%22ru%22%2C%22is_premium%22%3Atrue%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1718982968&hash=4f6f7845ed81452b4c53220b1129220500eba8d7e583e36530a860ce5370fad0", BaseGameInfo = pocketfi };
            BaseToken pocketfiToken2 = new BaseToken { Data = "query_id=AAHYqLQxAAAAANiotDG_xTOf&user=%7B%22id%22%3A833923288%2C%22first_name%22%3A%22Yutizo%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22yutizo%22%2C%22language_code%22%3A%22ru%22%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1719050763&hash=d6ea91ecb058f54329f42baf4474622dd31c16d2be9d0d6cf32fe72268f8569a", BaseGameInfo = pocketfi };
            BaseToken pocketfiToken3 = new BaseToken { Data = "query_id=AAHNQeo0AAAAAM1B6jTx97sO&user=%7B%22id%22%3A887767501%2C%22first_name%22%3A%22%D0%90%D0%BD%D0%B0%D1%81%D1%82%D0%B0%D1%81%D0%B8%D1%8F%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22lakshmiiana%22%2C%22language_code%22%3A%22ru%22%2C%22is_premium%22%3Atrue%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1719050893&hash=02ebda93d3aa7d5c7c27b1355f9227f93c8cd76144e2a3bf0a4e1da8c26e77f6", BaseGameInfo = pocketfi };

            BaseToken timetonToken1 = new BaseToken { Data = "ce0427410b069b52ae382b30738befbce56a4cb4d73bda60bc8f7ef8efc780f7", BaseGameInfo = timeton };
            BaseToken timetonToken2 = new BaseToken { Data = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjEwMDM0OTUsImlhdCI6MTcxOTE3MjYwNX0.lr1xGu_bpV2ShXKBm_TcjAnB5asuIsqrpQZYewenbC8", BaseGameInfo = timeton };
            BaseToken timetonToken3 = new BaseToken { Data = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjg5OTU3NSwiaWF0Ijo", BaseGameInfo = timeton };

            db.tokens.AddRange(cubesToken1, cubesToken2, cubesToken3, 
                pocketfiToken1, pocketfiToken2, pocketfiToken3,
                timetonToken1, timetonToken2, timetonToken3

                );

            // Сохранить
            db.SaveChanges();
        }
    }
}

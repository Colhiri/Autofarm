using Microsoft.EntityFrameworkCore;

using Autofarm.Сommon;
using Autofarm.Сommon.DataBase;

namespace Autofarm
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            Logger logger = Logger.GetInstance();

            GameContext db = new GameContext();
            TestLoadDataDB loadData = new TestLoadDataDB(db);
            loadData.LoadData();

            db.gamesInfo.Load();
            db.urls.Load();
            db.headers.Load();
            db.tokens.Load();

            IGame cubesGame = new Cubes.EngineGame("Cubes", db);
            IGame pocketGame = new PocketFI.EngineGame("PocketFI", db);

            List<IGame> games = new List<IGame>() { pocketGame, cubesGame };

            List<Task> tasks = new List<Task>();

            // Основной цикл фарминга
            while (true)
            {
                for (int gameIndex = 0; gameIndex < games.Count; gameIndex++)
                {
                    IGame game = games[gameIndex];
                    tasks.Add(game.MainLoop());
                }

                await Task.WhenAll(tasks);

                tasks.Clear();
            }
        }
    }
} 
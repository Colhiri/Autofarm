using Microsoft.EntityFrameworkCore;

using Autofarm.Сommon;
using Autofarm.Сommon.DataBase;
using Autofarm.Cubes;
using Autofarm.PocketFI;
using System.Threading.Tasks;

namespace Autofarm
{

    public class TaskWithTimers
    {
        public Task task;
        DateTime endTimeStamp;
        public bool TaskReady => endTimeStamp <= DateTime.Now;

        public TaskWithTimers(Task task, DateTime endTimeStamp)
        {
            this.task = task;
            this.endTimeStamp = endTimeStamp;
        }

    }

    internal class Program
    {
        public static List<TaskWithTimers> MainTasks = new List<TaskWithTimers>();
        public static int MAX_COUNT_TASKS_ON_EACH_TOKEN = 1;
        private static List<IGame> games;

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

            IGame cubesGame = new Cubes.CubesGame("Cubes", db);
            IGame pocketGame = new PocketFI.PocketFIGame("PocketFI", db);

            // IGame FuelGame = new  FuelMining.FuelMiningGame("FuelMining", db);

            games = new List<IGame>() { pocketGame, cubesGame };
            // List<IGame> games = new List<IGame>() { FuelGame };

            while (true)
            {
                foreach (IGame game in games)
                {
                    List<TaskWithTimers> newTasks = await game.MainLoop();

                    if (newTasks.Count > 0)
                    {
                        MainTasks.AddRange(newTasks);
                    }
                }

                List<Task> readyTasks = MainTasks.Where(t => t.TaskReady).Select(t => t.task).ToList();

                if (readyTasks.Count > 0)
                {
                    await Task.WhenAll(readyTasks);

                    readyTasks.Clear();

                    MainTasks.RemoveAll(t => t.task.IsCompleted);
                }
            }
        }
    }
} 
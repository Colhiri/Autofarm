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
        int timerSeconds;
        public bool TaskReady => timerSeconds <= 0;

        public TaskWithTimers(Task task, int timerSeconds)
        {
            this.task = task;
            this.timerSeconds = timerSeconds;
        }

        public void Dicrement()
        {
            timerSeconds--;
        }
    }

    internal class Program
    {
        public static List<TaskWithTimers> MainTasks = new List<TaskWithTimers>();
        public static int MAX_COUNT_TASKS_ON_EACH_TOKEN = 5;
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
                if (games[0].checkTokenTasksComplete.Select(tok => tok.COUNT_TASKS).First() < MAX_COUNT_TASKS_ON_EACH_TOKEN)
                {
                    TaskWithTimers task10 = new TaskWithTimers(games[0].TimeEvent10(), 5);
                    MainTasks.Add(task10);
                }
                if (games[1].checkTokenTasksComplete.Select(tok => tok.COUNT_TASKS).First() < MAX_COUNT_TASKS_ON_EACH_TOKEN)
                {
                    TaskWithTimers task20 = new TaskWithTimers(games[1].TimeEvent20(), 100);
                    MainTasks.Add(task20);
                }

                List<Task> readyTasks = MainTasks.Where(t => t.TaskReady).Select(t => t.task).ToList();

                await Task.WhenAll(readyTasks);

                readyTasks.Clear();

                MainTasks.RemoveAll(t => t.task.IsCompleted);

                MainTasks.ForEach(t => t.Dicrement());

                Thread.Sleep(100);
            }
        }

        private static async Task MainLoopGames()
        {
            
            // Основной цикл фарминга
            while (true)
            {
                MainTasks.RemoveAll(task => task.IsCompleted);

                Console.WriteLine($"Current count tasks = {MainTasks.Count}");

                bool check1 = games[0].checkTokenTasksComplete.Select(tok => tok.NO_LOCK_TOKEN).Contains(true) || 
                    games[0].checkTokenTasksComplete.Count == 0 ||
                    games[0].checkTokenTasksComplete.Select(tok => tok.COUNT_TASKS).First() < MAX_COUNT_TASKS_ON_EACH_TOKEN;

                bool check2 = games[1].checkTokenTasksComplete.Select(tok => tok.NO_LOCK_TOKEN).Contains(true) || 
                    games[1].checkTokenTasksComplete.Count == 0 || 
                    games[1].checkTokenTasksComplete.Select(tok => tok.COUNT_TASKS).First() < MAX_COUNT_TASKS_ON_EACH_TOKEN;

                if (check1)
                {
                    games[0].MainLoop(MainTasks);
                }
                if (check2)
                {
                    games[1].MainLoop(MainTasks);
                }

                check1 = games[0].checkTokenTasksComplete.Select(tok => tok.NO_LOCK_TOKEN).Contains(true);
                check2 = games[1].checkTokenTasksComplete.Select(tok => tok.NO_LOCK_TOKEN).Contains(true);

                while (check1 == false && check2 == false)
                {
                    check1 = games[0].checkTokenTasksComplete.Select(tok => tok.NO_LOCK_TOKEN).Contains(true);
                    check2 = games[1].checkTokenTasksComplete.Select(tok => tok.NO_LOCK_TOKEN).Contains(true);
                }
            }
        }
    }
} 
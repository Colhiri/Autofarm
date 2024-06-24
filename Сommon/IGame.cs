namespace Autofarm.Сommon
{
    public class CheckTokenTasks
    {
        public CheckTokenTasks(string iD_TOKEN, int cOUNT_TASKS)
        {
            ID_TOKEN = iD_TOKEN;
            COUNT_TASKS = cOUNT_TASKS;
        }

        /// <summary>
        /// Идентификатор токена
        /// </summary>
        public string ID_TOKEN { get; set; }

        /// <summary>
        /// Максимальное количество задач в классе
        /// </summary>
        public int COUNT_TASKS { get; set; }

        /// <summary>
        /// Сколько ждать
        /// </summary>
        public int WAIT_SECONDS { get; set; }

    }

    public interface IGame
    {
        /// <summary>
        /// Токены и проверка их занятости (нельзя создать еще одно действие, если токен уже имеется)
        /// </summary>
        List<CheckTokenTasks> checkTokenTasksComplete { get; set; }

        /// <summary>
        /// Основное действие
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="header"></param>
        Task PlayGame(string url, BaseToken token, BaseHeader header);

        /// <summary>
        /// Получить информацию о пользователе/игре
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="header"></param>
        Task GetInfo(string url, BaseToken token, BaseHeader header);

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        BaseToken RefreshToken(string url, BaseHeader header);

        /// <summary>
        /// Получить токен для приложений, которые требуют создания окна, например, BLUM
        /// </summary>
        /// <returns></returns>
        BaseToken GetToken();

        /// <summary>
        /// Загрузить токены, заголовки, команды
        /// </summary>
        void LoadRecources();

        /// <summary>
        /// Основной цикл игры (для нескольких аккаунтов/окон)
        /// </summary>
        void MainLoop(List<Task> MainTasks);
    }
}

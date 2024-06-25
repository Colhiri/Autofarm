
namespace Autofarm.Сommon
{
    public class CheckTokenTasks
    {
        public CheckTokenTasks(string iD_TOKEN)
        {
            ID_TOKEN = iD_TOKEN;
            TIME_START = DateTime.Now;
            TIME_END = TIME_START;
        }

        public void UpdateTimeStamps(int waitMilliSeconds)
        {
            TIME_START = DateTime.Now;
            TIME_END = TIME_START.AddMilliseconds(waitMilliSeconds);
        }

        /// <summary>
        /// Идентификатор токена
        /// </summary>
        public string ID_TOKEN { get; set; }
        public DateTime TIME_START { get; protected set; }
        public DateTime TIME_END { get; protected set; }
    }

    public interface IGame
    {
        /// <summary>
        /// Токены и проверка их занятости (нельзя создать еще одно действие, если токен уже имеется)
        /// </summary>
        List<CheckTokenTasks> checkTokenTasksComplete { get; set; }
        
        /// <summary>
        /// Текущий токен
        /// </summary>
        public CheckTokenTasks currentCheck { get; set; }

        /// <summary>
        /// Основное действие
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="header"></param>
        Task PlayGame(int waitMilliSeconds);

        /// <summary>
        /// Получить информацию о пользователе/игре
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="header"></param>
        Task<int> GetInfo(string url, BaseToken token, BaseHeader header);

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        BaseToken RefreshToken(string url, BaseHeader header);

        /// <summary>
        /// Получить текущий токен
        /// </summary>
        /// <param name="token"></param>
        void GetCurrentToken(BaseToken token);

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
        Task<List<TaskWithTimers>> MainLoop();
    }
}

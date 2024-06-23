namespace Autofarm.Сommon
{
    public interface IGame
    {
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
        Task MainLoop();
    }
}

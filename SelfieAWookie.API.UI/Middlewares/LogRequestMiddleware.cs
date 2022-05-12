namespace SelfieAWookie.API.UI.Middlewares
{
    public class LogRequestMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        ILogger<LogRequestMiddleware> _logger;
        #endregion

        #region Constructors
        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }
        #endregion

        #region Public methods
        public async Task Invoke(HttpContext context)
        {
            this._logger.LogDebug(context.Request.Path.Value);
            await this._next(context);
        }
        #endregion
    }
}

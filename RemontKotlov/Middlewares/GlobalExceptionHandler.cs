namespace RemontKotlov.Middlewares
{
    public class GlobalExceptionHandler
    {
        public RequestDelegate _requestDelegate;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(ex);
            }
        }

        private void HandleExceptionAsync(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}

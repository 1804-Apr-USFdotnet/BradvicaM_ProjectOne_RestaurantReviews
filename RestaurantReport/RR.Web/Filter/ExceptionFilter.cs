using System.Web.Mvc;
using RR.DomainContracts;
using RR.Logging;

namespace RR.Web.Filter
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        private readonly ILoggingService _loggingService;

        public ExceptionFilter()
        {
            _loggingService = new LoggingService();
        }

        public override void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            
            _loggingService.Log(ex);
        }
    }
}
using System;

namespace RR.DomainServices
{
    public interface ILoggingService
    {
        void Log(Exception e);
    }
}

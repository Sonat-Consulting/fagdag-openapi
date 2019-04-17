using System.Collections.Generic;

namespace NetCoreApi.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException (string message = null, IEnumerable<string> details = null) : base(message, details)
        {
        }
    }
}
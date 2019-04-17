using System.Collections.Generic;

namespace NetCoreApi.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message = null, IEnumerable<string> details = null) : base(message, details)
        {
        }
    }
}
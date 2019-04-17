using System.Collections.Generic;

namespace NetCoreApi.Exceptions
{
    public class MappingException : BaseException
    {
        public MappingException(string message = null, IEnumerable<string> details = null) : base(message, details)
        {
        }
    }
}
using System;
using System.Collections.Generic;

namespace NetCoreApi.Exceptions
{
    public class BaseException : Exception
    {
        protected BaseException(string message, IEnumerable<string> details = null) : base(message)
        {
            Details = details;
        }

        private IEnumerable<string> Details { get; }

        public override string Message
        {
            get
            {
                if (Details == null) return base.Message;
                return base.Message + "\n" + string.Join("\n", Details);
            }
        }
    }
}
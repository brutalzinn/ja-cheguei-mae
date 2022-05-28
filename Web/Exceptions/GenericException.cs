using System;
using System.Net;

namespace api_ja_cheguei_mae.Exceptions
{
    public class GenericException : Exception
    {
            public HttpStatusCode Status { get; private set; }

            public GenericException()
            { }

            public GenericException(HttpStatusCode status, string message) : base(message)
            {
                Status = status;
            }
    }
}

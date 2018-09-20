using System;

namespace Web.Exceptions
{
    public class RunpathAlbumWebException : Exception
    {
        public RunpathAlbumWebException() { }

        public RunpathAlbumWebException(string message) : base(message) { }

        public RunpathAlbumWebException(string message, Exception innerException) : base(message, innerException) { }
    }
}

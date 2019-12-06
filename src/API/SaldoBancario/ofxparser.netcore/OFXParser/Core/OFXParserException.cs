using System;

namespace OFXParser.Core
{
    class OFXParserException : Exception
    {
        public OFXParserException() : base()
        {
        }

        public OFXParserException(string message) : base(message)
        {
        }

        public OFXParserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

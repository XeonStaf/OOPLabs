using System;

namespace Shops.Tools
{
    public class CompanyManagerException : Exception
    {
        public CompanyManagerException()
        {
        }

        public CompanyManagerException(string message)
            : base(message)
        {
        }

        public CompanyManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
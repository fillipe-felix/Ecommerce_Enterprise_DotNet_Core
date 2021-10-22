using System;

namespace EE.Core.DomainObjects
{
    /// <summary>
    /// Classe DomainException
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
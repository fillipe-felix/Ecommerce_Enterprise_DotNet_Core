using System;
using FluentValidation.Results;


namespace EE.Core.Messages
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool IsValido()
        {
            throw new NotImplementedException();
        }
    }
}
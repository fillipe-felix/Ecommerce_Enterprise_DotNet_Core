using System;
using FluentValidation.Results;
using MediatR;


namespace EE.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
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
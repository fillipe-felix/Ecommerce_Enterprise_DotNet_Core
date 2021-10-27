using FluentValidation.Results;

namespace EE.Core.Messages
{
    /// <summary>
    /// Classe commandHandler
    /// </summary>
    public abstract class CommandHandler
    {
        /// <summary>
        /// ValidationResult
        /// </summary>
        protected ValidationResult ValidationResult;

        /// <summary>
        /// Construtor ValidationResult
        /// </summary>
        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        /// <summary>
        /// Adiciona erros
        /// </summary>
        /// <param name="mensagem"></param>
        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }
    }
}
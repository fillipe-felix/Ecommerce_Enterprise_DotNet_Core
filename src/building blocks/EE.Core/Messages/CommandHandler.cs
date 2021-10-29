using System.Threading.Tasks;
using EE.Core.Data;
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

        /// <summary>
        /// Método para persistir os dados no banco de dados
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        protected async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit())
            {
                AdicionarErro("Houve um erro ao persistir os dados");
            }
            
            return ValidationResult;
        }
    }
}
using System.Text.RegularExpressions;

namespace EE.Core.DomainObjects
{
    /// <summary>
    /// Classe de Email
    /// </summary>
    public class Email
    {
        public const int EnderecoMaxLength = 254;
        public const int EnderecoMinLength = 5;
        public string Endereco { get; private set; }

        /// <summary>
        /// Construtor do EntityFramework
        /// </summary>
        protected Email()
        {
            
        }

        public Email(string endereco)
        {
            if (!Validar(endereco))
            {
                throw new DomainException("E-mail inválido");
            }
            Endereco = endereco;
        }

        /// <summary>
        /// Regex de validação de e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool Validar(string email)
        {
            var regexEmail =
                new Regex(
                    @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
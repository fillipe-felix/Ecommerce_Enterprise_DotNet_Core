using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EE.Identidade.API.Models
{
    /// <summary>
    /// Classe para cadastro de um usuario
    /// </summary>
    public class UsuarioRegistro
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
        
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string SenhaConfirmacao { get; set; }
    }

    /// <summary>
    /// Classe para login de usuario
    /// </summary>
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
    }

    /// <summary>
    /// Class que tem a resposta do login que retorna o JWT e dados de quem esta logado
    /// </summary>
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }

    /// <summary>
    /// Class que retorna o usuario que tem informações do JWT
    /// </summary>
    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    /// <summary>
    /// Class que tem os dados das claims desse usuario
    /// </summary>
    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
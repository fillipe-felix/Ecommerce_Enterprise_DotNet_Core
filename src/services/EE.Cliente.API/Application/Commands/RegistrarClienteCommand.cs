using System;
using EE.Core.Messages;
using FluentValidation;

namespace EE.Cliente.API.Application.Commands
{
    /// <summary>
    /// Classe RegistrarClienteCommand
    /// </summary>
    public class RegistrarClienteCommand : Command
    {
        /// <summary>
        /// Id da classe
        /// </summary>
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Email do cliente
        /// </summary>
        public string Email { get; private set; }
        
        /// <summary>
        /// Cpf do cliente
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Construtor RegistrarClienteCommand
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cpf"></param>
        public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        /// <summary>
        /// Valida os dados do cliente
        /// </summary>
        /// <returns></returns>
        public override bool IsValido()
        {
            ValidationResult = new RegistrarClientevalidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    /// <summary>
    /// Class RegistrarClientevalidation
    /// </summary>
    public class RegistrarClientevalidation : AbstractValidator<RegistrarClienteCommand>
    {
        /// <summary>
        /// Constructor RegistrarClientevalidation
        /// </summary>
        public RegistrarClientevalidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id cliente inválido");
            
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(TerCpfValido)
                .WithMessage("O CPF informado não é válido");

            RuleFor(c => c.Email)
                .Must(TerEmailValido)
                .WithMessage("O e-mail informado não é válido");
        }

        /// <summary>
        /// Valida se tem CPF valido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        protected static bool TerCpfValido(string cpf)
        {
            return Core.DomainObjects.Cpf.Validar(cpf);
        }

        /// <summary>
        /// Valida se tem Email valido
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected static bool TerEmailValido(string email)
        {
            return Core.DomainObjects.Email.Validar(email);
        }
    }
}
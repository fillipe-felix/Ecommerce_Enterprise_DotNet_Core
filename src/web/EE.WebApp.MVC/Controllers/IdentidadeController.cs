using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using EE.WebApp.MVC.Models;
using EE.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Claim = System.Security.Claims.Claim;

namespace EE.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {

        private readonly IAutenticacaoService _autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        
        /// <summary>
        /// Retorna a View de Registro
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }

        /// <summary>
        /// Realiza o Resgistro de um usuário
        /// </summary>
        /// <param name="usuarioRegistro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioRegistro);
            }
            
            // API - Registro
            var resposta = await _autenticacaoService.Registro(usuarioRegistro);
            
            if (false)
            {
                return  View(usuarioRegistro);
            }
            
            // Realizar Login na APP
            await RealizarLogin(resposta);
            
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Retorna a View de Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Realiza o Login de um usuário
        /// </summary>
        /// <param name="usuarioLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioLogin);
            }
            
            // API - Login
            var resposta = await _autenticacaoService.Login(usuarioLogin);
            
            if (false)
            {
                return  View(usuarioLogin);
            }

            // Realizar Login na APP
            await RealizarLogin(resposta);
            
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Faz o logout do usuario na aplicação
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        
        /// <summary>
        /// quando o usuario for realizar o login, ele vai realizar o login mas trabalhando com cookie
        /// inclusive o JWT e as propreidades de autenticação
        /// </summary>
        /// <param name="usuarioRespostaLogin"></param>
        private async Task RealizarLogin(UsuarioRespostaLogin usuarioRespostaLogin)
        {
            var token = ObterTokenFormatado(usuarioRespostaLogin.AccessToken);

            var claims = new List<Claim>();
            
            //Add meu token dentro de uma Claim
            claims.Add(new Claim("JWT", usuarioRespostaLogin.AccessToken));
            claims.AddRange(token.Claims);

            // consegue gerar os claims dentro do formato de cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        
        /// <summary>
        /// obtem apenas o meu token trazendo todas as informações dele
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns>JwtSecurityToken</returns>
        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        }
    }
}
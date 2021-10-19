using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EE.WebApi.Core.Identidade
{
    public class CustomAuthorization
    {
        /// <summary>
        /// Verifica se usuario esta autenticado e se a claim passada o usuario possui na lista de claims do identity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
        
        public class ClaimsAuthorizeAttribute : TypeFilterAttribute
        {
            public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
            {
                Arguments = new object[]
                {
                    new Claim(claimName, claimValue)
                };
            }
        }
        
        public class RequisitoClaimFilter : IAuthorizationFilter
        {
            private readonly Claim _claim;

            public RequisitoClaimFilter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                //verifica se esta autenticado, se nao estiver ja retorna um 401
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new StatusCodeResult(401);
                    return;
                }

                //verifica se ele tem a claim para acessar o recurso e retorna um 403
                if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
        }
    }
}
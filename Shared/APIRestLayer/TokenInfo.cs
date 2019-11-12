using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIRestLayer
{
    public static class TokenInfo
    {
        public static string getClaim(HttpRequestMessage req, string atributo)
        {
            try
            {
                if (req.Headers.GetValues("Authorization").First() != null)
                {
                    string token = req.Headers.GetValues("Authorization").First();
                    var stream = token.Substring(7);
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                    var jti = tokenS.Claims.First(claim => claim.Type == atributo).Value;
                    if(jti != null)
                    {
                        return jti;
                    }
                    throw new ECompartida("No existe el atributo buscado en el token");
                }
                throw new ECompartida("Error con el token enviado");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

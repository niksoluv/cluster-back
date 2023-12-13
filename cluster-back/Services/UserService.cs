using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cluster_back.Services
{
    public class UserService
    {
        IRepositoryWrapper _wrapper;
        ILogger _logger;
        public UserService(IRepositoryWrapper wrapper, ILogger logger)
        {
            this._wrapper = wrapper;
            this._logger = logger;
        }
        public async Task<object> GetToken(UserModel user)
        {
            try
            {
                var identity = await GetIdentity(user.Username, user.Password);
                if (identity == null)
                {
                    return null;
                    //return BadRequest(new { errorText = "Invalid username or password." });
                }

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{GetToken} All method error", typeof(UserService));
                return null;
            }
        }
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            try
            {
                User user = await _wrapper.User.FindByCondition(
                    x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{GetIdentity} All method error", typeof(UserService));
                return null;
            }
        }
    }
}

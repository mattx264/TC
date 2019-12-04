using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;

namespace TC.WebService.Helpers
{
    public interface IUserHelper
    {
        public string PasswordHash(string password);
        public string GetGuid(ClaimsPrincipal claimsPrincipal);
        public string GetClaims(ClaimsPrincipal claimsPrincipal, string name);
        UserModel GetUser(ClaimsPrincipal user);
    }
    public class UserHelper : IUserHelper
    {
        // TODO move Salt to appsettings
        private const string Salt = "NZsP7NnmfBuYeJrRAKNuVQ==";
        private IUserRepository _userRepository;

        public UserHelper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string PasswordHash(string password)
        {

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        public string GetGuid(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("Guid").Value;

        }

        public string GetClaims(ClaimsPrincipal claimsPrincipal, string name)
        {
            return claimsPrincipal.FindFirst(name).Value;
        }
        public UserModel GetUser(ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }
            return _userRepository.GetByGuid(GetGuid(user));
        }
    }

}
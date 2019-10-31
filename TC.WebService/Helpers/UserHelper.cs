using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TC.WebService.Helpers
{
    public static class UserHelper
    {
        // TODO move Salt to appsettings
        private const string Salt = "NZsP7NnmfBuYeJrRAKNuVQ==";

        public static string PasswordHash(string password)
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
        public static string GetGuid (ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("Guid").Value;
           
        }
        public static string GetClaims(ClaimsPrincipal claimsPrincipal, string name) {
            return claimsPrincipal.FindFirst(name).Value;
        }

}

}

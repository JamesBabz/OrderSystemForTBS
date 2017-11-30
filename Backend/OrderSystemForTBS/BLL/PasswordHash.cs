using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class PasswordHash
    {
        // The password salt is 1024 bits (=128 bytes) long.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

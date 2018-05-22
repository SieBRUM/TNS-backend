using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TNSApi.Mapping;

namespace TNSApi.Services
{
    public static class AuthorizationService
    {

        public static AuthorizatedMessage CheckIfAuthorized(ref User user, ref IDatabaseServiceProvider db, HttpRequestHeaders headers, AccessLevel accessLevel)
        {
            if(!headers.TryGetValues("username", out var usernameValues) || !headers.TryGetValues("token", out var tokenValues))
            {
                return AuthorizatedMessage.NoUserError;
            }

            var username = usernameValues.FirstOrDefault();
            var token = tokenValues.FirstOrDefault();

            user = db.Users.Where(x => x.Username == username && x.Token == token).FirstOrDefault();

            if (user == null)
            {
                return AuthorizatedMessage.WrongCredentialsError;
            }

            if (user.IsActive == false)
            {
                return AuthorizatedMessage.NotActiveError;
            }

            if ((int)accessLevel == 1 && user.AccessLevel != "Admin")
            {
                return AuthorizatedMessage.AccessLevelError;
            }

            return AuthorizatedMessage.Authorized;
        }


        // source: https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }

    public enum AccessLevel
    {
        Default,
        Admin
    }

    public enum AuthorizatedMessage
    {
        Authorized,
        WrongCredentialsError,
        NoUserError,
        AccessLevelError,
        NotActiveError
    }
}
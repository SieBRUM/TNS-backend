using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
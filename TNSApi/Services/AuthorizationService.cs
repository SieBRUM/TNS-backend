using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNSApi.Mapping;

namespace TNSApi.Services
{
    public static class AuthorizationService
    {

        public static AuthorizatedMessage CheckIfAuthorized(ref User user, ref IDatabaseServiceProvider db, AccessLevel accessLevel)
        {
            if(user == null)
            {
                return AuthorizatedMessage.NoUserError;
            }

            string username = user.Username;
            string token = user.Token;

            user = db.Users.Where(x => x.Username == username && x.Token == token).FirstOrDefault();

            if(user == null)
            {
                return AuthorizatedMessage.WrongCredentialsError;
            }

            if((int)accessLevel == 1 && user.AccessLevel != "Admin")
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
        AccessLevelError
    }
}
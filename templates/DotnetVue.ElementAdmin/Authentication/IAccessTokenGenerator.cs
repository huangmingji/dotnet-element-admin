using System;
using System.Collections.Generic;

namespace DotnetVue.ElementAdmin.Authentication
{
    public interface IAccessTokenGenerator
    {
        string GenerateToken(string userId, List<string> permissions, DateTime expires);
    }
}
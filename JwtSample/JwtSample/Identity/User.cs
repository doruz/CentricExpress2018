using System;
using Identity.Dapper.Entities;

namespace JwtSample
{
    public class User : DapperIdentityUser<int>
    {
        public User(string username) : base(username)
        {
        }

        public User()
        {
        }
    }
}

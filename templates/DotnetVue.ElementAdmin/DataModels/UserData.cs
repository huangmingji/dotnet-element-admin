using System;
using System.Collections.Generic;

namespace DotnetVue.ElementAdmin.DataModels
{
    public class UserData
    {
        private static Guid _id = Guid.NewGuid();

        public UserData()
        {
            Id = _id;
            Name = "washon";
            Account = "admin";
            Password = "password";
            HeadIcon = "static/img/portrait.jpg";
            Permissions = new List<string>() { "admin" };
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string HeadIcon { get; set; }

        public List<string> Permissions { get; set; }
    }
}

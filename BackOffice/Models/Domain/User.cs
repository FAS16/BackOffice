using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice.Models.Auth;
using WebGrease.Css.Ast;

namespace BackOffice.Models.Domain
{
    public class User
    {
        public int Id { get; set; }

        private static User _instance;
        public string Email { get; set; }
        public BearerToken Token { get; set; } 
        


        private User() { }
        public static User Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new User();
                }
                return _instance;
            }
        }


        public void Nullify()
        {
            _instance = null;
        }



    }
}
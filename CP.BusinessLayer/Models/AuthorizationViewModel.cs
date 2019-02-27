using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class AuthorizationViewModel
    {
        public int AuthorizationId { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class Authorization
    {
        public int AuthorizationId { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
    public enum Role
    {
        Admin,
        Dentist,
        Registrator
    }
}

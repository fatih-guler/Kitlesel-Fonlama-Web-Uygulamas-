using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    [Table("Users")]
    public class User:MyEntityBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public long TelephoneNumber { get; set; }
        public string CompanyName { get; set; }
        public bool IsInvestor { get; set; }

        //Mapping
        public virtual List<Comment> comments { get; set; }
        public virtual List<Address> addresses { get; set; }

        public User()
        {
            this.comments = new List<Comment>();
            this.addresses = new List<Address>();
        }
       
    }
}

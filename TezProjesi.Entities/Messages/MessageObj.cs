using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.Messages
{
    public class MessageObj
    {
        public string Description { get; set; }

        public MessageErrorCode MessageCode { get; set; }
    }
}

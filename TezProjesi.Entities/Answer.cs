using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    public class Answer:MyEntityBase
    {
        public string AnswerDescription { get; set; }
        public virtual User user { get; set; }
        public virtual Question question { get; set; }
    }
}

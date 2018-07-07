using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    public class Question:MyEntityBase
    {
        public string QuestionDescription { get; set; }
        //Mapping
        public virtual User user { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual Project Project { get; set; }
        public Question()
        {
            this.Answers = new List<Answer>();
        }
    }
}

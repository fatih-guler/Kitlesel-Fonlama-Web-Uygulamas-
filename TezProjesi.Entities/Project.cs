using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.Entities
{
    [Table("Projects")]
   public class Project:MyEntityBase
    {
        public string ProjectName { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime ProjectDuration { get; set; }
        public decimal ProjectAmount { get; set; }
        public string ProjectDescription { get; set; }
        public byte[] ImageOfProject { get; set; }

       //Mapping
        public virtual User user { get; set; }
        public virtual List<Comment> CommentsOfProject { get; set; }

        public virtual List<Reward> Rewards { get; set; }
        public virtual List<Fund> Funds { get; set; }
        public virtual List<Question> Questions { get; set; }

        public Project()
        {
            this.Rewards = new List<Reward>();
            this.Funds = new List<Fund>();
            this.Questions = new List<Question>();
            this.CommentsOfProject = new List<Comment>();
            this.user = new User();
        }
    }
}

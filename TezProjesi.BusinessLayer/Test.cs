using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.DataAccessLayer.EntityFramework;
using TezProjesi.Entities;

namespace TezProjesi.BusinessLayer
{
    public class Test
    {
        Repository<Project> projects;
        public Test()
        {
            projects = new Repository<Project>();
        }
        public void Get()
        {
            projects.Insert(new Project() { ProjectDescription = "asdasd" });
        }
    }
}

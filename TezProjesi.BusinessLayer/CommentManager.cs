using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.BusinessLayer.Abstract;
using TezProjesi.DataAccessLayer.EntityFramework;
using TezProjesi.Entities;

namespace TezProjesi.BusinessLayer
{
    public class CommentManager:ManagerBase<Comment>
    {
        Repository<User> repo_user = new Repository<User>();
        Repository<Project> repo_project = new Repository<Project>();
        public bool AddComment(int ProjectId, int Userid, string comment,string comment_title)
        {
            Project p = repo_project.Find(x => x.Id == ProjectId);
            User user = repo_user.Find(y => y.Id == Userid);
            Comment my_comment = new Comment()
            {
                Content = comment,
                Date = DateTime.Now,
                Project = p,
                User = user,
                Title = comment_title
            };
            base.Insert(my_comment);
            int result = Save();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool RemoveComment(int comment_id)
        {
            Comment c = Find(x => x.Id == comment_id);
            
            Delete(c);
            int delete_result = Delete(c);
            if (delete_result > 0)
            {
                return true;
            }
            return false;
        }
        public bool ConfirmComment(int comment_id)
        {
            Comment c = Find(x => x.Id == comment_id);
            c.IsConfirmed = true;
            Update(c);      
            return true;
        }
    }
}

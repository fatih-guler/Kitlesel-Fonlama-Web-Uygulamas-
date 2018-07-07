using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.BusinessLayer.Abstract;
using TezProjesi.BusinessLayer.Results;
using TezProjesi.DataAccessLayer.EntityFramework;
using TezProjesi.Entities;
using TezProjesi.Entities.Messages;

namespace TezProjesi.BusinessLayer
{
    public class ProjectManager : ManagerBase<Project>
    {

        Repository<Reward> repo_reward = new Repository<Reward>();

        //Bütün Projeleri Döndür
        public List<Project> GetAllProject()
        {
            return List();
        }
        //Id si bilinen Projeyi Döndür
        public Project GetProjectById(int id)
        {
            return Find(x => x.Id == id);
        }
        public int AddProject(Project newproject)
        {


            int project_result = Insert(newproject);
            if (project_result > 0)
            {
                int id = newproject.Id;
                return id;
            }
            return -1;
        }
        public BusinessLayerResult<Reward> AddReward(List<Reward> new_rewards)
        {
            BusinessLayerResult<Reward> myreward = new BusinessLayerResult<Reward>();
            foreach (Reward r in new_rewards)
            {
                repo_reward.Insert(r);
                if (Save() <= 0)
                {
                    myreward.AddError(MessageErrorCode.RewardCouldntAdd, r.Content+ " ödülü eklenemedi.");
                }
            }
            return myreward;
        }

    }
}

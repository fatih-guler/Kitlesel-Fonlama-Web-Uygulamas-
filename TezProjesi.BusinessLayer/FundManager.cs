using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.BusinessLayer.Abstract;
using TezProjesi.Entities;
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.BusinessLayer
{
    public class FundManager:ManagerBase<Fund>
    {
        public bool AddFund(FundOfProjectViewModel model,User user , Project project)
        {

            Fund fund = new Fund()
            {
                Amount = model.ccAmount,
                Date = DateTime.Now,
                user_Investor = user,
                Project = project,
            };

            int result = Insert(fund);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}

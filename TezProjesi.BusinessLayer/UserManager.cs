using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.BusinessLayer.Abstract;
using TezProjesi.BusinessLayer.Results;
using TezProjesi.Entities;
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.BusinessLayer
{
    public class UserManager:ManagerBase<User>
    {
        public bool ChangePassword(int sesid, string OldPassword, string NewPassword, string Re_NewPassword)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            res.Result = Find(x => x.Id == sesid&& x.Password == OldPassword);
            if (res.Result != null && Re_NewPassword == NewPassword)
            {
                res.Result.Password = NewPassword;
                int sonuc = Update(res.Result);
                if (sonuc > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}

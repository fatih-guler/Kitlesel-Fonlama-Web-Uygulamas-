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
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.BusinessLayer
{
    public class LoginRegisterManager:ManagerBase<User>
    {
        Repository<User> repo_user = new Repository<User>();
        
        public BusinessLayerResult<User> Login(LoginViewModel user)
        {
            BusinessLayerResult<User> MyUser = new BusinessLayerResult<User>();
            MyUser.Result = Find(x => x.UserName == user.UserName.ToLower() && x.Password == user.Password.ToLower());
            if (MyUser.Result == null)
            {
                MyUser.AddError(MessageErrorCode.UserIsNotFound, "Böyle bir kullanıcı bulunamadı...");
            }
            return MyUser;
        }
        public BusinessLayerResult<User> Register(RegisterViewModel user)
        {
            //E-mail ve kullanıcı adı var mı 
            BusinessLayerResult<User> MyUser = new BusinessLayerResult<User>();
            MyUser.Result = Find(x => x.UserName.ToLower() == user.UserName.ToLower()  || x.Email == user.Email);
            if (MyUser.Result != null)
            {
                MyUser.AddError(MessageErrorCode.UserAlreadyExists, "Girilen kullanıcı adı veya mail adresi zaten kullanılıyor");
            }
            else
            {
                User new_User = new User() { Email = user.Email, Password = user.Password, UserName = user.UserName, IsInvestor = user.IsInvestor  };

                int db_result = repo_user.Insert(new_User);
                if (db_result<=0)
                {
                    MyUser.AddError(MessageErrorCode.AnErrorOccur, "Kullanıcı Kaydedilirken Bir Hata Meydana Geldi");
                }
            }
            return MyUser;

        }
    }
}

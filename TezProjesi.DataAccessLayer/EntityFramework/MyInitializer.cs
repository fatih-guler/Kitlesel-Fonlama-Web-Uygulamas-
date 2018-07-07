using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TezProjesi.Entities;
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        //Veritabanı Oluşturulduktan Sonra
        protected override void Seed(DatabaseContext context)
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    Investor investor = new Investor()
            //    {
            //        FullName = "mck" + i,
            //        Password = "123",
            //        TelephoneNumber = 123,
            //        UserName = "Muhsi" + i
            //    };

            //    context.Investors.Add(investor);
            //}
            //context.SaveChanges();
            base.Seed(context);
        }
        //Veritabanı Oluşturulmadan Önce
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
        }
    }
}

namespace Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Design;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Migrations.Utilities;
    using System.Linq;
    using Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DataAccessLayer.GarmentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.DataAccessLayer.GarmentContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "quantri"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "quantri" };

                manager.Create(user, "Vitv@!@#");
                manager.AddToRole(user.Id, "Admin");
            }
            context.Factories.AddOrUpdate(
                p => p.Id,
                new Factory { Id = 1, Name = "Xí nghiệp 01" }
            );
        }
    }
}

namespace GameSlam.Infrastructure.Migrations
{
    using Core.Enums;
    using Core.Extentions;
    using Core.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repositories;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<GameSlam.Infrastructure.Repositories.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Data_intitial";
        }


        /// <summary>
        /// This method is called after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(GameSlam.Infrastructure.Repositories.ApplicationDbContext context)
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

            // RUN::  
            // Add-Migration Initial >>>> enable migration
            // Add-Migration Initial -IgnoreChanges >>::>(track changes generates up and down)
            // update -database -Verbose  >>::>(push database changes)

            context.Categories.SeedEnumValues<Category, CategoryEnum>(@enum => @enum);
            context.SaveChanges();

            context.ApprovalStatuses.SeedEnumValues<ApprovalStatus, ApprovalStatusEnum>(@enum => @enum);
            context.SaveChanges();

            
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "AuthorizedUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AuthorizedUser" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "asukhu@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "asukhu@gmail.com" };
                string userPWD = "Welcome1!";                                                                      

                manager.Create(user, userPWD);
                manager.AddToRole(user.Id, "Admin");
            }
        }


    }
}

namespace CSharpBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSharpBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CSharpBlog.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }
            if (userManager.FindByEmail("administrator@csharpblog.bg")==null)
            {
                var user = new ApplicationUser
                {
                    UserName = "administrator@csharpblog.bg",
                    Email = "administrator@csharpblog.bg"
                };
                IdentityResult userResult = userManager.Create(user, "administrator");
                if (userResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category() { Name = "Articles" });
                context.Categories.Add(new Category() { Name = "Hardware" });
                context.Categories.Add(new Category() { Name = "Lifestyle" });
                context.Categories.Add(new Category() { Name = "News" });
                context.Categories.Add(new Category() { Name = "Projects" });
                context.Categories.Add(new Category() { Name = "Tutorials" });
            }
        }
    }
}

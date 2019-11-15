namespace AdminPanel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections;
    using System.Data.Entity.Infrastructure;
    using AdminPanel.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminPanel.Models.DatabaseEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdminPanel.Models.DatabaseEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.            
        }
    }
}

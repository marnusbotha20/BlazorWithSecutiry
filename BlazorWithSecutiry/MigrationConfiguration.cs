using BlazorWithSecutiry.Data;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;

namespace BlazorWithSecutiry
{
    internal sealed class MigrationConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsAssembly = Assembly.GetExecutingAssembly();
            MigrationsNamespace = "MyProgram.Migrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // TODO: Initialize seed data here
        }

        private void DoDbUpdate()
        {
            DbMigrator migrator = new DbMigrator(new MigrationConfiguration());
            foreach (string migration in migrator.GetPendingMigrations())
                Console.WriteLine(migration);
            migrator.Update();
        }
    }
}

namespace MasssDefect.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MasssDefect.Data.MassDefectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MasssDefect.Data.MassDefectContext context)
        {
            //var solars = context.SaveChanges();
        }
    }
}

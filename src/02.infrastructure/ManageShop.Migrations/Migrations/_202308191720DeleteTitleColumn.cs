using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202308191720)]
    public class _202308191720DeleteTitleColumn : Migration
    {
        public override void Up()
        {
            Delete.Column("ProductTitle").FromTable("NewProducts");
        }
        public override void Down()
        {
            Create.Column("ProductTitle").OnTable("NewProducts").AsString().NotNullable();
        }

        
    }
}

using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202320081634)]
    public class _202320081634DeletePriceColumn : Migration
    {
        public override void Up()
        {
            Delete.Column("Price").FromTable("Products");
        }

        public override void Down()
        {
            Create.Column("Price").OnTable("Products").AsDouble().NotNullable();

        }
    }
}

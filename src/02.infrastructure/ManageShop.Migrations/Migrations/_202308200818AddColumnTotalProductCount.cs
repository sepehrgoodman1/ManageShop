using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202308200818)]
    public class _202308200818AddColumnTotalProductCount : Migration
    {
        public override void Up()
        {
            Create.Column("TotalProductCount").OnTable("SalesInvoices").AsInt32()
                .NotNullable();
        }

        public override void Down()
        {
            Delete.Column("TotalProductCount").FromTable("SalesInvoices");
        }

    
    }
}

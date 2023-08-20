using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202320080818)]
    public class _202320080818AddColumnTotalProductCount : Migration
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

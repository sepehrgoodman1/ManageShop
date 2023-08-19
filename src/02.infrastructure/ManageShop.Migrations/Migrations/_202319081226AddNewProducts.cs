using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202319081226)]
    public class _202319081226AddNewProducts : Migration
    {
        public override void Up()
        {
            AddNewProductsTable();
        }

        public override void Down()
        {
            Delete.Table("NewProducts");
        }

        void AddNewProductsTable()
        {
            Create.Table("NewProducts").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                .WithColumn("ProductCode").AsInt32().NotNullable()
                                .WithColumn("ProductTitle").AsString().NotNullable()
                                .WithColumn("ProductRecivedCount").AsInt32().NotNullable()
                                .WithColumn("PurchaseInvoiceId").AsInt32().NotNullable()
                                .ForeignKey("FK_NewProducts_ProductGroup", "PurchaseInvoices", "Id");
        }

       
    }
}

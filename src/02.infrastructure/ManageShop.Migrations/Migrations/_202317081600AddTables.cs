using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202317081600)]
    public class _202317081600AddTables : Migration
    {
        public override void Up()
        {

        }
        public override void Down()
        {

        }

        void CreateProducts()
        {
            Create.Table("Products").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                  .WithColumn("Title").AsString()
                                  .WithColumn("MinimumInventory").AsInt32()
                                  .WithColumn("Inventory").AsInt32()
                                  .WithColumn("Price").AsDouble()
                                  .WithColumn("Status").AsInt32()
                                  .WithColumn("ProductGroupId").AsInt32()
                                  .ForeignKey("FK_Products_ProductGroup", "ProductGroup", "Id");
        }
        void CreateProductGroup()
        {
            Create.Table("ProductGroups").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                 .WithColumn("Name").AsString(); 
        }
        void CreateProductPurchaseInvoice()
        {
            Create.Table("ProductPurchaseInvoices").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("ProductId").AsInt32().PrimaryKey()
                                    .ForeignKey("FK_ProductPurchaseInvoices_Product", "Products", "Id")
                                    .WithColumn("PurchaseInvoiceId").AsInt32().PrimaryKey()
                                    .ForeignKey("FK_ProductPurchaseInvoices_PurchaseInvoice", "PurchaseInvoices", "Id");
        }
        void CreateProductSalesInvoice()
        {
            Create.Table("ProductSalesInvoices").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("ProductId").AsInt32().PrimaryKey()
                                    .ForeignKey("FK_ProductSalesInvoice_Product", "Products", "Id")
                                    .WithColumn("SalesInvoicesId").AsInt32().PrimaryKey()
                                    .ForeignKey("FK_ProductSalesInvoice_PurchaseInvoice", "SalesInvoice", "Id");
        }

    }
}

           




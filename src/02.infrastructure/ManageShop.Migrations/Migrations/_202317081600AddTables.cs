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
            CreateProductGroup();
            CreatePurchaseInvoice();
            CreateProducts();
            CreateProductPurchaseInvoice();
            CreateSalesInvoice();
            CreateAccountingDocument();
            CreateProductSalesInvoice();
        }
        public override void Down()
        {
            Delete.Table("ProductSalesInvoices");
            Delete.Table("AccountingDocuments");
            Delete.Table("SalesInvoices");
            Delete.Table("ProductPurchaseInvoices");
            Delete.Table("Products");
            Delete.Table("PurchaseInvoices");
            Delete.Table("ProductGroups");
        }

        void CreateProductGroup()
        {
            Create.Table("ProductGroups").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                 .WithColumn("Name").AsString().NotNullable();
        }
        void CreatePurchaseInvoice()
        {
            Create.Table("PurchaseInvoices").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("Date").AsDateTime().NotNullable();
        }

        void CreateProducts()
        {
            Create.Table("Products").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                  .WithColumn("Title").AsString().NotNullable()
                                  .WithColumn("MinimumInventory").AsInt32().NotNullable()
                                  .WithColumn("Inventory").AsInt32().NotNullable()
                                  .WithColumn("Price").AsDouble().NotNullable()
                                  .WithColumn("Status").AsInt32().NotNullable()
                                  .WithColumn("ProductGroupId").AsInt32().NotNullable()
                                  .ForeignKey("FK_Products_ProductGroup", "ProductGroups", "Id");
        }
      
        void CreateProductPurchaseInvoice()
        {
            Create.Table("ProductPurchaseInvoices").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("ProductId").AsInt32()
                                    .ForeignKey("FK_ProductPurchaseInvoices_Product", "Products", "Id")
                                    .WithColumn("PurchaseInvoiceId").AsInt32()
                                    .ForeignKey("FK_ProductPurchaseInvoices_PurchaseInvoice", "PurchaseInvoices", "Id");
        }
        void CreateSalesInvoice()
        {
            Create.Table("SalesInvoices").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("ClientName").AsString().NotNullable()
                                    .WithColumn("Date").AsDateTime().NotNullable()
                                    .WithColumn("TotalSales").AsDouble().NotNullable();
        }

        void CreateAccountingDocument()
        {
            Create.Table("AccountingDocuments").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("Date").AsDateTime().NotNullable()
                                    .WithColumn("SalesInvoiceId").AsInt32().NotNullable()
                                    .WithColumn("TotalPrice").AsDouble().NotNullable();
        }

     
       
        void CreateProductSalesInvoice()
        {
            Create.Table("ProductSalesInvoices").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                    .WithColumn("ProductId").AsInt32()
                                    .ForeignKey("FK_ProductSalesInvoice_Product", "Products", "Id")
                                    .WithColumn("SalesInvoicesId").AsInt32()
                                    .ForeignKey("FK_ProductSalesInvoice_PurchaseInvoice", "SalesInvoices", "Id");
        }

    }
}

           




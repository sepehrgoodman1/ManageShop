﻿using FluentMigrator;

namespace ManageShop.Migrations.Migrations
{
    [Migration(202320081430)]
    public class _202320081430AddOneToOneRelation : Migration
    {
        public override void Up()
        {
            Create.ForeignKey("FK_AccountingDocuments_SalesInvoices") 
                            .FromTable("AccountingDocuments").ForeignColumn("SalesInvoiceId")
                            .ToTable("SalesInvoices").PrimaryColumn("Id");

        }

        public override void Down()
        {
            Delete.ForeignKey("FK_AccountingDocuments_SalesInvoices").OnTable("AccountingDocuments");
        }

     
    }
}

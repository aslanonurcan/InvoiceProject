namespace InvoiceProjectDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class original : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceDetails", "product_ProductID", "dbo.Products");
            DropIndex("dbo.InvoiceDetails", new[] { "product_ProductID" });
            RenameColumn(table: "dbo.InvoiceDetails", name: "product_ProductID", newName: "ProductID");
            DropPrimaryKey("dbo.InvoiceDetails");
            AddColumn("dbo.InvoiceHeaders", "InvoiceAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceDetails", "VAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceDetails", "invoiceheader_InvoieceID", c => c.Int());
            AlterColumn("dbo.InvoiceDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceDetails", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceDetails", "VATAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceDetails", "TotalAmountwithVAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceDetails", "ProductID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.InvoiceDetails", new[] { "InvoiceID", "ProductID" });
            CreateIndex("dbo.InvoiceDetails", "ProductID");
            CreateIndex("dbo.InvoiceDetails", "invoiceheader_InvoieceID");
            AddForeignKey("dbo.InvoiceDetails", "invoiceheader_InvoieceID", "dbo.InvoiceHeaders", "InvoieceID");
            AddForeignKey("dbo.InvoiceDetails", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            DropColumn("dbo.InvoiceDetails", "UrunID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceDetails", "UrunID", c => c.Int(nullable: false));
            DropForeignKey("dbo.InvoiceDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.InvoiceDetails", "invoiceheader_InvoieceID", "dbo.InvoiceHeaders");
            DropIndex("dbo.InvoiceDetails", new[] { "invoiceheader_InvoieceID" });
            DropIndex("dbo.InvoiceDetails", new[] { "ProductID" });
            DropPrimaryKey("dbo.InvoiceDetails");
            AlterColumn("dbo.InvoiceDetails", "ProductID", c => c.Int());
            AlterColumn("dbo.InvoiceDetails", "TotalAmountwithVAT", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "VATAmount", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "TotalAmount", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "Quantity", c => c.String());
            DropColumn("dbo.InvoiceDetails", "invoiceheader_InvoieceID");
            DropColumn("dbo.InvoiceDetails", "VAT");
            DropColumn("dbo.InvoiceHeaders", "InvoiceAmount");
            AddPrimaryKey("dbo.InvoiceDetails", new[] { "InvoiceID", "UrunID" });
            RenameColumn(table: "dbo.InvoiceDetails", name: "ProductID", newName: "product_ProductID");
            CreateIndex("dbo.InvoiceDetails", "product_ProductID");
            AddForeignKey("dbo.InvoiceDetails", "product_ProductID", "dbo.Products", "ProductID");
        }
    }
}

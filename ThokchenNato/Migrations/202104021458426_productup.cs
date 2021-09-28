﻿namespace ThokchenNato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PhotoURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PhotoURL");
        }
    }
}

namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "MiddleName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "FirstName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "FirstName", c => c.String());
            AlterColumn("dbo.Student", "MiddleName", c => c.String());
            AlterColumn("dbo.Student", "LastName", c => c.String());
        }
    }
}

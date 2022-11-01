using FluentMigrator;

namespace OkArt.Migrations;

[Migration(0)]
public class InitMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("user")
            .WithColumn("login").AsString(256).PrimaryKey()
            .WithColumn("password_hash").AsString(256);

        Create.Table("solution")
            .WithColumn("id").AsInt64().Identity().PrimaryKey()
            .WithColumn("name").AsString(256)
            .WithColumn("owner_login").AsString(256)
            .WithColumn("file_name").AsString(128)
            .WithColumn("description").AsString(8000).Nullable();
    }
}
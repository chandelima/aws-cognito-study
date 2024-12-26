using FluentMigrator;

namespace ToDoApp.Infra.Migrations;

[Migration(000_001)]
public class _000001_CreateToDoTable : Migration
{
    private string _tableName = "todos";

    public override void Up()
    {
        if (!Schema.Table(_tableName).Exists())
        {
            Create
                .Table(_tableName)
                .WithColumn("id").AsString(26).PrimaryKey().NotNullable()
                .WithColumn("description").AsString(100).NotNullable()
                .WithColumn("creation_date").AsString(25).NotNullable()
                .WithColumn("completion_date").AsString(25).Nullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(_tableName).Exists())
        {
            Delete.Table(_tableName);
        }
    }
}

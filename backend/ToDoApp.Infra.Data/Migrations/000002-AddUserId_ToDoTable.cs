using FluentMigrator;

namespace ToDoApp.Infra.Data.Migrations;

[Migration(000_002)]
public class _000002_AddUserIdToDoTable : Migration
{
    private string _tableName = "todos";
    private string _columnName = "user_id";

    public override void Up()
    {
        if (Schema.Table(_tableName).Exists())
        {
            Alter
                .Table(_tableName)
                .AddColumn(_columnName).AsString(36).NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(_tableName).Exists())
        {
            Delete
                .Column(_columnName)
                .FromTable(_tableName);
        }
    }
}

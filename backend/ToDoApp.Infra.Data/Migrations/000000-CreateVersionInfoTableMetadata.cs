using FluentMigrator.Runner.VersionTableInfo;

namespace ToDoApp.Infra.Data.Migrations;

public class CreateTabelaVersionInfoMetadata : DefaultVersionTableMetaData
{
    public override string TableName => "__migrations_metadata";
}

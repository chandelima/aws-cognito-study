using FluentMigrator.Runner.VersionTableInfo;

namespace ToDoApp.Infra.Migrations;

public class CreateTabelaVersionInfoMetadata : DefaultVersionTableMetaData
{
    public override string TableName => "__migrations_metadata";
}

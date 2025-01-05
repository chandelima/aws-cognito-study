using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infra.Data.EntitiesConfiguration;
internal class ToDoEntityConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("todos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").HasColumnType("TEXT").IsRequired(true);
        builder.Property(x => x.Description).HasColumnName("description").HasColumnType("TEXT").IsRequired(true);
        builder.Property(x => x.CreationDate).HasColumnName("creation_date").HasColumnType("TEXT").IsRequired(true);
        builder.Property(x => x.CompletionDate).HasColumnName("completion_date").HasColumnType("TEXT");
        builder.Property(x => x.UserId).HasColumnName("user_id").HasColumnType("TEXT");
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoAssignment.Models.Todos.Entity;

namespace ToDoAssignment.Repository.ToDos.Configuration;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos").HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("To_Do_Id");
        builder.Property(t => t.Title).HasColumnName("Title");
        builder.Property(t => t.Description).HasColumnName("Description");
        builder.Property(t => t.TimeCreated).HasColumnName("Time_Created");
        builder.Property(t => t.StartDate).HasColumnName("Date_Started");
        builder.Property(t => t.EndDate).HasColumnName("Date_Ended");
        builder.Property(t => t.TimeUpdated).HasColumnName("Time_Updated");
        builder.Property(t => t.Priority).HasColumnName("Priority");
        builder.Property(t => t.Completed).HasColumnName("Completed");
        builder.Property(t => t.CategoryId).HasColumnName("Category_Id");
        builder.Property(t => t.UserId).HasColumnName("User_Id");

        builder.HasOne(t => t.Category).WithMany(c => c.ToDos).HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.User).WithMany(u => u.ToDos).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(t => t.Category).AutoInclude();
        builder.Navigation(t => t.User).AutoInclude();
    }


}

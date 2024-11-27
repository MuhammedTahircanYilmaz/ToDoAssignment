using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoAssignment.Models.Categories.Entity;

namespace ToDoAssignment.Repository.Configurations.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Category_Id");
        builder.Property(c => c.UserId).HasColumnName("User_Id");
        builder.Property(c => c.Name).HasColumnName("Category_Name");
        builder.Property(c => c.TimeCreated).HasColumnName("Time_Created");
        builder.Property(c => c.TimeUpdated).HasColumnName("Time_Updated");

        builder.HasMany(c => c.ToDos).WithOne(t => t.Category).HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.User).WithMany(u => u.Categories).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(t => t.User).AutoInclude();
        builder.Navigation(c=>c.ToDos).AutoInclude();


    }
}

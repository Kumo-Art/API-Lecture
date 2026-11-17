//the base entity: the columns Every table shares, so we only have to write them once
namespace api.Models
{
    public class BaseEntity
    {
        //EF Core treeats a property named Id as the primary key.
        //The DB fills it in for use when a new row is saved / created.
       public int Id { get; set; }
       //DateTime.UtcNow is the current time in UTC, so every server agrees on the time.

       public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
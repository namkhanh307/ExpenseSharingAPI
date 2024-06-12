using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities;

public abstract class BaseEntity{
    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedTime = LastUpdatedTime = DateTime.Now;
    }

    [Key]
    public string Id { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime LastUpdatedTime { get; set; }
    public DateTime? DeletedTime { get; set; }

}
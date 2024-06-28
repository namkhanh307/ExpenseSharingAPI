using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace Repositories.Entities;

public class Item : BaseEntity
{
    public string? Name {get; set;}
    public required double Price {get; set;}
    public string? Image{get; set;}
    public required string PersonId {get; set;}
    [ForeignKey("PersonID")]
    public virtual required Person Person {get; set;}
    public required string ReportId {get; set;}
    [ForeignKey("ReportID")]
    public virtual required Report Report {get; set;}
}
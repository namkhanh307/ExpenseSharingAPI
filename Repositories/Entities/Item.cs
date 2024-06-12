using System.ComponentModel.Design;

namespace Repositories.Entities;

public class Item : BaseEntity
{
    public string? Name {get; set;}
    public required double Price {get; set;}
    public string? Image{get; set;}
    public required string PersonId {get; set;}
    public virtual required Person Person {get; set;}
    public required string ReportId {get; set;}
    public virtual required Report Report {get; set;}
}
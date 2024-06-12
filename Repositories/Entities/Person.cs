using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities;

public class Person
{
    [Key]
    public required string Id {get; set;}
    public required string Name {get; set;}
    public required string Phone {get; set;}
    public required string Password {get; set;}
}
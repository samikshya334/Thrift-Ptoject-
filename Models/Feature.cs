// Feature.cs
using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;

public class Feature
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Value { get; set; }

 
    public int ProductId { get; set; }


    public Product Product { get; set; }
}

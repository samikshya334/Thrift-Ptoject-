using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Thrift_Us.Models;

public class Like
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    [Required]
    public int ProductId { get; set; }
}

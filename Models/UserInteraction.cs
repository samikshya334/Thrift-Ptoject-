
using System.ComponentModel.DataAnnotations;

public class UserInteraction
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public int ProductId { get; set; }

}

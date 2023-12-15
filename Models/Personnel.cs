using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeaBaseAPI;

public class Personnel
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;
    public Department Department { get; set; }
    [DisplayName("Is Deployed?")]
    public bool IsDeployed { get; set; } = false;
}

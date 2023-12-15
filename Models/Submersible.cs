using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeaBaseAPI;

public class Submersible
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name cannot be blank.")]
    public string VesselName { get; set; } = default!;
    [DisplayName("Is In Use?")]
    public bool IsDeployed { get; set; } = false;
    public Personnel Pilot { get; set; } = null!;
    public ICollection<Personnel> Crew { get; set; } = null!;
    public double Condition { get; set; }
}

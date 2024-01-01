using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SeaBaseAPI;

public sealed class SubmersibleDto
{
    [Required(ErrorMessage = "Name cannot be blank.")]
    public string VesselName { get; set; } = default!;
    [JsonPropertyName("Is Deployed?")]
    public bool IsDeployed { get; set; } = false;
    public Personnel? Pilot { get; set; }
    public ICollection<Personnel>? Crew { get; set; }
    [Range(0, 1)]
    public double Condition { get; set; } = 1.0;
}

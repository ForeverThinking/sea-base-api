using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SeaBaseAPI;

public sealed record PersonnelDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; } = default!;
    public Department Department { get; set; }
    [JsonPropertyName("Is Deployed?")]
    public bool IsDeployed { get; init; } = false;
}

using System.ComponentModel.DataAnnotations;

namespace SeaBaseAPI;

public sealed class Personnel
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; } = default!;
    public Department Department { get; set; }
    public bool IsDeployed { get; set; } = false;
}

using System.ComponentModel.DataAnnotations;

namespace SeaBaseAPI;

public sealed class Personnel
{
    [Key]
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public Department Department { get; init; }
    public bool IsDeployed { get; init; } = false;
}

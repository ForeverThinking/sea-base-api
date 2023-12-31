﻿using System.ComponentModel.DataAnnotations;

namespace SeaBaseAPI;

public sealed class Submersible
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name cannot be blank.")]
    public string VesselName { get; set; } = default!;
    public bool IsDeployed { get; set; } = false;
    public Personnel? Pilot { get; set; }
    public ICollection<Personnel>? Crew { get; set; }
    [Range(0, 1)]
    public double Condition { get; set; }
}

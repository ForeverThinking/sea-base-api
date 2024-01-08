using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public interface ISubmersibleService
{
    public Task AddSubmersibleAsync(SubmersibleDto dto);
    public Task<ICollection<SubmersibleDto>> GetAllSubmersiblesAsync();
}

public sealed class SubmersibleService : ISubmersibleService
{
    private readonly SeaBaseContext _context;

    public SubmersibleService(SeaBaseContext context)
    {
        _context = context;
    }

    public async Task AddSubmersibleAsync(SubmersibleDto dto)
    {
        var submersible = new Submersible
        {
            VesselName = dto.VesselName,
            IsDeployed = dto.IsDeployed,
            Pilot = dto.Pilot,
            Crew = dto.Crew,
            Condition = dto.Condition
        };

        await _context.Submersibles.AddAsync(submersible);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<SubmersibleDto>> GetAllSubmersiblesAsync()
        => await _context.Submersibles.Select(s => new SubmersibleDto
        {
            VesselName = s.VesselName,
            IsDeployed = s.IsDeployed,
            Pilot = s.Pilot,
            Crew = s.Crew,
            Condition = s.Condition
        })
        .ToListAsync();
}

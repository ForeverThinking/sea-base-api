using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public interface ISubmersibleService
{
    public Task AddSubmersibleAsync(SubmersibleDto dto);
    public Task<ICollection<SubmersibleDto>> GetAllSubmersiblesAsync();
    public Task<SubmersibleDto?> GetSingleSubmersibleAsync(int id);
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
            Pilot = dto.Pilot,
            Crew = dto.Crew,
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

    public async Task<SubmersibleDto?> GetSingleSubmersibleAsync(int id)
    {
        var result = await _context.Submersibles.FindAsync(id);

        if (result is not null)
        {
            return new SubmersibleDto
            {
                VesselName = result.VesselName,
                Pilot = result.Pilot,
                Crew = result.Crew,
                IsDeployed = result.IsDeployed,
                Condition = result.Condition
            };
        }

        return null;
    }
}

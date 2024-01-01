namespace SeaBaseAPI;

public interface ISubmersibleService
{
    public Task AddSubmersibleAsync(SubmersibleDto dto);
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
}

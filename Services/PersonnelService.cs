using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public interface IPersonnelService
{
    public Task<IEnumerable<PersonnelDto>> GetAllPersonnelAsync();
}

public sealed class PersonnelService : IPersonnelService
{
    private readonly SeaBaseContext _context;

    public PersonnelService(SeaBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PersonnelDto>> GetAllPersonnelAsync()
        => await _context.Personnel.Select(person => new PersonnelDto 
        { 
            Name = person.Name, 
            Department = person.Department, 
            IsDeployed = person.IsDeployed 
        })
        .ToArrayAsync();
}

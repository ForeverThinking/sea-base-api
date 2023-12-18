using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public interface IPersonnelService
{
    public Task<IEnumerable<PersonnelDto>> GetAllPersonnelAsync();
    public Task AddPersonnelAsync(PersonnelDto dto);
    public Task<bool> DeletePersonnelAsync(int id);
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

    public async Task AddPersonnelAsync(PersonnelDto dto)
    {
        var personnel = new Personnel()
        {
            Name = dto.Name,
            Department = dto.Department,
            IsDeployed = dto.IsDeployed
        };

        await _context.Personnel.AddAsync(personnel);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeletePersonnelAsync(int id)
    {
        var staff = _context.Personnel.Find(id);

        if (staff is not null)
        {
            _context.Personnel.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}

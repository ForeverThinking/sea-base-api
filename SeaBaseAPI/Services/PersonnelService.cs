using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public interface IPersonnelService
{
    public Task<IEnumerable<PersonnelDto>> GetAllPersonnelAsync();
    public Task AddPersonnelAsync(PersonnelDto dto);
    public Task<bool> DeletePersonnelAsync(int id);
    public Task<PersonnelDto?> GetPersonAsync(int id);
    public Task<bool> UpdatePersonnelAsync(int id, PersonnelDto dto);
}

public sealed class PersonnelService : IPersonnelService
{
    private readonly SeaBaseContext _context;

    public PersonnelService(SeaBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PersonnelDto>> GetAllPersonnelAsync()
        => await _context.Personnel.OrderBy(person => person.Id)
        .Select(person => new PersonnelDto
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

    public async Task<PersonnelDto?> GetPersonAsync(int id)
    {
        var staff = await _context.Personnel.FindAsync(id);
        
        if (staff is not null)
        {
            PersonnelDto person = new()
            {
                Name = staff.Name,
                Department = staff.Department,
                IsDeployed = staff.IsDeployed
            };

            return person;
        }

        return null;
    }

    public async Task<bool> UpdatePersonnelAsync(int id, PersonnelDto dto)
    {
        var currentPersonnel = await _context.Personnel.FindAsync(id);

        if (currentPersonnel is null)
        {
            return false;
        }

        currentPersonnel.Name = dto.Name;
        currentPersonnel.Department = dto.Department;
        currentPersonnel.IsDeployed = dto.IsDeployed;

        try
        {
            _context.Personnel.Update(currentPersonnel);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }
}

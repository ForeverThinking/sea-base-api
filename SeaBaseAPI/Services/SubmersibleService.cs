﻿using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public interface ISubmersibleService
{
    public Task<Submersible> AddSubmersibleAsync(SubmersibleDto dto);
    Task<bool> DeleteSubmersibleAsync(int id);
    public Task<ICollection<SubmersibleDto>> GetAllSubmersiblesAsync();
    public Task<SubmersibleDto?> GetSingleSubmersibleAsync(int id);
    public Task<bool> UpdateSubmersibleAsync(int id, SubmersibleDto dto);
}

public sealed class SubmersibleService : ISubmersibleService
{
    private readonly SeaBaseContext _context;

    public SubmersibleService(SeaBaseContext context)
    {
        _context = context;
    }

    public async Task<Submersible> AddSubmersibleAsync(SubmersibleDto dto)
    {
        var submersible = new Submersible
        {
            VesselName = dto.VesselName,
            Pilot = dto.Pilot,
            Crew = dto.Crew,
        };

        await _context.Submersibles.AddAsync(submersible);
        await _context.SaveChangesAsync();
        return await _context.Submersibles.OrderBy(s => s.Id).LastAsync();
    }

    public async Task<bool> DeleteSubmersibleAsync(int id)
    {
        var sub = await _context.Submersibles.FindAsync(id);

        if (sub is null)
        {
            return false;
        }

        _context.Remove(sub);
        await _context.SaveChangesAsync();
        return true;
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

    public async Task<bool> UpdateSubmersibleAsync(int id, SubmersibleDto dto)
    {
        var currentSubmersible = await _context.Submersibles.FindAsync(id);

        if (currentSubmersible is null)
        {
            return false;
        }

        currentSubmersible.VesselName = dto.VesselName;

        try
        {
            _context.Submersibles.Update(currentSubmersible);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }
}

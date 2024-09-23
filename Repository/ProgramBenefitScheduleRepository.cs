using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Interface;
using System;

namespace Repository;

public class ProgramBenefitScheduleRepository : IProgramBenefitScheduleRepository
{
    private readonly RepositoryContext _context;

    public ProgramBenefitScheduleRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync()
    {
        return await _context.programBenefitSchedule.ToListAsync();
    }

    public async Task<ProgramBenefitSchedule> GetByProgramCodeAndMBSItemCodeAsync(string programCode, string mbsItemCode)
    {
        return await _context.programBenefitSchedule.FirstOrDefaultAsync(x => x.ProgramCode == programCode && x.MBSItemCode == mbsItemCode);
    }

    public async Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule)
    {
        _context.programBenefitSchedule.Add(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<ProgramBenefitSchedule> UpdateAsync(string programCode, string mbsItemCode, ProgramBenefitSchedule schedule)
    {
         ProgramBenefitSchedule oldValues = _context.programBenefitSchedule.Single(x => x.ProgramCode == programCode && x.MBSItemCode == mbsItemCode);
        if (oldValues is not null)
        {
            oldValues.MBSScheduleFees = schedule.MBSScheduleFees;
            oldValues.ProgramMedicalFees = schedule.ProgramMedicalFees;
            oldValues.ChangedBy = schedule.ChangedBy;
            oldValues.ChangedDateTime = DateTime.Now;
            oldValues.Comments = schedule.Comments;
            _context.programBenefitSchedule.Update(oldValues);
            //_context.Entry(oldValues).CurrentValues.SetValues(schedule);

            //_context.Entry(schedule).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    _context.SaveChanges();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is ProgramBenefitSchedule)
                        {
                            var proposedValues = entry.CurrentValues;
                           PropertyValues databaseValues = await entry.GetDatabaseValuesAsync();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues?[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
        }
        return schedule;
    }

    public async Task<bool> DeleteAsync(string programCode, string mbsItemCode)
    {
        var schedule = await _context.programBenefitSchedule.FirstOrDefaultAsync(x => x.ProgramCode == programCode && x.MBSItemCode == mbsItemCode);

        if (schedule == null) return false;

        _context.programBenefitSchedule.Remove(schedule);
        await _context.SaveChangesAsync();
        return true;
    }
}

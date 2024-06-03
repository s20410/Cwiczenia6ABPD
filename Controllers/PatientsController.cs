using ABPDCwiczenia6.Models;

namespace ABPDCwiczenia6.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PacjenciController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PacjenciController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> PobierzDanePacjenta(int id)
    {
        var pacjent = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(r => r.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(r => r.PrescriptionsMedicines)
            .ThenInclude(rl => rl.Medicine)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (pacjent == null)
        {
            return NotFound();
        }

        var wynik = new
        {
            pacjent.IdPatient,
            pacjent.Name,
            pacjent.Surename,
            Recepty = pacjent.Prescriptions.Select(r => new
            {
                r.IdPrescriptions,
                r.Date,
                r.ValidityTerm,
                Doctor = new
                {
                    r.Doctor.IdDoctor,
                    r.Doctor.Name,
                    r.Doctor.Surename
                },
                Leki = r.PrescriptionsMedicines.Select(rl => new
                {
                    rl.Medicine.IdMedicine,
                    rl.Medicine.MedicineName,
                    rl.Dose,
                    rl.Description
                })
            }).OrderBy(r => r.ValidityTerm)
        };

        return Ok(wynik);
    }
}

using ABPDCwiczenia6.DTO;

namespace ABPDCwiczenia6.Controllers;
using ABPDCwiczenia6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PrescriptionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> DodajRecepte([FromBody] PrescriptionsDto prescriptionsDto)
    {
        var pacjent = await _context.Patients.FindAsync(prescriptionsDto.Patient.IdPatient);
        if (pacjent == null)
        {
            pacjent = new Patient
            {
                Name = prescriptionsDto.Patient.Name,
                Surename = prescriptionsDto.Patient.Surename
            };
            _context.Patients.Add(pacjent);
        }

        var lekarz = await _context.Doctors.FindAsync(prescriptionsDto.Doctor.IdDoctor);
        if (lekarz == null)
        {
            return BadRequest("Lekarz nie istnieje.");
        }

        var recepta = new Prescriptions
        {
            Date = prescriptionsDto.Date,
            ValidityTerm = prescriptionsDto.ValidityTerm,
            Patient = pacjent,
            Doctor = lekarz
        };

        if (prescriptionsDto.Medicine.Count > 10)
        {
            return BadRequest("Recepta nie może zawierać więcej niż 10 leków.");
        }

        foreach (var lekDto in prescriptionsDto.Medicine)
        {
            var lek = await _context.Medicines.FindAsync(lekDto.IdMedicine);
            if (lek == null)
            {
                return BadRequest($"Lek o Id {lek.IdMedicine} nie istnieje.");
            }

            var receptaLek = new PrescriptionsMedicine
            {
                Prescriptions = recepta,
                Medicine = lek,
                Dose = lekDto.Dose,
                Description = lekDto.Description
            };

            _context.PrescriptionsMedicines.Add(receptaLek);
        }

        _context.Prescriptions.Add(recepta);
        await _context.SaveChangesAsync();

        return Ok(recepta);
    }
}

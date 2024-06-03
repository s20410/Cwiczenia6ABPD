using ABPDCwiczenia6.DTO;
using ABPDCwiczenia6.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ABPDCwiczenia6.Controllers;

public class PrescriptionsControllerTests
{
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly PrescriptionsController _controller;

    public PrescriptionsControllerTests()
    {
        _mockContext = new Mock<ApplicationDbContext>();
        _controller = new PrescriptionsController(_mockContext.Object);
    }

    [Fact]
    public async Task DodajRecepte_ShouldAddNewPrescription_WhenDataIsValid()
    {
        // Arrange
        var receptaDto = new PrescriptionsDto
        {
            Date = DateTime.Now,
            ValidityTerm = DateTime.Now.AddDays(1),
            Patient = new PatientDto { IdPatient = 1, Name = "Jan", Surename = "Kowalski" },
            Doctor = new DoctorDto { IdDoctor = 1 },
            Medicine = new List<MedicineDto>
            {
                new MedicineDto { IdMedicine = 1, Dose = 10, Description = "Test" }
            }
        };

        _mockContext.Setup(m => m.Patients.FindAsync(It.IsAny<int>())).ReturnsAsync(new Patient { IdPatient = 1 });
        _mockContext.Setup(m => m.Doctors.FindAsync(It.IsAny<int>())).ReturnsAsync(new Doctor { IdDoctor = 1 });
        _mockContext.Setup(m => m.Medicines.FindAsync(It.IsAny<int>())).ReturnsAsync(new Medicine { IdMedicine = 1 });

        // Act
        var result = await _controller.DodajRecepte(receptaDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var recepta = Assert.IsType<Prescriptions>(okResult.Value);
        Assert.Equal(1, recepta.Patient.IdPatient);
        Assert.Single(recepta.PrescriptionsMedicines);
    }
}

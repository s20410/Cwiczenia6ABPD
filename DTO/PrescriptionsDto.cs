namespace ABPDCwiczenia6.DTO;

public class PrescriptionsDto
{
    public DateTime Date { get; set; }
    public DateTime ValidityTerm { get; set; }
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<MedicineDto> Medicine { get; set; }
}
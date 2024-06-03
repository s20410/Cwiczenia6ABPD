namespace ABPDCwiczenia6.Models;

public class Prescriptions
{
    public int IdPrescriptions { get; set; }
    public DateTime Date { get; set; }
    public DateTime ValidityTerm { get; set; }
    public int IdPatient { get; set; }
    public Patient Patient { get; set; }
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<PrescriptionsMedicine> PrescriptionsMedicines { get; set; }
}
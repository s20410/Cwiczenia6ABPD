namespace ABPDCwiczenia6.Models;

public class Medicine
{
    public int IdMedicine { get; set; }
    public string MedicineName { get; set; }
    public ICollection<PrescriptionsMedicine> PrescriptionsMedicines { get; set; }
}
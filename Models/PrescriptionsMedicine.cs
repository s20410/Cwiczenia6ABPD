namespace ABPDCwiczenia6.Models;

public class PrescriptionsMedicine
{
    public int IdPrescriptions { get; set; }
    public Prescriptions Prescriptions { get; set; }
    public int IdMedicine { get; set; }
    public Medicine Medicine { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}
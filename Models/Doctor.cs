namespace ABPDCwiczenia6.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string Name { get; set; }
    public string Surename { get; set; }
    public ICollection<Prescriptions> Prescriptions { get; set; }
}
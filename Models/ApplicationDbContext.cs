namespace ABPDCwiczenia6.Models;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescriptions> Prescriptions { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<PrescriptionsMedicine> PrescriptionsMedicines { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionsMedicine>()
            .HasKey(rl => new
            {
                rl.IdPrescriptions, rl.IdMedicine
            });

        modelBuilder.Entity<PrescriptionsMedicine>()
            .HasOne(rl => rl.Prescriptions)
            .WithMany(r => r.PrescriptionsMedicines)
            .HasForeignKey(rl => rl.IdPrescriptions);

        modelBuilder.Entity<PrescriptionsMedicine>()
            .HasOne(rl => rl.Medicine)
            .WithMany(l => l.PrescriptionsMedicines)
            .HasForeignKey(rl => rl.IdMedicine);
    }
}

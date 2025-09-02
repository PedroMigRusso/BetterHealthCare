using BetterHealthCareAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace BetterHealthCareAPI.Infrastructure
{
    public class BetterHealthCareDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAction> PatientActions { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<MedicalFile> MedicalFiles { get; set; }
        public BetterHealthCareDbContext(DbContextOptions<BetterHealthCareDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<List<int>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions)null));

            var comparer = new ValueComparer<List<int>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            modelBuilder.Entity<PatientAction>()
                .Property(p => p.FilesId)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Actions)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);
        }
    }
}

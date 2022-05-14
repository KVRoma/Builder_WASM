using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;


namespace Builder_WASM.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<UserRegistered> UserRegistereds { get; set; } = null!;
        public DbSet<Company> Companies  { get; set; } = null!;
        public DbSet<ClientJob> ClientJobs { get; set; } = null!;
        public DbSet<Estimate> Estimates { get; set; } = null!;
        public DbSet<EstimateLine> EstimateLines { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;



        //*************** Dictionary******************************
        public DbSet<DGroupe> DGroupes { get; set; } = null!;
        public DbSet<DItem> DItems { get; set; } = null!;
        public DbSet<DDescription> DDescriptions { get; set; } = null!;
        public DbSet<DSupplier> DSuppliers { get; set; } = null!;
        public DbSet<DMethodPayment> DMethodPayments { get; set; } = null!;
        public DbSet<DContractor> DContractors { get; set; } = null!;
        //*********************************************************

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            UsersSeed(builder);
        }

        private static void UsersSeed(ModelBuilder builder)
        {
            builder.Entity<UserRegistered>()
                .HasData(
                    new UserRegistered
                    {
                        Id = 1,                        
                        Name = "Test",                         
                        Password = "123",
                        Role = "Admin"
                    },
                    new UserRegistered
                    {
                        Id = 2,
                        Name = "Test2",                       
                        Password = "123",
                        Role = "User",
                    });
        }
    }
}

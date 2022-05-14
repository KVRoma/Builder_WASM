using Builder_WASM.Server.Data;
using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Entities.Dictionary;

namespace Builder_WASM.Server.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext context;

        private Repository<UserRegistered> userRegisteredRepository = null!;
        private Repository<Company> companyRepository = null!;
        private Repository<ClientJob> clientJobRepository = null!;
        private Repository<Estimate> estimateRepository = null!;
        private Repository<EstimateLine> estimateLineRepository = null!;
        private Repository<Payment> paymentRepository = null!;



        private Repository<DGroupe> dgroupeRepository = null!;
        private Repository<DItem> ditemRepository = null!;
        private Repository<DDescription> ddescriptionRepository = null!;
        private Repository<DSupplier> dsupplierRepository = null!;
        private Repository<DMethodPayment> dmethodPaymentRepository = null!;
        private Repository<DContractor> dcontractorRepository = null!;

        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }


        
        public Repository<UserRegistered> UserRegisteredRepository
        {
            get
            {
                if (this.userRegisteredRepository == null)
                {
                    this.userRegisteredRepository = new Repository<UserRegistered>(context);
                }
                return this.userRegisteredRepository;
            }
        }
        public Repository<Company> CompanyRepository
        {
            get
            {
                if (this.companyRepository == null)
                {
                    this.companyRepository = new Repository<Company>(context);
                }
                return companyRepository;
            }
        }
        public Repository<ClientJob> ClientJobRepository
        {
            get
            {

                if (this.clientJobRepository == null)
                {
                    this.clientJobRepository = new Repository<ClientJob>(context);
                }
                return clientJobRepository;
            }
        }
        public Repository<Estimate> EstimateRepository
        {
            get
            {

                if (this.estimateRepository == null)
                {
                    this.estimateRepository = new Repository<Estimate>(context);
                }
                return estimateRepository;
            }
        }
        public Repository<EstimateLine> EstimateLineRepository
        {
            get
            {

                if (this.estimateLineRepository == null)
                {
                    this.estimateLineRepository = new Repository<EstimateLine>(context);
                }
                return estimateLineRepository;
            }
        }
        public Repository<Payment> PaymentRepository
        {
            get
            {
                if (this.paymentRepository == null)
                {
                    this.paymentRepository = new Repository<Payment>(context);
                }
                return paymentRepository;
            }
        }



        public Repository<DGroupe> DGroupeRepository
        {
            get
            {

                if (this.dgroupeRepository == null)
                {
                    this.dgroupeRepository = new Repository<DGroupe>(context);
                }
                return dgroupeRepository;
            }
        }
        public Repository<DItem> DItemRepository
        {
            get
            {

                if (this.ditemRepository == null)
                {
                    this.ditemRepository = new Repository<DItem>(context);
                }
                return ditemRepository;
            }
        }
        public Repository<DDescription> DDescriptionRepository
        {
            get
            {

                if (this.ddescriptionRepository == null)
                {
                    this.ddescriptionRepository = new Repository<DDescription>(context);
                }
                return ddescriptionRepository;
            }
        }
        public Repository<DSupplier> DSupplierRepository
        {
            get
            {

                if (this.dsupplierRepository == null)
                {
                    this.dsupplierRepository = new Repository<DSupplier>(context);
                }
                return dsupplierRepository;
            }
        }
        public Repository<DMethodPayment> DMethodPaymentRepository
        {
            get
            {
                if (this.dmethodPaymentRepository == null)
                {
                    this.dmethodPaymentRepository = new Repository<DMethodPayment>(context);
                }
                return dmethodPaymentRepository;
            }
        }
        public Repository<DContractor> DContractorRepository
        {
            get
            {
                if (this.dcontractorRepository == null)
                {
                    this.dcontractorRepository = new Repository<DContractor>(context);
                }
                return dcontractorRepository;
            }
        }



        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

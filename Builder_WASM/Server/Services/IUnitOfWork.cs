using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Entities.Dictionary;

namespace Builder_WASM.Server.Services
{
    public interface IUnitOfWork
    {
        Repository<UserRegistered> UserRegisteredRepository { get; }
        Repository<UserMessage> UserMessageRepository { get; }
        Repository<Company> CompanyRepository { get; }
        Repository<ClientJob> ClientJobRepository { get; }
        Repository<Estimate> EstimateRepository { get; }
        Repository<EstimateLine> EstimateLineRepository { get; }
        Repository<Payment> PaymentRepository { get; }

       

        Repository<DGroupe> DGroupeRepository { get; }
        Repository<DItem> DItemRepository { get; }
        Repository<DDescription> DDescriptionRepository { get; }
        Repository<DSupplier> DSupplierRepository { get; }
        Repository<DMethodPayment> DMethodPaymentRepository { get; }
        Repository<DContractor> DContractorRepository { get; }

        Task SaveAsync();
        void Dispose();
    }
}

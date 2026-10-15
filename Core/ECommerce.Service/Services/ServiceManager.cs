using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Contratcs.UOW;

namespace ECommerce.Service.Services
{

    // Both of two ways are true.
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , IBasketRepository basketRepository) : IServiceManager
    {

        private readonly Lazy<IProductServices> LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
        public IProductServices ProductServices => LazyProductServices.Value;

        private readonly Lazy<IBasketService> LazyBasketService = new Lazy<IBasketService>(() => new BasketServices (basketRepository, mapper));
        public IBasketService BasketServices => LazyBasketService.Value;
    }

    //public class ServiceManager : IServiceManager
    //{
    //    private readonly IUnitOfWork unitOfWork;
    //    private readonly IMapper mapper;
    //    private readonly Lazy<IProductServices> LazyProductServices;

    //    public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , Lazy<IProductServices> LazyProductServices)
    //    {
    //        this.unitOfWork = unitOfWork;
    //        this.mapper = mapper;
    //        this.LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
    //    }

   

    //    public IProductServices ProductServices => LazyProductServices.Value;
    //}
}

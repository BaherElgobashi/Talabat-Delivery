using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{

    // Both of two ways are true.
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {

        private readonly Lazy<IProductServices> LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
        public IProductServices ProductServices => LazyProductServices.Value;
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

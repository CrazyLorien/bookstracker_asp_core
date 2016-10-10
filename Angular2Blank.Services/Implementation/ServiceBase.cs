using Angular2Blank.Data.Entities;
using Angular2Blank.Data.Repository;
using Angular2Blank.Services.Interfaces;

namespace Angular2Blank.Services.Implementation
{
    public class ServiceBase<T>: IServiceBase<T>  where T: BaseEntity
    {
        protected readonly IRepository<T> Repository;

        public ServiceBase(IRepository<T> repository)
        {
            Repository = repository;
        }
    }
}

using Aloblog.Domain.Common;

namespace Aloblog.Application.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
}
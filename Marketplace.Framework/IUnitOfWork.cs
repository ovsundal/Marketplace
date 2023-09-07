namespace Marketplace;

public interface IUnitOfWork
{
    Task Commit();
}
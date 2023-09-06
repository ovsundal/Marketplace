namespace Marketplace.Domain;

public interface ICustomerCreditService
{
    Task<bool> EnsureEnoughCredit(int customerId, decimal amount);
}
using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    public interface IPropertyRepository : IElasticsearchRepository<Property, Guid>
    {
        Task<IEnumerable<Property>> GetByTextAsync(string text, int limit);
    }
}
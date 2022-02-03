using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    public interface IPropertyRepository : IElasticsearchRepository<Property, Guid>
    {
        Task<IEnumerable<Property>> SearchPropertyAsync(string text,IEnumerable<string> marketKey, int limit);
    }
}
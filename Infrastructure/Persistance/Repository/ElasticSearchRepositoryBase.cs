using Domain.Common;
using Domain.Interfaces.Repository;
using Elasticsearch.Net;
using Infrastructure.Persistance.ElasticsearchContext;
using Nest;

namespace Infrastructure.Persistance.Repository
{
    public abstract class ElasticSearchRepositoryBase<TDocument, TPrimaryKey>
         : IElasticsearchRepository<TDocument, TPrimaryKey>
         where TDocument : class, IMessage<TPrimaryKey>
         where TPrimaryKey : struct
    {
        protected readonly IElasticContextProvider _context;
        protected readonly string IndexName;

        protected ElasticSearchRepositoryBase(IElasticContextProvider context,
            string indexName)
        {
            _context = context;
            IndexName = indexName;
        }

        public Task<IEnumerable<TDocument>> GetAllAsync() => Get();

 


        private TDocument Index(TDocument entity)
        {
            var response = _context.GetClient()
                .Index(entity, idx => idx.Index(IndexName)
                .Id(new Id(entity.Id.ToString()))
                .Refresh(Refresh.WaitFor));

            return entity;
        }

       

        private async Task<IEnumerable<TDocument>> Get()
        {
            var query = await _context
                .GetClient()
                .SearchAsync<TDocument>(idx => idx
                    .Index(IndexName)
                    .Size(1000)
                );
            return query.Documents;
        }

        public TDocument Add(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> AddAsync(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public TDocument AddOrUpdate(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> AddOrUpdateAsync(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> RemoveAsync(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> RemoveAsync(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public TDocument GetById(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByIdAsync(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }
    }
}
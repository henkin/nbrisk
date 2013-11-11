using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using TemplateLibrary.Events;
using TemplateLibrary.Repo;

namespace TemplateLibrary.Repository
{
    public class MongoRepository : MongoBase, IRepository, IService
    {
        public IEventService EventService { get; set; }

        public T GetByID<T>(Guid id) where T : class, IIdentifiable
        {
            var collection = GetCollection<T>();
            var entity = collection.FindOneAs<T>(Query.EQ("_id", id));
            return entity;
        }
        
        public IQueryable<T> FindByOwnerID<T>(Guid ownerID) where T : class, IIdentifiable, IOwnable
        {
            var collection = GetCollection<T>();
            return collection.AsQueryable<T>().Where(t => t.OwnerID == ownerID);
        }

        public IQueryable<T> Repo<T>() where T : class, IIdentifiable
        {
            var collection = GetCollection<T>();
            return collection.AsQueryable<T>();
        }

        public void Dispose() { }



    }
}
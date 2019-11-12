using Tivit.WebApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Tivit.WebApi.Settings;
using System.Threading.Tasks;

namespace Tivit.WebApi.Services
{
    public class DeviceService
    {
        private readonly IMongoCollection<Device> _document;

        public DeviceService(IDevicestoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _document = database.GetCollection<Device>(settings.DeviceCollectionName);
        }

        public Task<List<Device>> Get() =>
            _document.Find(doc => true).ToListAsync();

        public Task<Device> Get(string id) =>
            _document.Find<Device>(doc => doc.Id == id).FirstOrDefaultAsync();

        public Device Create(Device doc)
        {
            _document.InsertOne(doc);
            return doc;
        }

        public void Update(string id, Device docIn) =>
            _document.ReplaceOne(doc => doc.Id == id, docIn);

        public void Remove(Device docIn) =>
            _document.DeleteOne(doc => doc.Id == docIn.Id);

        public void Remove(string id) => 
            _document.DeleteOne(doc => doc.Id == id);
    }
}
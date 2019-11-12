using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tivit.WebApi.Models;
using Tivit.WebApi.Settings;

namespace Tivit.WebApi.Services
{
    public class DataOfDeviceService
    {
        private readonly IMongoCollection<DataOfDevice> _document;

        public DataOfDeviceService(IDevicestoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _document = database.GetCollection<DataOfDevice>(settings.DataOfDeviceCollectionName);
        }

        public Task<List<DataOfDevice>> Get(System.DateTime? dateIni = null, System.DateTime? dateEnd = null)
        {
            if(dateIni.HasValue && !dateEnd.HasValue)
                return _document.Find(doc => doc.CreationDate.Date >= dateIni.Value.Date).ToListAsync();

            if(!dateIni.HasValue && dateEnd.HasValue)
                return _document.Find(doc => doc.CreationDate.Date <= dateEnd.Value.Date).ToListAsync();

            if(dateIni.HasValue && dateEnd.HasValue)
                return _document.Find(doc => doc.CreationDate >= dateIni.Value && doc.CreationDate <= dateEnd.Value).ToListAsync();


            return _document.Find(doc => true).ToListAsync();
        }

        public Task<DataOfDevice> Get(string id) =>
            _document.Find<DataOfDevice>(doc => doc.Id == id).FirstOrDefaultAsync();

        public Task<List<DataOfDevice>> GetByDeviceId(string id) =>
            _document.Find<DataOfDevice>(doc => doc.Device.Id == id).ToListAsync();

        public DataOfDevice Create(DataOfDevice doc)
        {
            _document.InsertOne(doc);
            return doc;
        }

        public void Update(string id, DataOfDevice docIn) =>
            _document.ReplaceOne(doc => doc.Id == id, docIn);

        public void Remove(DataOfDevice docIn) =>
            _document.DeleteOne(doc => doc.Id == docIn.Id);

        public void Remove(string id) =>
            _document.DeleteOne(doc => doc.Id == id);
    }
}

using Drivers.Api.Models;
using MongoDB.Driver;

using Drivers.Api.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Drivers.Api.Services;
public class DriverServices
{
    private readonly IMongoCollection<Divers> _driversCollection;

    public DriverServices(
        IOptions<DatabaseSettings> databaseSettings)
    {
           //Inicializar mi cliente de MongoDB
          var mongoClient =new MongoClient(databaseSettings.Value.ConnectionString);
          //Conectar a la base de datos de MONGODB
           var mongoDB=
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName); 
            _driversCollection =
            mongoDB.GetCollection<Divers>
            (databaseSettings.Value.CollectionName);
    }
    
    public async Task<List<Divers>> GetAsync()=>
    await _driversCollection.Find(_=> true).ToListAsync();
    //Insertar
    public async Task InsertDriver(Divers driver)
    {
        await _driversCollection.InsertOneAsync(driver);
    }

    //Buscar por ID
    public async Task<Divers> GetDriverById(string id)
    {
        return await _driversCollection.FindAsync(new BsonDocument 
        {{"_id", new ObjectId(id)}}).Result.FirstAsync();
    }
    //Actualizar
     public async Task UpdateDriver(Divers driver)
    {
        var filter= Builders<Divers>.Filter.Eq(s=>s.Id, driver.Id);
        
        await _driversCollection.ReplaceOneAsync(filter, driver);
    }
    //Eliminar
     public async Task DeleteDriver(string id)
    {
        var filter= Builders<Divers>.Filter.Eq(s=>s.Id, id);
        await _driversCollection.DeleteOneAsync(filter);
    }

}
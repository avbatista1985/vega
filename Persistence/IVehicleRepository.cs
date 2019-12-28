using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Models;

namespace vega.Persistence
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        Task<Vehicle> GetVehicle(int Id, bool IncludeRelated = true);
        Task<Model> GetModel(int Id, bool IncludeRelated = true);
        Task<QueryResult<Vehicle>> GetVehicleList(VehicleQuery filter);
        void Remove(Vehicle vehicle);
        bool VehicleExists(int id);
        bool ModelExists(int id);
    }
    public interface IPhotoRepository
    {
        Task<IEnumerable< Photo>> GetPhotos(int vehicleId);
  
    }
}
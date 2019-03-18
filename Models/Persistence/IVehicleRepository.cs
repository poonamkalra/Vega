using System.Threading.Tasks;
using vega.Models;

namespace Vega.Models.Persistence
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id, bool includeRelated =true);
         void Add (Vehicle vehicle);
         void Remove(Vehicle vehicle);
    }
}
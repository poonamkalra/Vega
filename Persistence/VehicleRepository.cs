using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Core;
namespace Vega.Persistence
{
    public class VehicleRepository: IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
            .Include(I => I.Features)
                .ThenInclude(vf => vf.Feature)
            .Include(I => I.Model)
                .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(f => f.Id == id);
        }

        public void Add (Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}
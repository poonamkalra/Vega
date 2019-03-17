using vega.Models;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
 namespace vega.Controllers  
 {   
     [Route("/api/vehicles")]
     public class VehiclesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;           
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId","Invalid modelId.");
                 return BadRequest(ModelState);
            }

            var vehicle  = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await context.Vehicles.Include(I => I.Features).SingleOrDefaultAsync(f => f.Id == id);

            if(vehicle == null)
             return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
             
             return Ok(vehicleResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);

            if(vehicle == null)
             return NotFound();

            context.Vehicles.Remove(vehicle);
              await context.SaveChangesAsync();
              return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {   
                ModelState.AddModelError("ModelId","Invalid modelId.");
                 return BadRequest(ModelState);
            }

            var vehicle = await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

            mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            //context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        } 
        
    }
 }
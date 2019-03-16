using  vega.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using vega.Controllers.Resources;

namespace vega.Controllers
{
    public class MakesController: Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

         public MakesController(VegaDbContext _context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = _context;
        }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {

          var makes =  await context.Makes.Include(m=>m.Models).ToListAsync();
          return  mapper.Map<List<Make>,List<MakeResource>>(makes);
        }

         [HttpGet("api/features")]
        public async Task<IEnumerable<Feature>>  GetFeatures()
        {
            return await context.Features.ToListAsync();
        }
    }
}
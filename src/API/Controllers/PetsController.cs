using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly DataContext context;
        public PetsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPetsAsync()
        {
            return await context.Pets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetAsync(int id)
        {
            return await context.Pets.FindAsync(id);
        }
    }
}
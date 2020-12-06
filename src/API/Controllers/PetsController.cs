using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// The pets controller.
    /// </summary>
    public class PetsController : BaseApiController
    {
        private readonly DataContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetsController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PetsController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Returns the pets list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetUserEntity>>> GetPetsAsync()
        {
            return await dbContext.PetUsers.ToListAsync();
        }

        /// <summary>
        /// Returns the pet by it identifier.
        /// </summary>
        /// <param name="id">The pet identifier.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PetUserEntity>> GetPetAsync(int id)
        {
            return await dbContext.PetUsers.FindAsync(id);
        }
    }
}
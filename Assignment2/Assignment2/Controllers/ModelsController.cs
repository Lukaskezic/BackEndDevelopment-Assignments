using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using AutoMapper;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly Db _context;
        private readonly IMapper _mapper;

        public ModelsController(Db context)
        {
            _context = context;
            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Job, JobDTO>();
                configuration.CreateMap<JobDTO, Job>();
            });
            _mapper = config.CreateMapper();
        }

        // GET: api/Models
        [HttpGet("noexpenses/nojobs")]
        public async Task<ActionResult<IEnumerable<ModelDTO>>> GetModels()
        {
            var Models = from b in _context.Models
                         select new ModelDTO()
                         {
                             ModelId = b.ModelId,
                             FirstName = b.FirstName,
                             LastName = b.LastName,
                             Email = b.Email,
                             PhoneNo = b.PhoneNo,
                             AddresLine1 = b.AddresLine1,
                             AddresLine2 = b.AddresLine2,
                             Zip = b.Zip,
                             City = b.City,
                             BirthDay = b.BirthDay,
                             Height = b.Height,
                             ShoeSize = b.ShoeSize,
                             HairColor = b.HairColor,
                             Comments = b.Comments
                         };

            if (Models == null)
            {
                return NotFound();
            }

            return await Models.ToListAsync();
        }

        // GET: api/Models/5
        [HttpGet("{id}/withexpenses/withjobs")]
        public async Task<IActionResult> GetModeljobs(long id)
        {
            var Model = await _context.Models.Select(t => new
            {
                t.ModelId,
                jobs = t.Jobs.Select(job => new
                {
                    job.JobId,
                    job.Location
                }).ToList()
            }).ToListAsync();

            if (Model == null)
            {
                return NotFound();
            }

            return Ok(Model);
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/noexpenses/nojobs")]
        public async Task<IActionResult> PutModel(long id, ModelDTO model)
        {
            if (id != model.ModelId)
            {
                return BadRequest();
            }
            var Model = _mapper.Map<Model>(model);

            var modelItem = await _context.Models.FindAsync(id);
            if (modelItem == null)
            {
                return NotFound();
            }
            _context.Entry(modelItem).State = EntityState.Detached;

            _context.Entry(Model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.ModelId == id);
        }

        // POST: api/Models
        [HttpPost("noexpenses/nojobs")]
        public async Task<ActionResult<Model>> PostModel(ModelDTO modelDTO)
        {
            var model = new Model()
            {
                ModelId = modelDTO.ModelId,
                FirstName = modelDTO.FirstName,
                LastName = modelDTO.LastName,
                Email = modelDTO.Email,
                PhoneNo = modelDTO.PhoneNo,
                AddresLine1 = modelDTO.AddresLine1,
                AddresLine2 = modelDTO.AddresLine2,
                Zip = modelDTO.Zip,
                City = modelDTO.City,
                BirthDay = modelDTO.BirthDay,
                Height = modelDTO.Height,
                ShoeSize = modelDTO.ShoeSize,
                HairColor = modelDTO.HairColor,
                Comments = modelDTO.Comments,

            };
            _context.Models.Add(model);

            await _context.SaveChangesAsync();

            return Created(modelDTO.ModelId.ToString(), model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

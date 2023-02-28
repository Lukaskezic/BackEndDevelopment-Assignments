using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using AutoMapper;
using Humanizer;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly Db _context;
        private readonly IMapper _mapper;

        public JobsController(Db context)
        {
            _context = context;
            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Job, JobDTO>();
                configuration.CreateMap<JobDTO, Job>();
            });
            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var todoItems = await _context.Jobs.Select(t =>
                        new Job()
                        {
                            JobId = t.JobId,
                            Customer = t.Customer,
                            StartDate = t.StartDate,
                            Days = t.Days,
                            Location = t.Location,
                            Comments = t.Comments,
                            Models = t.Models,
                            Expenses = t.Expenses
                        }).ToListAsync();

            if (todoItems == null)
            {
                return NotFound();
            }

            return Ok(todoItems);
        }

        [HttpGet("noexpenses")]
        public async Task<IActionResult> GetJobsWithModelNames()
        {
            var todoItems = await _context.Jobs.Select(t =>new
                        {
                            JobId = t.JobId,
                            Customer = t.Customer,
                            StartDate = t.StartDate,
                            Days = t.Days,
                            Location = t.Location,
                            Comments = t.Comments,
                            Models = t.Models.Select(model => new
                            {
                                model.FirstName,
                                model.LastName
                            }).ToList()
                        }).ToListAsync();

            if (todoItems == null)
            {
                return NotFound();
            }

            return Ok(todoItems);
        }

        [HttpGet("{id}/withexpenses")]
        public async Task<ActionResult> GetJobsWithoutExpenses(long id)
        {
            var todoItems = await _context.Jobs.Select(t => new
            {
                JobId = t.JobId,
                Customer = t.Customer,
                StartDate = t.StartDate,
                Days = t.Days,
                Location = t.Location,
                Comments = t.Comments,
                Expenses = t.Expenses.Select(k => new
                {
                    k.ExpenseId,
                    k.amount
                }).ToList()
            }).ToListAsync();

            if (todoItems == null)
            {
                return NotFound();
            }

            return Ok(todoItems);
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(long id, JobDTO job)
        {
            if (id != job.JobId)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            var jobDTO = _mapper.Map<JobDTO>(job);

            var jobItem = await _context.Jobs.FindAsync(id);
            if (jobItem == null)
            {
                return NotFound();
            }
            _context.Entry(jobItem).State = EntityState.Detached;

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(JobDTO3 jobDTO)
        {
            var job = new Job()
            {
                JobId = jobDTO.JobId,
                Customer = jobDTO.Customer,
                StartDate = jobDTO.StartDate,
                Days = jobDTO.Days,
                Location = jobDTO.Location,
                Comments = jobDTO.Comments
            };
            _context.Jobs.Add(job);

            await _context.SaveChangesAsync();

            return Created(jobDTO.JobId.ToString(), job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }

        [HttpPatch("Add/{jobId}/{modelId}")]
        public async Task<IActionResult> PatchJob(long jobId, long modelId)
        {
            if (!_context.Models.Any(e => e.ModelId == modelId) || !JobExists(jobId))
            {
                return BadRequest();
            }

            var model = await _context.Models.FindAsync(modelId);

            var job = await _context.Jobs.Where(x => x.JobId == jobId).Include(x => x.Models).FirstOrDefaultAsync();

            job.Models.Add(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(jobId))
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

        [HttpPatch("Remove/{modelId}/{jobId}")]
        public async Task<IActionResult> PatchRemove(long modelId, long jobId)
        {
            if (!_context.Models.Any(e => e.ModelId == modelId) || !JobExists(jobId))
            {
                return BadRequest();
            }

            var model = await _context.Models.FindAsync(modelId);

            var job = await _context.Jobs.Where(x => x.JobId == jobId).Include(x => x.Models).FirstOrDefaultAsync();

            job.Models.Remove(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(jobId))
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
    }
}
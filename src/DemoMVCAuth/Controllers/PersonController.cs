using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVCAuth.Data;
using DemoProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Newtonsoft.Json;
namespace DemoMVCAuth.Controllers
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string contentType = bindingContext.HttpContext.Request.ContentType;

            if (contentType.Contains("application/json"))
            {
                using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
                var body = reader.ReadToEndAsync().Result;
                var person = JsonConvert.DeserializeObject<Person>(body);
                bindingContext.Result = ModelBindingResult.Success(person);
            }
            else if (contentType.Contains("multipart/form-data") || contentType.Contains("application/x-www-form-urlencoded"))
            {
                var form = bindingContext.HttpContext.Request.Form;
                var person = new Person
                {
                    // Assuming you have properties like FirstName, LastName, etc.
                    FirstName = form["FirstName"],
                    LastName = form["LastName"],
                    PhoneNumber = form["PhoneNumber"],
                    JobID = int.Parse(form["JobID"]),
                    // Add other properties
                };
                bindingContext.Result = ModelBindingResult.Success(person);
            }

            return Task.CompletedTask;
        }
    }
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }




        // GET: Person
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.People.Where(p => p.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier) || p.UserID == null).Include(p => p.Job);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Job)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name");

            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([ModelBinder(BinderType = typeof(PersonModelBinder))] Person newPerson, [ValidateNever] string PubliclyVisible, [ValidateNever] string JobName)
        {
            ModelState.Remove(nameof(PubliclyVisible));
            ModelState.Remove(nameof(JobName));
            if (ModelState.IsValid)
            {
                if (newPerson.JobID == 0)
                {
                    Job newJob = new Job() { Name = JobName };
                    _context.Jobs.Add(newJob);
                    _context.SaveChanges();
                    newPerson.JobID = newJob.ID;
                }
                if (PubliclyVisible == "on")
                {
                    newPerson.UserID = null;
                }
                _context.Add(newPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name", newPerson.JobID);
            ViewBag.PubliclyVisible = PubliclyVisible;
            ViewBag.JobName = JobName;
            if (Request.Headers["Accept"].ToString().Split(",").Contains("text/html"))
            {
                return View(newPerson);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    return Ok(newPerson);
                }
                else
                {
                    return BadRequest(ModelState.Select(kvp => new { kvp.Key, kvp.Value.Errors }));
                }
            }

        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name", person.JobID);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,JobID,UserID")] Person person, string PubliclyVisible)
        {
            if (id != person.ID)
            {
                return NotFound();
            }
            if (person.UserID != User.FindFirstValue(ClaimTypes.NameIdentifier) && person.UserID != null)
            {
                return Unauthorized();
            }
            var existingPerson = await _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.ID == person.ID);
            if (existingPerson != null && existingPerson.UserID != person.UserID && person.UserID != null)
            {
                ModelState.AddModelError(nameof(Person.UserID), "Cannot change the UserID of a person or set a null UserID to a value, it can only be set from a value to null.");
            }

            if (ModelState.IsValid)
            {
                if (PubliclyVisible == "on")
                {
                    person.UserID = null;
                }
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name", person.JobID);
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Job)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.ID == id);
        }
    }
}

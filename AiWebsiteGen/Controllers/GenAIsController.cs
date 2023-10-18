using AiWebsiteGen.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AiWebsiteGen.Controllers
{
    public class GenAIsController : Controller
    {
        private readonly websiteDbContext _context;

        public GenAIsController(websiteDbContext context)
        {
            _context = context;
           
        }

        // GET: GenAIs
        public async Task<IActionResult> Index()
        {
              return _context.GenAIs != null ? 
                          View(await _context.GenAIs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GenAIs'  is null.");
        }

        // GET: GenAIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GenAIs == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAIs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        public IActionResult Create()
        {
            ViewBag.StringValue = "Create";
            return View();
        }

        [HttpGet("GenAIs/Create/{stringValue}")]
        public IActionResult Create(string stringValue)
        {
            ViewBag.StringValue = stringValue;
            return View();
        }

        // POST: GenAIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenAIName,Summary,ImageFilename,AnchorLink,Like")] GenAI genAI, IFormFile imageFile, string stringValue)
        {
            if (ModelState.IsValid)
            {
                string genAINameLower = genAI.GenAIName.ToLower();

                // Format the ImageFilename as "GenAIName_thumbnail"
                string imageFileName = $"{genAINameLower}_thumbnail.jpg";

                // Format the AnchorLink as "Home/GenAISites#GenAIName_thumbnail"
                string anchorLink = $"Home/GenAISites#{genAINameLower}_thumbnail";

                genAI.ImageFilename = imageFileName;
                genAI.AnchorLink = anchorLink;

                _context.Add(genAI);
                await _context.SaveChangesAsync();

                // Upload image
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    string filePath = Path.Combine(uploadsFolder, imageFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                }

                await _context.SaveChangesAsync();
                if (stringValue == "CreateRedirectToGenAiSites/")
                {
                    Console.WriteLine("RedirectToGenAiSitesSuccess");
                    return RedirectToAction("GenAiSites", "Home");
                    
                }
                if (stringValue == "Create/")
                {
                    return RedirectToAction("Index", "Home");
                }

                
            }

            //print model state errors
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(genAI);

        }

        
        // GET: GenAIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GenAIs == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAIs.FindAsync(id);
            if (genAI == null)
            {
                return NotFound();
            }
            return View(genAI);
        }

        // POST: GenAIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenAIName,Summary,ImageFilename,AnchorLink,Like")] GenAI genAI, IFormFile imageFile)
        {
            if (id != genAI.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    string genAINameLower = genAI.GenAIName.ToLower();
                    string imageFileName = $"{genAINameLower}_thumbnail.jpg";
                    string anchorLink = $"Home/GenAISites#{genAINameLower}_thumbnail";

                    genAI.ImageFilename = imageFileName;
                    genAI.AnchorLink = anchorLink;

                    _context.Update(genAI);
                    await _context.SaveChangesAsync();

                    var existingGenAI = await _context.GenAIs.FindAsync(id);

                    if (existingGenAI == null)
                    {
                        return NotFound();
                    }

                    if (imageFile != null)
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        string oldFilePath = Path.Combine(uploadsFolder, existingGenAI.ImageFilename);

                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }

                        string filePath = Path.Combine(uploadsFolder, imageFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                    }
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!GenAIExists(genAI.Id))
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
            return View(genAI);
        }

        // GET: GenAIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GenAIs == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAIs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }
        public async Task<IActionResult> Like(int? id)
        {
            if (id == null || _context.GenAIs == null)
            {
                return NotFound();
            }
            
            var genAI = await _context.GenAIs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }
            genAI.Like++;
            _context.Update(genAI);
            await _context.SaveChangesAsync();
            

            return RedirectToAction("GenAiSites", "Home");
        }


        // POST: GenAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GenAIs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GenAIs'  is null.");
            }
            var genAI = await _context.GenAIs.FindAsync(id);
            if (genAI != null)
            {
                _context.GenAIs.Remove(genAI);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenAIExists(int id)
        {
          return (_context.GenAIs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

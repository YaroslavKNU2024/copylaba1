#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirusAppFinal;

namespace VirusAppFinal.Controllers
{
    public class VariantsController : Controller
    {
        private readonly VirusBaseContext _context;
        private Variant vr = new Variant();
        public VariantsController(VirusBaseContext context) {
            _context = context;
        }

        // GET: Variants
        public async Task<IActionResult> Index()
        {

            return View(await _context.Variants.ToListAsync());
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var variant = await _context.Variants
                .Include(v => v.Countries)
                .FirstOrDefaultAsync(m => m.Id == id);
            //ViewBag.CountryName = country.CountryName;
            if (variant == null)
            {
                return NotFound();
            }

            return View(variant);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var variant = await _context.Variants
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (variant == null)
            //    return NotFound();

            //return View(variant);
            //return RedirectToAction("Index", "Viruses", new { id = variant.Id, name = variant.VariantName });
        }

        // GET: Artists/Create
        public IActionResult Create() {

            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VariantName,VariantOrigin,VariantDateDiscovered, VirusId")] Variant variant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variant);
                vr = variant;
                await _context.SaveChangesAsync();
                //await Adder(variant.Id, variant);
                //return RedirectToAction("Index", "CountriesVariants");
                return RedirectToAction(nameof(Index));
            }
            return View(variant);
        }

        public async Task<IActionResult> ListDetails(int? id)
        {
            if (id is null)
            {
                return RedirectToPage("Index", "Variants");
            }

            var variant = await _context.Variants.FirstOrDefaultAsync(a => a.Id == id);
            if (variant is null)
            {
                return RedirectToPage("Index", "Variants");
            }

            var x = await _context.CountriesVariants.Where(c => c.VariantId == id).ToListAsync();
            //if (playsongs.Count == 0) return View(await _context.Songs.Where(a => a.Id <= 5).ToListAsync());

            List<int> countriesIds = new();
            List<string> timeAdded = new();
            foreach (var g in x)
            {
                countriesIds.Add(g.CountryId);
                //timeAdded.Add(g.TimeSongAdded.Date.ToShortDateString());
            }

            var countries = await _context.Countries
                .Where(c => countriesIds.Contains(c.Id)).ToListAsync();

            ViewBag.VariantId = id;
            ViewBag.countries = countries;
            ViewBag.VariantName = variant.VariantName;
            //ViewBag.TimeAdded = timeAdded;
            //ViewBag.iteratorTime = 0;
            //ViewBag.LinkToImage = playlist.PhotoLink;
            return View(countries);
        }

        public async Task<IActionResult> Adder(int id, Variant variant)
        {
            if (ModelState.IsValid)
            {
                Variant v = new Variant();
                v.VariantName = variant.VariantName;
                v.Id = id;
                CountriesVariant countriesvariant = new CountriesVariant();
                countriesvariant.Variant = v;
                //countriesvariant.TimeSongAdded = time;
                _context.Add(countriesvariant);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index", "Variants");
        }
        //public async Task<IActionResult> AddCountriesVariants()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(vr);
        //        await _context.SaveChangesAsync();
        //        //await Adder(variant.Id, variant);
        //        //return RedirectToAction("AddCountriesVariants", "Variants", new { id = variant.Id, v = variant });
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}
        //// POST: Songs/AddPlaylistSong/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddCountriesVariants(int id, [Bind("Id, VariantName, VariantOrigin, VariantDateDiscovered, VirusId")] Variant variant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == countriesvariant.CountryId);
        //        //if (country is null) return NotFound();
        //        //var newvariant = await _context.Variants.FirstOrDefaultAsync(c => c.Id == variant.Id);
        //        //if (variant is null) return NotFound();
        //        //var isThereACopy = await _context.CountriesVariants
        //        //    .FirstOrDefaultAsync(c => c.VariantId == countriesvariant.VariantId
        //        //                              && c.CountryId == countriesvariant.CountryId);
        //        //if (isThereACopy is not null)
        //        //    return RedirectToAction("ListDetails", "Countries",
        //        //        new { id = countriesvariant.VariantId });
        //        //var time = DateTimeOffset.UtcNow;
        //        //Variant v = new Variant();
        //        //v.VariantName = variant.VariantName;
        //        //v.Id = id;
        //        //CountriesVariant countriesvariant = new CountriesVariant();
        //        //countriesvariant.Variant= v;
        //        //countriesvariant.TimeSongAdded = time;
        //        _context.Add(vr);
        //        await _context.SaveChangesAsync();

        //        //return RedirectToAction("Index", "Songs", new {id = albumId, name = _context.Albums.Where(b => b.Id == albumId).FirstOrDefault().Name});
        //    }
        //    return RedirectToPage("Index", "Variants");
        //    //return RedirectToAction("ListDetails", "Countries", new { id = countriesvariant.VariantId });

        //}










        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null) {
            //    return NotFound();
            //}

            //var variant = await _context.Variants.Include(c => c.Country).FirstOrDefaultAsync(c => c.Id == id);
            //if (variant == null)
            //{
            //    return NotFound();
            //}
            //ViewBag.Countries = new MultiSelectList(_context.Countries, "Id", "CountryName");
            ////ViewBag.CountryName = countriesEdit.CountryName;
            //return View(variant);
            if (id == null)
                return NotFound();

            var variant = await _context.Variants.FindAsync(id);
            if (variant == null)
            {
                return NotFound();
            }
            return View(variant);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VariantName,VariantOrigin,VariantDateDiscovered, VirusId")] Variant variant)
        {
            if (id != variant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(variant.Id))
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
            return View(variant);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Variants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null) {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variant = await _context.Variants.FindAsync(id);
            _context.Variants.Remove(variant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Variants.Any(e => e.Id == id);
        }
    }
}
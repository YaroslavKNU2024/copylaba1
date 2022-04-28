#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirusAppFinal;
using VirusAppFinal.Models;


namespace VirusAppFinal.Controllers
{
    public class CountriesController : Controller
    {
        private readonly VirusBaseContext _context;

        public CountriesController(VirusBaseContext context) {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            //    try
            //    {
            //        _context.Add(countryVariant);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (Exception ex) { }
            return View(await _context.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(v => v.Variants)
                .FirstOrDefaultAsync(m => m.Id == id);
            //ViewBag.CountryName = country.CountryName;
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryName")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                //Country newCountry = new Country();

                //newCountry.CountryName = country.CountryName;
                //var cList = _context.Countries;
                //foreach (var c in cList)
                //{
                //    if (c.CountryName == country.CountryName)
                //    {
                //        newCountry.Id = c.Id;
                //    }
                //}

                //countryVariant.Country = newCountry;
                //_context.Add(countryVariant);
                return RedirectToAction("Index", "Countries");
                //return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.Include(c => c.Variants).FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            ViewBag.Variants = new MultiSelectList(_context.Variants, "Id", "VariantName");
            var countriesEdit = new CountriesEdit
            {
                Id = country.Id,
                CountryName = country.CountryName,
                VariantsIds = country.Variants.Select(c => c.Id).ToList()
            };
            ViewBag.CountryName = countriesEdit.CountryName;
            return View(countriesEdit);
        }




        

        //public async Task<IActionResult> ListDetails(int? id)
        //{
        //    if (id is null)
        //    {
        //        return RedirectToPage("Index", "Variants");
        //    }

        //    var variant = await _context.Variants.FirstOrDefaultAsync(a => a.Id == id);
        //    if (variant is null)
        //    {
        //        return RedirectToPage("Index", "Variants");
        //    }

        //    var x = await _context.CountriesVariants.Where(c => c.VariantId == id).ToListAsync();
        //    //if (playsongs.Count == 0) return View(await _context.Songs.Where(a => a.Id <= 5).ToListAsync());

        //    List<int> countriesIds = new();
        //    List<string> timeAdded = new();
        //    foreach (var g in x)
        //    {
        //        countriesIds.Add(g.CountryId);
        //        //timeAdded.Add(g.TimeSongAdded.Date.ToShortDateString());
        //    }

        //    var countries = await _context.Countries
        //        .Where(c => countriesIds.Contains(c.Id)).ToListAsync();

        //    ViewBag.VariantId = id;
        //    ViewBag.countries = countries;
        //    ViewBag.VariantName = variant.VariantName;
        //    //ViewBag.TimeAdded = timeAdded;
        //    //ViewBag.iteratorTime = 0;
        //    //ViewBag.LinkToImage = playlist.PhotoLink;
        //    return View(countries);
        //}


        //// GET: Songs/AddPlaylistSong/5
        //public async Task<IActionResult> AddCountriesVariants(int? listId)
        //{
        //    if (listId is null)
        //        return NotFound();

        //    //ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name");
        //    ViewBag.VariantId = listId;
        //    var list = await _context.Variants.FindAsync(listId);
        //    if (list is null) return NotFound();
        //    ViewBag.VariantName = list.VariantName;
        //    ViewBag.Countries = new MultiSelectList(_context.Countries, "Id", "CountryName");
        //    return View();
        //}
        //// POST: Songs/AddPlaylistSong/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddCountriesVariants(int albumId, [Bind("Id, CountryId, VariantId, Cases, Dead")] CountriesVariant countriesvariant)
        //{
        //    if (ModelState.IsValid) {
        //        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == countriesvariant.CountryId);
        //        if (country is null) return NotFound();
        //        var variant = await _context.Variants.FirstOrDefaultAsync(c => c.Id == countriesvariant.VariantId);
        //        if (variant is null) return NotFound();
        //        var isThereACopy = await _context.CountriesVariants
        //            .FirstOrDefaultAsync(c => c.VariantId == countriesvariant.VariantId
        //                                      && c.CountryId == countriesvariant.CountryId);
        //        if (isThereACopy is not null)
        //            return RedirectToAction("ListDetails", "Countries",
        //                new { id = countriesvariant.VariantId });
        //        var time = DateTimeOffset.UtcNow;
        //        //countriesvariant.TimeSongAdded = time;
        //        _context.Add(countriesvariant);
        //        await _context.SaveChangesAsync();
        //        //return RedirectToAction("Index", "Songs", new {id = albumId, name = _context.Albums.Where(b => b.Id == albumId).FirstOrDefault().Name});
        //    }
        //    return RedirectToAction("ListDetails", "Countries", new { id = countriesvariant.VariantId });

        //}









        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountriesEdit countryEdit)//[Bind("Id,CountryName")] Country country)
        {
            if (id != countryEdit.Id)
                return NotFound();

            if (ModelState.IsValid) {
                var country = await _context.Countries.Include(c => c.Variants).FirstOrDefaultAsync(d => d.Id == countryEdit.Id);
                if (country is null)
                    return NotFound();

                country.CountryName = countryEdit.CountryName;
                var variants = await _context.Variants.Where(v => countryEdit.VariantsIds.Contains(v.Id)).ToListAsync();
                country.Variants = variants;
                try {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!CountryExists(country.Id))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

                //return View(countryEdit);

                //return RedirectToAction("Index", "Songs", new { id = song.AlbumId, name = albumName });
                //try
                //{
                //    _context.Update(country);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!CountryExists(country.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                //return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            ViewBag.CountryName = country.CountryName;
            //ViewBag.AlbumId = song.AlbumId;
            //var album = await _context.Albums.FindAsync(country.AlbumId);
            //ViewBag.AlbumName = album!.Name;
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country  == null) return NotFound();
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
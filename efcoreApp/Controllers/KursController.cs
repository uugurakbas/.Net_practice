using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController:Controller
    {

        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(){

            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id){

            if(id == null){

                return NotFound();
            }

            var krs = await _context.Kurslar.Include(o => o.OgrenciKayitlari).ThenInclude(o => o.Ogrenci).FirstOrDefaultAsync(o => o.KursId == id);
            //aynı  işe yarar
            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id);
            
            if(krs == null){

                return NotFound();
            }

            return View(krs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // crossside ataklarını engeller
        public async Task<IActionResult> Edit(int id, Kurs model){

            if(id != model.KursId){
                return NotFound();
            }

            if(ModelState.IsValid){

                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException)  // update hatası
                {
                    if(!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id){

            if(id == null){

                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);

            if(kurs == null){

                return NotFound();
            }
            return View(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id){

            var kurs = await _context.Kurslar.FindAsync(id);
            if(kurs == null){

                return NotFound();
            }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
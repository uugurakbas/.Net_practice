using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {

        private readonly DataContext _context;


        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(){

            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id){

            if(id == null){

                return NotFound();
            }
    
            var ogr = await _context.Ogrenciler.Include(o => o.KursKayitlari).ThenInclude(O => O.Kurs).FirstOrDefaultAsync(o => o.OgrenciId == id);
            //aynı  işe yarar
            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id);
            
            if(ogr == null){

                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // crossside ataklarını engeller
        public async Task<IActionResult> Edit(int id, Ogrenci model){

            if(id != model.OgrenciId){
                return NotFound();
            }

            if(ModelState.IsValid){

                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)  // olmayan bir kayıtla güncelleme yapılmaya çalışıyorsa
                {
                    if(!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
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

            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if(ogrenci == null){

                return NotFound();
            }

            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id){//ıd nin nereden geldiğini kontrol etmek


            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if(ogrenci == null){
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
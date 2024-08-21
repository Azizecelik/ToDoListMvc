using Microsoft.AspNetCore.Mvc;
using ToDoListProject.Data;
using ToDoListProject.Models;

namespace ToDoListProject.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor: Veritabanı bağlamını (_context) alır ve bu sınıfın içinde kullanıma hazır hale getirir.
        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Edit (Get): Belirtilen id'ye sahip görevi getirir ve edit sayfasında görüntüler.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev == null)
            {
                return NotFound(); // Eğer görev bulunamazsa 404 sayfası döndürülür.
            }
            return View(gorev); // Görevi edit sayfasında göster.
        }

        // Edit (Post): Düzenlenen görevi alır ve veritabanında günceller.
        [HttpPost]
        public IActionResult Edit(GorevlerInfo gorev)
        {
            if (ModelState.IsValid)
            {
                // Tarih alanını güncel tarih olarak ayarlar
                gorev.Tarih = DateTime.Now;

                // Görevi günceller
                _context.Gorevler.Update(gorev);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Güncelleme sonrası anasayfaya yönlendirir.
            }
            return View(gorev); // Eğer model hatalıysa aynı sayfaya geri döner.
        }

        // Delete (Get): Belirtilen id'ye sahip görevi getirir ve delete sayfasında görüntüler.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev == null)
            {
                return NotFound(); // Eğer görev bulunamazsa 404 sayfası döndürülür.
            }
            return View(gorev); // Görevi delete sayfasında göster.
        }

        // Delete (Post): Belirtilen id'ye sahip görevi veritabanından siler.
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _context.Gorevler.Find(id);
            if (item == null)
            {
                return NotFound(); // Eğer görev bulunamazsa 404 sayfası döndürülür.
            }

            // Görevi veritabanından siler
            _context.Gorevler.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index"); // Silme işlemi sonrası anasayfaya yönlendirir.
        }

        // Index: Tüm görevlerin listesini getirir ve anasayfada gösterir.
        public IActionResult Index()
        {
            var gorevlerListesi = _context.Gorevler.ToList(); // Görevlerin tamamını getirir.
            return View(gorevlerListesi); // Görevler listesini index sayfasında gösterir.
        }

        // Create (Get): Yeni bir görev oluşturma sayfasını açar.
        [HttpGet]
        public IActionResult Create()
        {
            var model = new GorevlerInfo(); // Yeni bir görev modeli oluşturur.
            return View(model); // Yeni görev sayfasını görüntüler.
        }

        // Create (Post): Yeni oluşturulan görevi veritabanına ekler.
        [HttpPost]
        public IActionResult Create(GorevlerInfo gorevlerInfo)
        {
            // Görev oluşturulma tarihini şu anki zaman olarak ayarlar.
            gorevlerInfo.Tarih = DateTime.Now;
            if (ModelState.IsValid)
            {
                // Görev bilgilerini veritabanına kaydeder.
                _context.Gorevler.Add(gorevlerInfo);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Kayıt sonrası anasayfaya yönlendirir.
            }

            return View(gorevlerInfo); // Eğer model hatalıysa aynı sayfaya geri döner.
        }

        // UpdateGorevDurumu (Post): Görev durumunu günceller.
        [HttpPost]
        public IActionResult UpdateGorevDurumu(int id, bool durum)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev != null)
            {
                // Görev durumunu günceller
                gorev.GorevDurumu = durum;
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); // Güncelleme sonrası anasayfaya yönlendirir.
        }
    }
}

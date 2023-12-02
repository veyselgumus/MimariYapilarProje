#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Context;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using AppCore.Results.Bases;

namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class KullanicisController : Controller
    {
        // Add service injections here
        private readonly IKullaniciService _kullaniciService;
        private readonly IRolService _rolService;

        public KullanicisController(IKullaniciService kullaniciService, IRolService rolService)
        {
            _kullaniciService = kullaniciService;
            _rolService = rolService;
        }

        // GET: Kullanicis
        public IActionResult Index()
        {
            List<KullaniciModel> kullaniciList = _kullaniciService.Query().OrderBy(k=>k.UserName).ToList(); // TODO: Add get list service logic here
            return View(kullaniciList);
        }

        // GET: Kullanicis/Details/5

        // GET: Kullanicis/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_rolService.Query().ToList(), "Id", "Adi");
            return View();
        }

        // POST: Kullanicis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[AllowAnonymous]
		public IActionResult Create(KullaniciModel kullanici)
        {
			Result result = _kullaniciService.Add(kullanici);
			if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
				_kullaniciService.Add(kullanici);
                TempData["Message"] = result.Message;
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_rolService.Query().ToList(), "Id", "Adi", kullanici.RolId);
			TempData["Message"] = result.Message;
			return View(kullanici);
        }

        // GET: Kullanicis/Edit/5
        public IActionResult Edit(int id)
        {
            KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k=>k.Id==id); // TODO: Add get item service logic here
            if (kullanici == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_rolService.Query().ToList(), "Id", "Adi", kullanici.RolId);
            return View(kullanici);
        }

        // POST: Kullanicis/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KullaniciModel kullanici)
        {
            if (ModelState.IsValid)
            {
                Result result=_kullaniciService.Update(kullanici);
                if(result.IsSuccessful)
                {
                    _kullaniciService.Update(kullanici);
                    TempData["Message"]=result.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["Message"] = result.Message;
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_rolService.Query().ToList(), "Id", "Adi", kullanici.RolId);
            return View(kullanici);
        }

        // GET: Kullanicis/Delete/5
        public IActionResult Delete(int id)
        {
            KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k=>k.Id==id); // TODO: Add get item service logic here
            if (kullanici == null)
            {
                return NotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanicis/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _kullaniciService.Delete(id);
            if (result.IsSuccessful)
            {
				_kullaniciService.Delete(id);
                TempData["Message"]=result.Message;
				return RedirectToAction(nameof(Index));
			}
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));
        }
	}
}

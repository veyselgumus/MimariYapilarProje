#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Context;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using AppCore.Results.Bases;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class TursController : Controller
    {
        // Add service injections here
        private readonly ITurService _turService;

        public TursController(ITurService turService)
        {
            _turService = turService;
        }

        // GET: Turs
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<TurModel> turList = _turService.Query().OrderBy(t=>t.Adi).ToList(); // TODO: Add get list service logic here
            return View(turList);
        }

        // GET: Turs/Details/5
        public IActionResult Details(int id)
        {
            TurModel tur = _turService.Query().SingleOrDefault(t=>t.Id==id); // TODO: Add get item service logic here
            if (tur == null)
            {
                return NotFound();
            }
            return View(tur);
        }

		// GET: Turs/Create
		[AllowAnonymous]
		public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Turs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[AllowAnonymous]
		public IActionResult Create(TurModel tur)
        {
            if (ModelState.IsValid)
            {
                Result result = _turService.Add(tur);
                if(result.IsSuccessful)
                {
					_turService.Add(tur);
                    TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Index));
				}
				TempData["Message"] = result.Message;
				// TODO: Add insert service logic here

			}
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(tur);
        }

        // GET: Turs/Edit/5
        public IActionResult Edit(int id)
        {
            TurModel tur = _turService.Query().SingleOrDefault(t=>t.Id==id); // TODO: Add get item service logic here
            if (tur == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(tur);
        }

        // POST: Turs/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TurModel tur)
        {
            if (ModelState.IsValid)
            {
				// TODO: Add update service logic here
				Result result = _turService.Update(tur);
                if( result.IsSuccessful)
                {
					_turService.Update(tur);
                    TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Index));
				}
				TempData["Message"] = result.Message;
			}
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(tur);
        }

        // GET: Turs/Delete/5
        public IActionResult Delete(int id)
        {
            TurModel tur = _turService.Query().SingleOrDefault(t=>t.Id==id); // TODO: Add get item service logic here
            if (tur == null)
            {
                return NotFound();
            }
            return View(tur);
        }

        // POST: Turs/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _turService.Delete(id);
            if(result.IsSuccessful)
            {
				_turService.Delete(id);
                TempData["Message"]=result.Message;
				return RedirectToAction(nameof(Index));
			}
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));

		}
	}
}

#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Context;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using AppCore.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using AppCore.Results;

namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MimarsController : Controller
    {
        // Add service injections here
        private readonly IMimarService _mimarService;

        public MimarsController(IMimarService mimarService)
        {
            _mimarService = mimarService;
        }

		// GET: Mimars
		[AllowAnonymous]
		public IActionResult Index()
        {
            List<MimarModel> mimarList = _mimarService.Query().ToList(); // TODO: Add get list service logic here
            return View(mimarList);
        }

		// GET: Mimars/Details/5
		[AllowAnonymous]
		public IActionResult Details(int id)
        {
            MimarModel mimar = _mimarService.Query().SingleOrDefault(m=>m.Id==id); // TODO: Add get item service logic here
            if (mimar == null)
            {
                return NotFound();
            }
            return View(mimar);
        }

		// GET: Mimars/Create
		[AllowAnonymous]
		public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Mimars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[AllowAnonymous]
		public IActionResult Create(MimarModel mimar,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                Result result;
                result = UpdateImage(mimar, image);
                if(result.IsSuccessful)
                {
					result = _mimarService.Add(mimar);
					if (result.IsSuccessful)
					{
						_mimarService.Add(mimar);
						TempData["Message"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
					TempData["Message"] = result.Message;
				}
				TempData["Message"] = result.Message;
			}
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(mimar);
        }
		private Result UpdateImage(MimarModel mimar, IFormFile image)
		{
			Result result = new SuccessResult();
			if (image is not null && image.Length > 0)
			{
				#region dosya uzantısı ve boyutu
				string fileName = image.FileName;
				string extension = Path.GetExtension(fileName);

				if (!".jpg, .jpeg, .png".Split(",").Any(e => e.ToLower().Trim() == extension.ToLower()))
				{
					return new ErrorResult("Dosya Uzantısı (.jpeg,.jpg,.png) Olmadığı İçin Dosya Yüklenemedi!");
				}

				double dosyaBoyutu = 0.3; //mb
				double dosyaBoyutuByte = dosyaBoyutu * Math.Pow(1024, 2);

				if (image.Length > dosyaBoyutuByte)
				{
					return new ErrorResult("Dosya Boyutu Çok Büyük Olduğu İçin Dosya Yüklenemedi!");
				}
				#endregion

				#region image ve imageextension özelliklerinin güncellenmesi
				using (MemoryStream memoryStream = new MemoryStream())
				{
					image.CopyTo(memoryStream);
					mimar.Image = memoryStream.ToArray();
					mimar.ImageExtension = extension;
				}
				#endregion
			}
			return new SuccessResult("");
		}
		// GET: Mimars/Edit/5
		public IActionResult Edit(int id)
        {
            MimarModel mimar = _mimarService.Query().SingleOrDefault(m=>m.Id==id); // TODO: Add get item service logic here
            if (mimar == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(mimar);
        }

        // POST: Mimars/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MimarModel mimar,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                Result result;
                result = UpdateImage(mimar, image);
                if(result.IsSuccessful)
                {
					result = _mimarService.Update(mimar);
					if (result.IsSuccessful)
					{
						_mimarService.Update(mimar);
						TempData["Message"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
					TempData["Message"] = result.Message;
				}
				TempData["Message"] = result.Message;
			}
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(mimar);
        }

        // GET: Mimars/Delete/5
        public IActionResult Delete(int id)
        {
            MimarModel mimar = _mimarService.Query().SingleOrDefault(m=>m.Id==id); // TODO: Add get item service logic here
            if (mimar == null)
            {
                return NotFound();
            }
            return View(mimar);
        }

        // POST: Mimars/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _mimarService.Delete(id);
            if(result.IsSuccessful)
            {
				_mimarService.Delete(id);
                TempData["Message"] = result.Message;
				return RedirectToAction(nameof(Index));
			}
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));

		}
		public IActionResult FotoSil(int id)
		{
			if (id != null)
			{
				_mimarService.ResimSil(id);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
	}
}

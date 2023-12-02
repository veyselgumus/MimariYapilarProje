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
	[Authorize(Roles = "Admin")]
	public class YapisController : Controller
	{
		// Add service injections here
		private readonly IYapiService _yapiService;
		private readonly ITurService _turService;
		private readonly IMimarService _mimarService;

		public YapisController(IYapiService yapiService, ITurService turService, IMimarService mimarService)
		{
			_yapiService = yapiService;
			_turService = turService;
			_mimarService = mimarService;
		}



		// GET: Yapis
		[AllowAnonymous]
		public IActionResult Index()
		{
			List<YapiModel> yapiList = _yapiService.Query().ToList(); // TODO: Add get list service logic here
			return View(yapiList);
		}

		// GET: Yapis/Details/5
		[AllowAnonymous]
		public IActionResult Details(int id)
		{
			YapiModel yapi = _yapiService.Query().SingleOrDefault(y => y.Id == id); // TODO: Add get item service logic here
			if (yapi == null)
			{
				return View("Error", "Building not Found");
			}
			return View(yapi);
		}

		// GET: Yapis/Create
		[AllowAnonymous]
		public IActionResult Create()
		{
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewBag.Mimar = new SelectList(_mimarService.Query().ToList(), "Id", "AdSoyadGosterim");
			ViewBag.Tur = new MultiSelectList(_turService.Query().ToList(), "Id", "Adi");
			return View();
		}

		// POST: Yapis/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public IActionResult Create(YapiModel yapi, IFormFile image)
		{
			if (ModelState.IsValid)
			{
				Result result;

				//imaj yükleme
				result = UpdateImage(yapi, image);
				if (result.IsSuccessful)
				{
					result = _yapiService.Add(yapi);
					if (result.IsSuccessful)
					{
						TempData["Message"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
					TempData["Message"] = result.Message;
				}
				TempData["Message"] = result.Message;
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewBag.Mimar = new SelectList(_mimarService.Query().ToList(), "Id", "AdSoyadGosterim");
			ViewBag.Tur = new MultiSelectList(_turService.Query().ToList(), "Id", "Adi");
			return View(yapi);
		}

		private Result UpdateImage(YapiModel yapi, IFormFile image)
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
					yapi.Image = memoryStream.ToArray();
					yapi.ImageExtension = extension;
				}
				#endregion
			}
			return new SuccessResult("");
		}

		// GET: Yapis/Edit/5
		public IActionResult Edit(int id)
		{
			YapiModel yapi = _yapiService.Query().SingleOrDefault(y => y.Id == id); // TODO: Add get item service logic here
			if (yapi == null)
			{
				return NotFound();
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items

			ViewBag.Mimar = new SelectList(_mimarService.Query().ToList(), "Id", "AdSoyadGosterim", yapi.MimarId);
			ViewBag.Tur = new MultiSelectList(_turService.Query().ToList(), "Id", "Adi", yapi.TurIds);
			return View(yapi);
		}
		public IActionResult FotoSil(int id)
		{
			if (id != null)
			{
				_yapiService.ResimSil(id);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
		// POST: Yapis/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		
		public IActionResult Edit(YapiModel yapi, IFormFile image)
		{
			Result result;

			if (ModelState.IsValid)
			{
				//imaj yükleme
				result = UpdateImage(yapi, image);
				if (result.IsSuccessful)
				{
					result = _yapiService.Update(yapi);
					if(result.IsSuccessful)
					{
						_yapiService.Update(yapi);
						TempData["Message"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
					
				}
				TempData["Message"] = result.Message;
				return RedirectToAction(nameof(Index));
			}
			


			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewBag.Mimar = new SelectList(_mimarService.Query().ToList(), "Id", "AdSoyadGosterim", yapi.MimarId);
			ViewBag.Tur = new MultiSelectList(_turService.Query().ToList(), "Id", "Adi", yapi.TurIds);
			return View(yapi);
		}

		// GET: Yapis/Delete/5
		public IActionResult Delete(int id)
		{
			YapiModel yapi = _yapiService.Query().SingleOrDefault(y => y.Id == id); // TODO: Add get item service logic here
			if (yapi == null)
			{
				return NotFound();
			}
			return View(yapi);

		}

		// POST: Yapis/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			Result result = _yapiService.Delete(id);
			if (result.IsSuccessful)
			{
				_yapiService.Delete(id);
				TempData["Message"] = result.Message;
				return RedirectToAction(nameof(Index));
			}
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Examen_DAW.Models;
using Examen_DAW.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Examen_DAW.Controllers
{
	public class ComenziController : Controller
	{


		private readonly ApplicationDbContext MyDataBase;

		public ComenziController(ApplicationDbContext _MyDataBase)
		{
			MyDataBase = _MyDataBase;
		}
		public IActionResult Index(string? _Search)
		{
			if (_Search != null)
			{
				ViewBag.Articles = MyDataBase.Comenzi.Where(_Article => _Article.Nume.ToUpper().Contains(_Search.ToUpper()));
			}
			else
			{
				ViewBag.Articles = MyDataBase.Comenzi;
			}

			if (TempData.ContainsKey("Message"))
			{
				ViewBag.TempMsg = TempData["Message"];
			}

			return View();
		}

		public IActionResult Show(int _Id)
		{
			Comenzi? _Comenzi = MyDataBase.Comenzi.Include("Category").Where(_Comenzi => _Comenzi.Id == _Id).First();

			if (_Comenzi == null)
			{
				return View("Error", new ErrorViewModel { RequestId = "404. No such order in my database!" });
			}

			return View(_Comenzi);
		}

		public IActionResult Edit(int _Id)
		{
			List<SelectListItem> _SelectList = new List<SelectListItem>();

			List<Produse> _Produsele = MyDataBase.Produse.ToList();

			foreach (Produse _Produse in _Produsele)
			{
				_SelectList.Add(new SelectListItem { Value = _Produse.Id.ToString(), Text = _Produse.NumeProdus });
			}

			Comenzi? _Comenzi = MyDataBase.Comenzi.Find(_Id);

			if (_Comenzi == null)
			{
				return View("Error", new ErrorViewModel { RequestId = "404. No such order in my database!" });
			}

			ViewBag.ProduseList = _SelectList;

			return View(_Comenzi);
		}

		[HttpPost]
		public IActionResult Edit(int _Id, Comenzi _Comenzi)
		{
			List<SelectListItem> _SelectList = new List<SelectListItem>();

			List<Produse> _Produsele = MyDataBase.Produse.ToList();

			foreach (Produse _Produse in _Produsele)
			{
				_SelectList.Add(new SelectListItem { Value = _Produse.Id.ToString(), Text = _Produse.NumeProdus });
			}

			_Comenzi.Id = _Id;

			Comenzi? _OriginalComenzi = MyDataBase.Comenzi.Find(_Id);

			if (_OriginalComenzi == null)
			{
				return View("Error", new ErrorViewModel { RequestId = "404. No such order in my database !" });
			}

			if (!ModelState.IsValid)
			{
				ViewBag.ProduseList = _SelectList;

				return View(_Comenzi);
			}

			try
			{
				_OriginalComenzi.Nume = _Comenzi.Nume;
				_OriginalComenzi.Adresa = _Comenzi.Adresa;
				_OriginalComenzi.ProduseId = _Comenzi.ProduseId;

				MyDataBase.SaveChanges();

				TempData.Add("Message", "Order edited with succes");

				return Redirect("/Comenzi/Index");
			}
			catch
			{
				return View("Error", new ErrorViewModel { RequestId = "Error. Contact the dev team!" });

			}
		}


		public IActionResult New()
		{
			List<SelectListItem> _SelectList = new List<SelectListItem>();

			List<Produse> _Produsele = MyDataBase.Produse.ToList();

			foreach (Produse _Produse in _Produsele)
			{
				_SelectList.Add(new SelectListItem { Value = _Produse.Id.ToString(), Text = _Produse.NumeProdus });
			}

			ViewBag.ProduseList = _SelectList;

			return View();
		}

		[HttpPost]
		public IActionResult New(Comenzi _Comenzi)
		{
			List<SelectListItem> _SelectList = new List<SelectListItem>();

			List<Produse> _Produsele = MyDataBase.Produse.ToList();

			foreach (Produse _Produse in _Produsele)
			{
				_SelectList.Add(new SelectListItem { Value = _Produse.Id.ToString(), Text = _Produse.NumeProdus });
			}

			if (!ModelState.IsValid)
			{
				ViewBag.ProduseList = _SelectList;

				return View(_Comenzi);
			}

			try
			{
				MyDataBase.Comenzi.Add(_Comenzi);

				MyDataBase.SaveChanges();

				TempData.Add("Message", "Added with succes");

				return Redirect("/Comenzi/Index");
			}
			catch
			{
				return View("Error", "Error. Contact dev team!");
			}
		}

		[HttpPost]
		public IActionResult Delete(int _Id)
		{
			Comenzi? _Comenzi = MyDataBase.Comenzi.Find(_Id);

			if (_Comenzi == null)
			{
				return View("Error", "404. No such order...");
			}

			try
			{
				MyDataBase.Comenzi.Remove(_Comenzi);

				MyDataBase.SaveChanges();

				TempData.Add("Message", "Deleted with succes");

				return Redirect("/Comenzi/Index");
			}
			catch
			{
				return View("Error", new ErrorViewModel { RequestId = "Error. Contact dev team!" });
			}
		}
	}
}

using Banking_B.Models;
using Banking_B.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_B.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BankController(ILogger<BankController> logger) : ControllerBase
    {
        private readonly IBankService service = new BankService();

        private readonly ILogger<BankController> _logger = logger;

        [HttpGet(Name = "GetBanks")]
        public List<BankModel> Get()
        {
            _logger.LogInformation("GET /Bank");
            return service.GetBanks();
        }

        //// GET: BankController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: BankController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: BankController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BankController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: BankController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BankController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: BankController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: BankController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OFX.Models;
using OFX.Repository;

namespace OFX.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ITransacoesRepository _transacoesRepository;

        public HomeController(IHostingEnvironment hostingEnv,
                              ITransacoesRepository transacoesRepository)
        {
            _hostingEnv = hostingEnv;
            _transacoesRepository = transacoesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload([FromForm] IEnumerable<IFormFile> files)
        {

            if (files == null || files.Count() == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }
            else
            {
                var paths = new Upload(_hostingEnv).UploadFile(files);
                if (paths.Count() > 0)
                {
                    var result = ImportToDatabase.Ofx(paths);
                    if (result.Count() > 0)
                    {
                        _transacoesRepository.Insert(result);
                        return Json(_transacoesRepository.GetAll());
                    }

                    return Json("");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateTransaction(int id, string obs)
        {
            _transacoesRepository.Update(id, obs);
            
            return Json(_transacoesRepository.GetAll());
        }
    }
}

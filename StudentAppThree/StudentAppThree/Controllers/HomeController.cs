using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using StudentAppThree.Models;

namespace StudentAppThree.Controllers
{
    public class HomeController : Controller
    {
        StudentiDBEntities2 _db = new StudentiDBEntities2();

        /// <summary>
        /// Show welcome page
        /// </summary>
        /// <returns>HTML page</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Shows strongly typed partial view with list of students
        /// </summary>
        /// <returns>Partial View list of students</returns>
        public ActionResult ShowStudents()
        {

            List<Studenti> listS = _db.Studenti.Where(x => x.IsDeleted == false).ToList();

            return View("_partialShowStudents.cshtml", listS);
        }

        /// <summary>
        /// Shows strongly typed partial view with list of kolegij
        /// </summary>
        /// <returns>Partial View list of kolegij</returns>
        public ActionResult ShowKolegije()
        {
            List<Kolegij> listK = _db.Kolegij.ToList();

            return View("_partialShowKolegije.cshtml", listK);
        }

        /// <summary>
        /// Shows main view with list of students, kolegij and jQuery DataTable 
        /// </summary>
        /// <returns>Main View</returns>
        public ActionResult AppIndex()
        {
            var viewModel = new MainPageViewModel();

            List<Studenti> list = _db.Studenti.Where(x => x.IsDeleted == false).Take(10).ToList();
            IEnumerable<StudentViewModel> studentiVMList = list.Select(Mapper.Map<Studenti, StudentViewModel>);

            viewModel.StudentViewModel = studentiVMList;
            viewModel.Kolegiji = _db.Kolegij.ToList();

            return View(viewModel);
        }


        /// <summary>
        /// Saves new or edited record 
        /// </summary>
        /// <param name="IdForPopis">Popis Id</param>
        /// <param name="StudentViewModel">Student Id</param>
        /// <param name="KolegijViewModel">Kolegij Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AppIndex(int? IdForPopis, int? StudentViewModel, int? KolegijViewModel)
        {
            var popisInDB = _db.Popis.Include("Kolegij").Include("Studenti").Where(x => x.PopisId == IdForPopis).SingleOrDefault();


            try
            {
                if (IdForPopis > 0 && popisInDB != null)  // 
                {
                    Popis popis = _db.Popis.Where(x => x.PopisId == IdForPopis).SingleOrDefault();

                    popis.KolegijId = (int)KolegijViewModel;
                    popis.StudentId = (int)StudentViewModel;

                    _db.SaveChanges();
                }
                else
                {
                    Popis popis = new Popis();

                    popis.KolegijId = (int)KolegijViewModel;
                    popis.StudentId = (int)StudentViewModel;

                    _db.Popis.Add(popis);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            var viewModel = new MainPageViewModel();

            List<Studenti> list = _db.Studenti.Where(x => x.IsDeleted == false).Take(10).ToList();
            IEnumerable<StudentViewModel> studentiVMList = list.Select(Mapper.Map<Studenti, StudentViewModel>);

            viewModel.StudentViewModel = studentiVMList;
            viewModel.Kolegiji = _db.Kolegij.ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteRecord(int id)
        {
            try
            {
                var hasRecord = _db.Popis.Where(x => x.PopisId == id).SingleOrDefault();

                if (hasRecord != null)
                {
                    _db.Popis.Remove(hasRecord);
                    _db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            var viewModel = new MainPageViewModel();

            List<Studenti> list = _db.Studenti.Where(x => x.IsDeleted == false).Take(10).ToList();
            IEnumerable<StudentViewModel> studentiVMList = list.Select(Mapper.Map<Studenti, StudentViewModel>);

            viewModel.StudentViewModel = studentiVMList;
            viewModel.Kolegiji = _db.Kolegij.ToList();

            return View(viewModel);
        }


        public ActionResult ShowAllPopis()
        {
            var list = _db.Popis.Include("Kolegij").Include("Studenti").ToList();

            ViewBag.List = list;

            return View();
        }

        /// <summary>
        /// Store Popis to database
        /// </summary>
        /// <param name="modelToSave">ViewModel with model data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveEdit(PopisViewModel modelToSave)
        {
            try
            {
                List<Kolegij> listK = _db.Kolegij.ToList();
                ViewBag.Kolegij = new SelectList(listK, "KolegijId", "Naziv");

                List<Studenti> listS = _db.Studenti.ToList();
                ViewBag.Studenti = new SelectList(listS, "StudentId", "Prezime");

                var popisLista = _db.Popis.Where(x => x.PopisId == modelToSave.PopisId).ToList();

                // if popis id is not 0 and it can be found as existing already
                if (modelToSave.PopisId > 0 && popisLista.Count != 0)
                {
                    Popis popis = new Popis();

                    popis.KolegijId = modelToSave.KolegijId;
                    popis.StudentId = modelToSave.StudentId;

                    _db.Popis.Add(popis);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View();
        }

        // TODO delete
        public ActionResult ShowAllPopisDva()
        {
            var viewModel = new MainPageViewModel();

            List<Studenti> list = _db.Studenti.Where(x => x.IsDeleted == false).Take(10).ToList();
            IEnumerable<StudentViewModel> studentiVMList = list.Select(Mapper.Map<Studenti, StudentViewModel>);

            viewModel.Kolegiji = _db.Kolegij.ToList();
            viewModel.StudentViewModel = studentiVMList;
            viewModel.Popisi = _db.Popis.Include("Kolegij").Include("Studenti").ToList();
            
            return View(viewModel);
        }

        public JsonResult ShowAllPopisDvaJson()
        {
            List<PopisViewModel> list = _db.Popis.Include("Kolegij").Include("Studenti").Select(x => new PopisViewModel
            {
                Ime = x.Studenti.Ime,
                StudentId = x.StudentId,
                KolegijId = x.KolegijId,
                PopisId = x.PopisId,
                Prezime = x.Studenti.Prezime,
                Naziv = x.Kolegij.Naziv
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEditStudent(int StudentModelId)
        {

            List<Kolegij> listK = _db.Kolegij.ToList();
            ViewBag.Kolegij = new SelectList(listK, "KolegijId", "Naziv");

            List<Studenti> listS = _db.Studenti.ToList();
            ViewBag.Studenti = new SelectList(listS, "StudentId", "Prezime");

            PopisViewModel model = new PopisViewModel();

            if (StudentModelId > 0)
            {
                Popis popis = _db.Popis.SingleOrDefault();

                model.StudentId = popis.StudentId;
                model.Prezime = popis.Studenti.Prezime;
                model.Naziv = popis.Kolegij.Naziv;
                model.KolegijId = popis.KolegijId;
            }

            return PartialView("_partialAddEdit.cshtml", model);
        }

        public ActionResult AddEditStudentDva(int PopisModelId)
        {

            List<Kolegij> listK = _db.Kolegij.ToList();
            ViewBag.Kolegij = new SelectList(listK, "KolegijId", "Naziv");

            List<Studenti> listS = _db.Studenti.ToList();
            ViewBag.Studenti = new SelectList(listS, "StudentId", "PunoIme");

            //PopisViewModel model = new PopisViewModel();
            MainPageViewModel model = new MainPageViewModel();

            var popisLista = _db.Popis.Where(x => x.PopisId == PopisModelId).ToList(); 

            // if popis id is not 0 and it can be found as existing already
            if (PopisModelId > 0 &&  popisLista.Count != 0)
            {
                Popis popis = _db.Popis.SingleOrDefault();


                model.MyProperty.PopisId = PopisModelId;
                model.MyProperty.StudentId = popis.Studenti.StudentId;
                model.MyProperty.Prezime = popis.Studenti.Prezime;
                model.MyProperty.Naziv = popis.Kolegij.Naziv;
                model.MyProperty.KolegijId = popis.KolegijId;
            }

            return PartialView("_partialAddEdit", model);
        }

        [HttpPost]
        public ActionResult ShowOnePopis(int popisId)
        {
            MainPageViewModel model = new MainPageViewModel();


            model.MyProperty = new PopisViewModel();

            var popis = _db.Popis.Include("Kolegij").Include("Studenti").SingleOrDefault(x => x.PopisId == popisId);

            ViewBag.Popis = popis;

            MainPageViewModel viewModel = new MainPageViewModel();

            List<PopisViewModel> list = _db.Popis.Include("Kolegij").Include("Studenti").Where(x => x.PopisId == popisId).Select(x => new PopisViewModel
            {
                Ime = x.Studenti.Ime,
                StudentId = x.StudentId,
                KolegijId = x.KolegijId,
                PopisId = x.PopisId,
                Prezime = x.Studenti.Prezime,
                Naziv = x.Kolegij.Naziv
            }).ToList();

            viewModel.PopisViewModels = list;

            List<KolegijViewModel> listK = _db.Kolegij.Select(x => new KolegijViewModel
            {
                KolegijId = x.KolegijId,
                Naziv = x.Naziv,
                Trajanje = x.Trajanje
            }).ToList();

            viewModel.KolegijViewModel = listK;

            ViewBag.Kolegiji = listK;

            List<StudentViewModel> listS = _db.Studenti.Select(x => new StudentViewModel
            {
                StudentId = x.StudentId,
                Prezime = x.Prezime,
                Ime = x.Ime
            }).ToList();

            viewModel.StudentViewModel = listS;
            //viewModel.MyProperty.PopisId = popisId;

            ViewBag.Studenti = listS;
            viewModel.IdForPopis = popisId;

            //return PartialView("_partialShowOnePopisModalBody", model);
            return PartialView("_partialShowOnePopisModalBody", viewModel);
        }
    }
}
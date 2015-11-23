using DCS.BAL.Services;
using DCS.BAL.DataAccess;
using DCS.BAL.Interface;
using DCS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DCS.DAL;
using DCS.Common.Models;
using System.Linq.Expressions;
using AutoMapper;
using System.Web.Script.Serialization;



namespace DCSProject.Controllers
{
    
    public class ETOPlaybookController : Controller
    {
        //ETOPlaybookService obj;
        private IRepository<ETOPlaybook, ETOPlaybookModel> _ETOPlaybook;
        private IRepository<Project, ProjectModel> _Project;

        public ETOPlaybookController()
        {
            _ETOPlaybook = new Repository<ETOPlaybook, ETOPlaybookModel>();
            _Project = new Repository<Project, ProjectModel>();
        }

        public ActionResult Index()
        {
            var s = _ETOPlaybook.GetAll();
            return View(s);
        }
       // [Authorize(Users = "us/STDP18,us/CARRODO")]
        public ActionResult CreateETOPlaybook()
        {
            return View();
        }

        public JsonResult AddETOPlaybook(string ETOPlaybookName)
        {
            ETOPlaybook obj = new ETOPlaybook();
            obj.Name = ETOPlaybookName;
            obj.IsActive = true;
            obj.Created = DateTime.Now;
            _ETOPlaybook.Add(obj);
            int Id = _ETOPlaybook.GetTopETOPlaybookId();
            return Json(Id, JsonRequestBehavior.AllowGet);
        }
       // [Authorize(Users = "us/STDP18,us/CARRODO")]
        public ActionResult EditETOPlaybook()
        {
            return View();
        }

        public JsonResult GetETOPlaybook(int ETOPlaybookId)
        {
            Expression<Func<ETOPlaybook, bool>> filter = x => x.ID == ETOPlaybookId;
            ETOPlaybookModel model = _ETOPlaybook.Get(filter);
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }

       // [Authorize(Users = "us/STDP18,us/CARRODO")]
        public JsonResult GetETOPlayBookMile()
        {
            var mresult = EToPlayBook.GetETOPlayBookMileStones();

            IQueryable<Project> projects = _Project.GetAllList();

            var projects1 = projects.Select(x => new
            {
                ID = x.ID,
                Name = x.Name,
                PaybookId = x.EOPlaybookId
            });

            JsonResult json = new JsonResult();
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Dictionary<string, object> result = new Dictionary<string, object>();

            result["playbooks"] = mresult;
            result["projects"] = projects1;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

     //   [Authorize(Users = "us/STDP18,us/CARRODO")]
        public JsonResult DeleteETOPlaybook(Int32 EtoPlaybookId)
        {
            Expression<Func<ETOPlaybook, bool>> filter = x => x.ID == EtoPlaybookId;
            ETOPlaybook objETOPlaybook = _ETOPlaybook.GetEntity(filter);
            bool result = _ETOPlaybook.Delete(objETOPlaybook);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

     //   [Authorize(Users = "us/STDP18,us/CARRODO")]
        public JsonResult UpdateETOPlaybook(ETOPlaybookModel model)
        {
            _ETOPlaybook.UpdateETOPlaybook(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DisplayPart(int ETOPlaybookId)
        {
            if (ETOPlaybookId == 0)
            {
                return HttpNotFound();
            }
            Expression<Func<ETOPlaybook, bool>> filter = x => x.ID == ETOPlaybookId;
            ETOPlaybookModel model = _ETOPlaybook.Get(filter);
            return PartialView("~/Views/ETOPlaybook/EditETOPlaybook.cshtml", model);
        }
    }


}
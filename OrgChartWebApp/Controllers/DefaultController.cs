using OrgChartWebApp.DAL;
using OrgChartWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrgChartWebApp.Controllers
{
    public class DefaultController : Controller
    {
        OrgChartDatabaseEntities entities = new OrgChartDatabaseEntities();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read()
        {
            var nodes = entities.Employees.Select(p => new NodeModel{ id = p.Id, pid = p.ReportsTo, fullName = p.FullName });
            return Json(new { nodes = nodes }, JsonRequestBehavior.AllowGet);
        }

        
        public EmptyResult UpdateNode(NodeModel model)
        {
            var node = entities.Employees.First(p => p.Id == model.id);
            node.FullName = model.fullName;
            node.ReportsTo = model.pid;
            entities.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult RemoveNode(int id)
        {
            var node = entities.Employees.First(p => p.Id == id);
            entities.Employees.Remove(node);

            int? parentId = node.ReportsTo;

            var children = entities.Employees.Where(p => p.ReportsTo == node.Id);
            foreach (var child in children)
            {
                child.ReportsTo = node.ReportsTo;
            }

            entities.SaveChanges();
            return new EmptyResult();
        }

        public JsonResult AddNode(NodeModel model)
        {
            Employee employee = new Employee();
            employee.FullName = model.fullName;
            employee.ReportsTo = model.pid;
            entities.Employees.Add(employee);

            entities.SaveChanges();

            return Json(new { id = employee.Id }, JsonRequestBehavior.AllowGet);
        }
    }
}
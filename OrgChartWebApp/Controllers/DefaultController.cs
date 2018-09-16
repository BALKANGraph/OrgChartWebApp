using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrgChartWebApp.DAL;
using OrgChartWebApp.Models;

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
            var nodes = entities.Employees.Select(p => new { id = p.Id, fullName = p.FullName });
            var links = entities.Employees.Select(p => new { from = p.Id, to = p.ReportsTo });
            return Json(new { nodes = nodes, links = links }, JsonRequestBehavior.AllowGet);
        }

        public EmptyResult UpdateLink(LinkModel model)
        {
            var node = entities.Employees.First(p => p.Id == model.from);
            node.ReportsTo = model.to;
            entities.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult UpdateNode(NodeModel model)
        {
            var node = entities.Employees.First(p => p.Id == model.id);
            node.FullName = model.fullName;
            entities.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult DeleteNode(IdModel model)
        {
            var node = entities.Employees.First(p => p.Id == model.id);
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

        public JsonResult InsertNode(NodeModel model)
        {
            Employee employee = new Employee();
            employee.FullName = model.fullName;
            employee.ReportsTo = model.reportsTo;
            entities.Employees.Add(employee);

            entities.SaveChanges();

            return Json(new { id = employee.Id }, JsonRequestBehavior.AllowGet);
        }
    }
}
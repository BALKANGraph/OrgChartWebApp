# Tutorial: How to create OrgChart JS using MS SQL database and ASP.NET MVC

This tutorial teaches you the basics of building an OrgChart web app using MS SQL database and ASP.NET MVC web app.

## Get started
Start by installing [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=button+cta&utm_content=download+vs2017). Then, open Visual Studio.

On the Start page, select New Project. In the New project dialog box, select the Visual C# category on the left, then Web, and then select the ASP.NET Web Application (.NET Framework) project template. Name your project "OrgChartWebApp" and then choose OK.

![s1](https://balkangraph.com/js/img/s1.png)

In the New ASP.NET Web Application dialog, choose Empty select MVC checkbox and then choose OK.

![s1](https://balkangraph.com/js/img/s2.png)

In the solution explorer pane choose Manage NuGet Packages...

![s1](https://balkangraph.com/js/img/s3.png)

Install OrgChartJS package

![s1](https://balkangraph.com/js/img/s4.png)

Add newSQL Server Database OrgChartDatabase.mdf file in App_Data folder

![s1](https://balkangraph.com/js/img/s5.png)

Double click on OrgChartDatabase.mdf and choose New Query from the context menu

![s1](https://balkangraph.com/js/img/s6.png)

Execute the following query to create Employees table

```
CREATE TABLE [dbo].[Employees] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [ReportsTo] INT           NULL,
    [FullName]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

![s1](https://balkangraph.com/js/img/s7.png)

Create DAL folder and add OrgChartModel ADO.NET Entity Data Model then click Add

![s1](https://balkangraph.com/js/img/s8.png)

Click Next

![s1](https://balkangraph.com/js/img/s9.png)

Click Next

![s1](https://balkangraph.com/js/img/s10.png)

Click Next

![s1](https://balkangraph.com/js/img/s11.png)

Click Finish

![s1](https://balkangraph.com/js/img/s12.png)

Add DefaultController and view

![s1](https://balkangraph.com/js/img/s13.png)

Add 1 model cs files in Models folder


```
namespace OrgChartWebApp.Models
{
    public class NodeModel
    {
        public int id { get; set; }
        public int? pid { get; set; }
        public string fullName { get; set; }
    }
}
```

The controller should have Read, UpdateLink, UpdateNode, RemoveNode and AddNode action methods

```

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
```

And finaly the View  

```

@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>OrgChart</title>
    <script src="~/Scripts/OrgChart.js"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>

    <style>
        html, body {
            width: 100%;
            height: 100%;
            padding: 0;
            margin: 0;
            overflow: hidden;
        }

        #tree {
            width: 100%;
            height: 100%;
        }

        .field_0 {
            font-family: Impact;
        }
    </style>
</head>
<body>

    <div id="tree"></div>

    <script> 

        function updateNodeHandler(sender, node) {
            $.post("@Url.Action("UpdateNode")", node)
                .done(function () {
                    sender.updateNode(node);
                })
            return false;
        }

        function removeNodeHandler(sender, id) {
            $.post("@Url.Action("RemoveNode")", { id: parseInt(id) })
                .done(function () {
                    sender.removeNode(id);
                })
            return true;
        }

        function addNodeHandler(sender, node) {
            node.id = 0;
            node.pid = parseInt(node.pid);
            node.fullName = "John Smith";

            $.post("@Url.Action("AddNode")", node)
                .done(function (response) {
                    node.id = response.id;
                    sender.addNode(node);
                })

            return false;
        }

        $.get("@Url.Action("Read")").done(function (response) {
            var chart = new OrgChart(document.getElementById("tree"), {
                template: "luba",
                enableDragDrop: true,
                nodeMenu: {
                    edit: { text: "Edit" },
                    add: { text: "Add" },
                    remove: { text: "Remove" }
                },
                onUpdate: updateNodeHandler,
                onRemove: removeNodeHandler,
                onAdd: addNodeHandler,
                nodeBinding: {
                    field_0: "fullName"
                },
                nodes: response.nodes
            });
        });
    </script>
</body>
</html>

```

Press F5 and enjoy

[BALKANGraph](https://balkangraph.com)

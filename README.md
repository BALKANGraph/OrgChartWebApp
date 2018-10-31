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

![s1](https://balkangraph.com/js/img/s5.png)

![s1](https://balkangraph.com/js/img/s6.png)

![s1](https://balkangraph.com/js/img/s7.png)

![s1](https://balkangraph.com/js/img/s8.png)

![s1](https://balkangraph.com/js/img/s9.png)

![s1](https://balkangraph.com/js/img/s10.png)

![s1](https://balkangraph.com/js/img/s11.png)

![s1](https://balkangraph.com/js/img/s12.png)

![s1](https://balkangraph.com/js/img/s13.png)

```
CREATE TABLE [dbo].[Employees] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [ReportsTo] INT           NULL,
    [FullName]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

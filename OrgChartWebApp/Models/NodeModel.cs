using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrgChartWebApp.Models
{
    public class NodeModel
    {
        public int id { get; set; }
        public int? pid { get; set; }
        public string fullName { get; set; }
    }
}
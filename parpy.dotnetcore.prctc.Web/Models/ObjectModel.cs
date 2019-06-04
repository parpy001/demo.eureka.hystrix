using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parpy.dotnetcore.prctc.Web.Models
{
    public class ObjectModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public List<string> List { get; set; }

        public object Obj { get; set; }
        public DateTime Now { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{
    public class EventManager
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public EventManager(string name, DateTime date, string category)
        {
            Name = name;
            Date = date;
            Category = category;
        }
    }
}

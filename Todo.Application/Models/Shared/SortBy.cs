using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Models.Shared
{
    public class SortBy<T>
    {
        public string propertyName { get; set; }
        public SortBy(string sortByProperty) {
            var property = typeof(T).GetProperty(sortByProperty);
            if(property == null)
            {
                throw new Exception($"Property name {sortByProperty} does not exists");
            }
            this.propertyName = sortByProperty;
        }
    }
}

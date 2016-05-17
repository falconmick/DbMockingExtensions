using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMockingExtensions.ExampleDb.Models
{
    public class Revision
    {
        public int Id { get; set; }
        public string ChangeDescription { get; set; }
    }
}

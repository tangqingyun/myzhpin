using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrmExpressionTest
{
    public class Staff
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Code { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Deletion { get; set; }
    }
}

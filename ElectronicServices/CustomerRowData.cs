using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicServices
{
    public class CustomerRowData
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public decimal Pay { get; set; }
        public decimal Take { get; set; }
        public decimal Balance { get; set; }
    }
}

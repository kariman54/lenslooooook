using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Dal.Models
{
    public class Email
    {
        public int id { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
        public string To { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAppThree.Models
{
    public class PopisMainViewModel
    {
        public Popis PopisVM { get; set; }
        public IEnumerable<StudentViewModel> Studenti { get; set; }
        public IEnumerable<Kolegij> Kolegiji { get; set; }
    }
}
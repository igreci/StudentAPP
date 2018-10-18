using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAppThree.Models
{
    public class MainPageViewModel
    {
        public PopisViewModel MyProperty { get; set; }
        public IEnumerable<KolegijViewModel> KolegijViewModel { get; set; }
        public IEnumerable<PopisViewModel> PopisViewModels { get; set; }
        public IEnumerable<Popis> Popisi { get; set; }
        public IEnumerable<StudentViewModel> StudentViewModel { get; set; }
        public IEnumerable<Kolegij> Kolegiji { get; set; }
    }
}
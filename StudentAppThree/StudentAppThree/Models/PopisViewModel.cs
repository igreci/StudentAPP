using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAppThree.Models
{
    public class PopisViewModel
    {
        public int PopisId { get; set; }
        public int StudentId { get; set; }
        public int KolegijId { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Naziv { get; set; }

        public virtual Kolegij Kolegij { get; set; }
        public virtual Studenti Studenti { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAppThree.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Unesite ime")]
        [Description("First Name")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Unesite prezime")]
        [Description("Last Name")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unesite ime")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Unesite ispravan OIB")]
        [Description("Personal number, 11 digits exactly")]
        public string OIB { get; set; }

        [Required(ErrorMessage = "Unesite datum rođenja")]
        [DataType(DataType.Date)]
        [Description("Date of Birth")]
        public DateTime DatumRodenja { get; set; }

        public string PunoIme {
            get
            {
                return this.Ime + " " + this.Prezime;
            }
        }
    }
}
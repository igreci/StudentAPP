using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAppThree.Models
{
    public class Range1To3Validation : ValidationAttribute
    {
        StudentiDBEntities2 _db = new StudentiDBEntities2();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = (Popis)validationContext.ObjectInstance;

            var popis = _db.Popis.Where(x => x.KolegijId == obj.StudentId).ToList();

            if (popis.Count >= 1 && popis.Count <= 3)
            {
                return ValidationResult.Success;
            }
            else if (popis.Count <= 0)
            {
                return new ValidationResult("Najmanje 1 kolegij");
            }
            else
            {
                return new ValidationResult("Dozvoljeno najviše 3 kolegija");
            }
        }
    }
}
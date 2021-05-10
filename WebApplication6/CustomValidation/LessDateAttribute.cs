using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication6.CustomValidation
{
    public class LessDateAttribute:ValidationAttribute
    {
public LessDateAttribute():base("{0} Date should less than Current date")
        {

        }

        public override bool IsValid(object value)
        {
            DateTime propValue = Convert.ToDateTime(value);
            if (propValue <= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
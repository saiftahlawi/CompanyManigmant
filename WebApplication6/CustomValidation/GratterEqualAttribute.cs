using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication6.CustomValidation
{
    public class GratterEqualAttribute : ValidationAttribute//Abistract class
    {
        private readonly double MaxValue;
        public GratterEqualAttribute(double a) : base("{0} should greater than" + a)
        {
            MaxValue = a;
}
        public override bool IsValid(object value)
        {
            if(double.Parse(value.ToString())< MaxValue)
            {
                return false;
            }
            else
            {
                return true;
            }
           
        }
    }
}
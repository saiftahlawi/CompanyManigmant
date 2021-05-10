using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication6.CustomValidation;
namespace WebApplication6.Models
{
    [MetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {
        //Add methods or add new propert
        [Display(Name ="Confirm Email")]
        [Compare("Email", ErrorMessage ="Email is not matched")]
        [Required]
        public string ConfEmail { get; set; }

        [Required (ErrorMessage ="you must conferm password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password is not matched")]
        public string ConfPass { get; set; }
    }
    // Spicifies the metadata class to associate with  a data model  class.
    public class CompanyMetaData
    {
        // edit properites 

        [Display(Name ="ID")]
        public int CID { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string CName { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string CType { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }


        //[Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string CUrl { get; set; }

        [Display(Name ="Date")]
        [DisplayFormat (DataFormatString ="{0:D}",ApplyFormatInEditMode =false)]
        [DataType(DataType.Date)]
        [LessDate]
        public Nullable<System.DateTime> EDate { get; set; }


        [Required]
        //[StringLength(15,MinimumLength=8,ErrorMessage ="shuld between 8 to 15")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage ="password is not strong")]
        public string Password { get; set; }

      //  [Range(10000,1000000,ErrorMessage ="must between 10000 to 1000000")]
    //  [GratterEqual(10000)] //CustomValidation
        public Nullable<int> Capital { get; set; }
        public string Logo { get; set; }
    }
}
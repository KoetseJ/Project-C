//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webtest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int OrderDetails_Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Products { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string Street_Name { get; set; }
        public int House_number { get; set; }
        public string Zip_code { get; set; }
        public string Country { get; set; }
        public int Order_Number { get; set; }
    }
}

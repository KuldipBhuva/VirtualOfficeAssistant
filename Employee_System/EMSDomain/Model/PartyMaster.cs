//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMSDomain.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PartyMaster
    {
        public int PID { get; set; }
        public Nullable<int> PPID { get; set; }
        public string PartyName { get; set; }
        public string ContactPerson { get; set; }
        public Nullable<decimal> Mobile { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BillingAddress { get; set; }
        public string BillingName { get; set; }
        public Nullable<decimal> BillingPhone { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CompID { get; set; }
    }
}
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
    
    public partial class VehicleRepairing
    {
        public int ID { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<int> VID { get; set; }
        public Nullable<int> Repairing_TypeID { get; set; }
        public Nullable<System.DateTime> Rep_Date { get; set; }
        public string Amount_Paid { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> ExpType { get; set; }
    
        public virtual Company_master Company_master { get; set; }
        public virtual Masters_Tran Masters_Tran { get; set; }
        public virtual VehicleMaster VehicleMaster { get; set; }
    }
}

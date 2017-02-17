using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel
{
    public class ChequeReturnModel
    {
        public int Viewbagidformenu { get; set; }
        public int CRID { get; set; }
        [Required(ErrorMessage = "Client Required")]
        public string ClientName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        //[Required(ErrorMessage = "Contact Required")]
        public Nullable<decimal> Phone { get; set; }
        [Required(ErrorMessage = "Bank Required")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "Branch Required")]
        public string BankBranch { get; set; }
        [Required(ErrorMessage = "Cheque No. Required")]
        public string ChequeNo { get; set; }
        [Required(ErrorMessage = "Cheque Date Required")]
        public Nullable<System.DateTime> ChequeDate { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        public Nullable<decimal> Amount { get; set; }
        [Required(ErrorMessage = "Deposited Date Required")]
        public Nullable<System.DateTime> DepositedDate { get; set; }
        public string Remarks { get; set; }
        public string Action { get; set; }
        public string Attachment { get; set; }
        public Nullable<bool> Status { get; set; }

        public List<ChequeReturnModel> ListRetChq { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.TradeLicense;
using EMSDomain.ViewModel.Tenancy;
using EMSDomain.ViewModel.Vehicle;
using System.Data.SqlClient;

namespace EMSMethods
{
    [Serializable]
    public class ReportService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();
        DateTime today = System.DateTime.Now;

        //Employee
        public List<EmployeeItem> GetALL()
        {
            var data = (from em in Dbcontext.employee_master.DefaultIfEmpty()
                        join cm in Dbcontext.Company_master on em.Compid equals cm.id
                        join bm in Dbcontext.Branch_master on em.Branchid equals bm.id into br
                        from b in br.DefaultIfEmpty()
                        from pass in Dbcontext.EmployeePassports.Where(p => p.EmpId == em.id).DefaultIfEmpty()
                        from h in Dbcontext.EmployeeHealthCards.Where(h => h.EmpId == em.id && h.Type == "Health").DefaultIfEmpty()
                        from l in Dbcontext.EmployeeHealthCards.Where(l => l.EmpId == em.id && h.Type == "Labour").DefaultIfEmpty()
                        from e in Dbcontext.EmployeeHealthCards.Where(e => e.EmpId == em.id && h.Type == "Emirates ID").DefaultIfEmpty()
                        from d in Dbcontext.EmployeeHealthCards.Where(d => d.EmpId == em.id && h.Type == "Driving License").DefaultIfEmpty()
                        select new EmployeeItem()
                        {
                            CompDetails = new CompanyItem()
            {
                CompName = cm.CompName,
            },
                            BranchDetails = new BranchItem()
                            {
                                BranchName = (b == null ? string.Empty : b.BranchName)
                            },
                            PassDetails = new EmpPassportItem()
                            {
                                PassportNo = pass.PassportNo
                            },
                            HealthDetails = new EmpHealthItem()
                            {
                                CardNo = (h == null ? string.Empty : h.CardNo)
                            },
                            LabourDetails = new EmpHealthItem()
                            {
                                CardNo = (l == null ? string.Empty : l.CardNo)
                            },
                            EIDDetails = new EmpHealthItem()
                            {
                                CardNo = (e == null ? string.Empty : e.CardNo)
                            },
                            DLDetails = new EmpHealthItem()
                            {
                                CardNo = (d == null ? string.Empty : d.CardNo)
                            },
                            Empname = em.Empname,
                            Empno = (em == null ? string.Empty : em.Empno),
                            Mobile = (em == null ? string.Empty : em.Mobile),
                            FileNumber = (em == null ? string.Empty : em.FileNumber),
                            DOJ = em.DOJ
                        }).Distinct().ToList();
            return data;
        }
        //Driving License        
        public List<EmpHealthItem> getDlicenseRpt(DateTime date, int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.ExpiryDate == date && DL.Type == "Driving License" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getDLbyDay(string day, int cid)
        {
            var dd = Convert.ToDouble(day);
            DateTime withindays = DateTime.Today.AddDays(dd);
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Driving License" && DL.ExpiryDate >= today && DL.ExpiryDate <= withindays && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,
                        }).Distinct().ToList();
            return data;
        }

        public List<EmpHealthItem> getDlicenseAllRpt(int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Driving License" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        //Passport
        public List<EmpPassportItem> getPassportRpt(DateTime date, int cid)
        {
            var data = (from Pass in Dbcontext.EmployeePassports
                        join EM in Dbcontext.employee_master on Pass.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        join mt in Dbcontext.Masters_Tran on Pass.IssueCountry equals mt.Id into mast
                        from m in mast.DefaultIfEmpty()
                        where Pass.ExpiryDate == date && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpPassportItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CountryDetails = new EMSDomain.ViewModel.clsMasterData()
                          {
                              Name = (m == null ? string.Empty : m.Name)
                          },
                            PassportNo = Pass.PassportNo,
                            PlaceIssue = Pass.PlaceIssue,
                            IssueCountry = Pass.IssueCountry,
                            IssueDate = Pass.IssueDate,
                            RenewDate = Pass.RenewDate,
                            ExpiryDate = Pass.ExpiryDate

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpPassportItem> getPassportbyDay(string day, int cid)
        {
            var dd = Convert.ToDouble(day);
            DateTime withindays = DateTime.Today.AddDays(dd);
            var data = (from Pass in Dbcontext.EmployeePassports
                        join EM in Dbcontext.employee_master on Pass.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        join mt in Dbcontext.Masters_Tran on Pass.IssueCountry equals mt.Id into mast
                        from m in mast.DefaultIfEmpty()
                        where Pass.ExpiryDate >= today && Pass.ExpiryDate <= withindays && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpPassportItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CountryDetails = new EMSDomain.ViewModel.clsMasterData()
                          {
                              Name = (m == null ? string.Empty : m.Name)
                          },
                            PassportNo = Pass.PassportNo,
                            PlaceIssue = Pass.PlaceIssue,
                            IssueCountry = Pass.IssueCountry,
                            IssueDate = Pass.IssueDate,
                            RenewDate = Pass.RenewDate,
                            ExpiryDate = Pass.ExpiryDate

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpPassportItem> getPassportAllRpt(int cid)
        {
            var data = (from Pass in Dbcontext.EmployeePassports
                        join EM in Dbcontext.employee_master on Pass.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        join mt in Dbcontext.Masters_Tran on Pass.IssueCountry equals mt.Id into mast
                        from m in mast.DefaultIfEmpty()
                        where EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpPassportItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            PassportNo = Pass.PassportNo,
                            PlaceIssue = Pass.PlaceIssue,
                            IssueCountry = Pass.IssueCountry,
                            IssueDate = Pass.IssueDate,
                            RenewDate = Pass.RenewDate,
                            ExpiryDate = Pass.ExpiryDate,
                            CountryDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (m == null ? string.Empty : m.Name)
                            }

                        }).Distinct().ToList();
            return data;
        }
        //Health Card
        public List<EmpHealthItem> getHealthRpt(DateTime date, int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.ExpiryDate == date && DL.Type == "Health" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getHealthbyDay(string day, int cid)
        {
            var dd = Convert.ToDouble(day);
            DateTime withindays = DateTime.Today.AddDays(dd);

            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Health" && DL.ExpiryDate >= today && DL.ExpiryDate <= withindays && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,
                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getHealthbyAllRpt(int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Health" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,
                        }).Distinct().ToList();
            return data;
        }
        //Labour Card
        public List<EmpHealthItem> getLabourRpt(DateTime date, int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.ExpiryDate == date && DL.Type == "Labour" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getLabourbyDay(string day, int cid)
        {
            var dd = Convert.ToDouble(day);
            DateTime withindays = DateTime.Today.AddDays(dd);
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Labour" && DL.ExpiryDate >= today && DL.ExpiryDate <= withindays && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,
                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getLabourAllRpt(int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Labour" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        //Emirates ID
        public List<EmpHealthItem> getEIDRpt(DateTime date, int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.ExpiryDate == date && DL.Type == "Emirates ID" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getEIDbyDay(string day, int cid)
        {
            var dd = Convert.ToDouble(day);
            DateTime withindays = DateTime.Today.AddDays(dd);
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Emirates ID" && DL.ExpiryDate >= today && DL.ExpiryDate <= withindays && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,
                        }).Distinct().ToList();
            return data;
        }
        public List<EmpHealthItem> getEIDAllRpt(int cid)
        {
            var data = (from DL in Dbcontext.EmployeeHealthCards
                        join EM in Dbcontext.employee_master on DL.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where DL.Type == "Emirates ID" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpHealthItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            CardNo = DL.CardNo,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        //Visa
        public List<EmpVisaItem> getVisaRpt(DateTime date, int cid)
        {
            var data = (from V in Dbcontext.EmployeeVisas
                        join EM in Dbcontext.employee_master on V.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where V.ExpiryDate == date && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpVisaItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            VisaNo = V.VisaNo,
                            IssueDate = V.IssueDate,
                            ExpiryDate = V.ExpiryDate,
                            IssueCountry = V.IssueCountry

                        }).Distinct().ToList();
            return data;
        }
        public List<EmpVisaItem> getVisabyDay(string day, int cid)
        {
            var dd = Convert.ToDouble(day);
            DateTime withindays = DateTime.Today.AddDays(dd);
            var data = (from v in Dbcontext.EmployeeVisas
                        join EM in Dbcontext.employee_master on v.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where v.ExpiryDate >= today && v.ExpiryDate <= withindays && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpVisaItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            VisaNo = v.VisaNo,
                            IssueDate = v.IssueDate,
                            ExpiryDate = v.ExpiryDate,
                            IssueCountry = v.IssueCountry
                        }).Distinct().ToList();
            return data;
        }
        public List<EmpVisaItem> getVisaAllRpt(int cid)
        {
            var data = (from V in Dbcontext.EmployeeVisas
                        join EM in Dbcontext.employee_master on V.EmpId equals EM.id
                        join CM in Dbcontext.Company_master on EM.Compid equals CM.id
                        where EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpVisaItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            },
                            VisaNo = V.VisaNo,
                            IssueDate = V.IssueDate,
                            ExpiryDate = V.ExpiryDate,
                            IssueCountry = V.IssueCountry

                        }).Distinct().ToList();
            return data;
        }

        //Trade Licence

        public List<TradeLicenseItem> gettradelicenseRpt(DateTime date, int cid)
        {
            var data = (from DL in Dbcontext.Trade_License
                        join CM in Dbcontext.Company_master on DL.CompID equals CM.id
                        join CM1 in Dbcontext.Company_master on DL.SponsorID equals CM1.id
                        join m in Dbcontext.Masters_Tran on DL.EmiratesID equals m.Id into mas
                        from mt in mas.DefaultIfEmpty()
                        join mast in Dbcontext.Masters_Tran on DL.LicenseTypeID equals mast.Id into mast
                        from mmt in mast.DefaultIfEmpty()
                        //join CM1 in Dbcontext.Company_master on DL.id equals EM.Compid
                        where DL.ExpiryDate == date && DL.CompID == (cid == 0 ? DL.CompID : cid)
                        //&& DL.Type == "Driving License"
                        select new TradeLicenseItem()
                        {
                            Lno = DL.Lno,
                            CompDetails = new CompanyItem()
                             {
                                 CompName = CM.CompName
                             },
                            MEmiratesDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (mt == null ? string.Empty : mt.Name)
                            },
                            LicTypeDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (mmt == null ? string.Empty : mmt.Name)
                            },
                            //EmpDetails = new EmployeeItem()
                            //{
                            //    Empno = EM.Empno,
                            //    Empname = EM.Empname
                            //},
                            EmiratesID = DL.EmiratesID,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        public List<TradeLicenseItem> getTradeByEID(int EID, int cid)
        {
            var data = (from DL in Dbcontext.Trade_License
                        join CM in Dbcontext.Company_master on DL.CompID equals CM.id
                        join m in Dbcontext.Masters_Tran on DL.EmiratesID equals m.Id into mas
                        from mt in mas.DefaultIfEmpty()
                        join mast in Dbcontext.Masters_Tran on DL.LicenseTypeID equals mast.Id into mast
                        from mmt in mast.DefaultIfEmpty()
                        //join EM in Dbcontext.employee_master on CM.id equals EM.Compid
                        where DL.EmiratesID == EID && DL.CompID == (cid == 0 ? DL.CompID : cid)
                        select new TradeLicenseItem()
                        {
                            Lno = DL.Lno,
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            MEmiratesDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (mt == null ? string.Empty : mt.Name)
                            },
                            LicTypeDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (mmt == null ? string.Empty : mmt.Name)
                            },
                            //EmpDetails = new EmployeeItem()
                            //{
                            //    Empno = EM.Empno,
                            //    Empname = EM.Empname
                            //},
                            //SponsorID
                            EmiratesID = DL.EmiratesID,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }
        public List<TradeLicenseItem> getAllTradeRpt(int cid)
        {
            var data = (from DL in Dbcontext.Trade_License
                        join CM in Dbcontext.Company_master on DL.CompID equals CM.id
                        join CM1 in Dbcontext.Company_master on DL.SponsorID equals CM1.id into sp
                        from s in sp.DefaultIfEmpty()
                        join m in Dbcontext.Masters_Tran on DL.EmiratesID equals m.Id into mas
                        from mt in mas.DefaultIfEmpty()
                        join mast in Dbcontext.Masters_Tran on DL.LicenseTypeID equals mast.Id into mast
                        from mmt in mast.DefaultIfEmpty()
                        //join CM1 in Dbcontext.Company_master on DL.id equals EM.Compid

                        //&& DL.Type == "Driving License"
                        where DL.CompID == (cid == 0 ? DL.CompID : cid)
                        select new TradeLicenseItem()
                        {
                            Lno = DL.Lno,

                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            MEmiratesDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (mt == null ? string.Empty : mt.Name)
                            },
                            LicTypeDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (mmt == null ? string.Empty : mmt.Name)
                            },
                            //EmpDetails = new EmployeeItem()
                            //{
                            //    Empno = EM.Empno,
                            //    Empname = EM.Empname
                            //},
                            EmiratesID = DL.EmiratesID,
                            IssueDate = DL.IssueDate,
                            ExpiryDate = DL.ExpiryDate,

                        }).Distinct().ToList();
            return data;
        }

        //Tenacny 

        public List<TenancyItem> gettenacny(int cid)
        {
            var data = (from DL in Dbcontext.TenancyMasters
                        join mast in Dbcontext.Masters_Tran on DL.TnTypeId equals mast.Id into mm
                        from m in mm.DefaultIfEmpty()
                        //join CM in Dbcontext.Company_master on DL.CompID equals CM.id
                        //join EM in Dbcontext.employee_master on CM.id equals EM.Compid
                        //where DL.ExpiryDate == date
                        //&& DL.Type == "Driving License"
                        where DL.TnTypeCompId == (cid == 0 ? DL.TnTypeCompId : cid)
                        select new TenancyItem()
                        {
                            TnNo = DL.TnNo,
                            TDate = DL.TDate,
                            Period = DL.Period,
                            LandLord = DL.LandLord,
                            TnType = DL.TnType,
                            FromDate = DL.FromDate,
                            ToDate = DL.ToDate,
                            Rent = DL.Rent,
                            TTDetails = new EMSDomain.ViewModel.clsMasterData()
                            {
                                Name = (m == null ? string.Empty : m.Name)
                            }


                        }).Distinct().ToList();
            return data;
        }

        //Vehical 

        public List<VehicleItem> getvehicalDetails(int cid)
        {
            var data = (from VD in Dbcontext.VehicleMasters
                        join CM in Dbcontext.Company_master on VD.CompID equals CM.id
                        where VD.Status == "Active" && VD.CompID == (cid == 0 ? VD.CompID : cid)
                        select new VehicleItem()
                        {
                            VehicleName = VD.VehicleName,
                            VehicleNo = VD.VehicleNo,
                            ModelName = VD.ModelName,
                            SalikNo = VD.SalikNo,
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            Reg_Date = VD.Reg_Date,
                            Reg_Exp_Date = VD.Reg_Exp_Date


                        }).Distinct().ToList();
            return data;
        }

        //Vehical insu

        //public List<VehicleItem> getvehicalInsDetails()
        //{
        //    var data = (from VD in Dbcontext.VehicleMasters
        //                join CM in Dbcontext.Company_master on VD.CompID equals CM.id

        //                select new VehicleItem()
        //                {
        //                    VehicleName = VD.VehicleName,
        //                    VehicleNo = VD.VehicleNo,
        //                    ModelName = VD.ModelName,
        //                    CompDetails = new CompanyItem()
        //                    {
        //                        CompName = CM.CompName
        //                    },
        //                    Reg_Date = VD.Reg_Date,
        //                    Reg_Exp_Date = VD.Reg_Exp_Date


        //                }).Distinct().ToList();
        //    return data;
        //}

        public List<EmployeeItem> GetData(int cid)
        {
            var comp = new SqlParameter("@cid", cid);
            List<EmployeeItem> empList = Dbcontext.Database.SqlQuery<EmployeeItem>("exec GetEmployeeData @cid", comp).ToList<EmployeeItem>();
            return empList;
        }
    }
}

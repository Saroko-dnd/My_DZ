using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;

namespace MapDBContext
{
    public static class EntityConnector
    {
        public static string ConnectionStringName = String.Empty;

        public static BanksDBContext StaticBanksDBContext;

        public static BankBranch LoadSelectedObjectData(string BankBranchName)
        {
            BankBranch SelectedBranch = StaticBanksDBContext.BankBranches.Where(res => res.BranchName == BankBranchName).
                First();
            return SelectedBranch;
        }

        public static List<string> GetListOfServices()
        {
            List<string> ListOfServices = new List<string>();
            foreach (Service CurrentService in StaticBanksDBContext.Services)
            {
                ListOfServices.Add(CurrentService.Servise);
            }
            return ListOfServices;
        }

        public static BankBranch GetNecessaryBankRates(string currency, bool Best, bool Buy)
        {
            switch (currency)
            {
                case "EURO":
                    if (Best && Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderBy(res => res.RelatedRates.EUROBuy).
                            First();
                    if (!Best && Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderByDescending(res => res.RelatedRates.EUROBuy).
                            First();
                    if (Best && !Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderByDescending(res => res.RelatedRates.EUROSell).
                            First();
                    if (!Best && !Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderBy(res => res.RelatedRates.EUROSell).
                            First();
                    break;
                case "RUB":
                    if (Best && Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderBy(res => res.RelatedRates.RuBuy).
                            First();
                    if (!Best && Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderByDescending(res => res.RelatedRates.RuBuy).
                            First();
                    if (Best && !Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderByDescending(res => res.RelatedRates.RuSell).
                            First();
                    if (!Best && !Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderBy(res => res.RelatedRates.RuSell).
                            First();
                    break;
                case "USD":
                    if (Best && Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderBy(res => res.RelatedRates.USDBuy).
                            First();
                    if (!Best && Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderByDescending(res => res.RelatedRates.USDBuy).
                            First();
                    if (Best && !Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderByDescending(res => res.RelatedRates.USDSell).
                            First();
                    if (!Best && !Buy)
                        return StaticBanksDBContext.BankBranches.Where(res => res.RelatedRates != null).OrderBy(res => res.RelatedRates.USDSell).
                            First();
                    break;
                default:
                    break;
            }
            return new BankBranch();
        }

        public static BankBranch GetNearestBranch(double LatitudeSearch, double LongitudeSearch)
        {
            List<BranchWithSumDif> SumOfDif = new List<BranchWithSumDif>();

            foreach (BankBranch CurrentBranch in StaticBanksDBContext.BankBranches)
            {
                SumOfDif.Add(new BranchWithSumDif()
                {
                    BranchName = CurrentBranch.BranchName,
                    SumDif = Calculations.distance(LatitudeSearch, LongitudeSearch, (double)CurrentBranch.MapLocation.Latitude, (double)CurrentBranch.MapLocation.Longitude, 'K')
                });
            }
            BranchWithSumDif NearestBranchName = SumOfDif.OrderBy(res => res.SumDif).First();
            return StaticBanksDBContext.BankBranches.Where(res => res.BranchName == NearestBranchName.BranchName).
                First();
        }

        public static void AddNewBankIfNecessary (string NewBankName)
        {
            if (!StaticBanksDBContext.Banks.Any(res => res.BankName == NewBankName))
            {
                StaticBanksDBContext.Banks.Add(new Bank() { BankName = NewBankName });
                StaticBanksDBContext.SaveChanges();
            }
        }

    }

    public class BranchWithSumDif
    {
        public double SumDif;
        public string BranchName;
    }
}

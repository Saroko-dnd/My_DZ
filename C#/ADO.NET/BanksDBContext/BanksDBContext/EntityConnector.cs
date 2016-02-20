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
            //если поместить (Sender as Label).Content.ToString() в запрос то возращает исключение
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
                        return StaticBanksDBContext.BankBranches.OrderBy(res => res.RelatedRates.EUROBuy).
                            First();
                    if (!Best && Buy)
                        return StaticBanksDBContext.BankBranches.OrderByDescending(res => res.RelatedRates.EUROBuy).
                            First();
                    if (Best && !Buy)
                        return StaticBanksDBContext.BankBranches.OrderByDescending(res => res.RelatedRates.EUROSell).
                            First();
                    if (!Best && !Buy)
                        return StaticBanksDBContext.BankBranches.OrderBy(res => res.RelatedRates.EUROSell).
                            First();
                    break;
                case "RUB":
                    if (Best && Buy)
                        return StaticBanksDBContext.BankBranches.OrderBy(res => res.RelatedRates.RuBuy).
                            First();
                    if (!Best && Buy)
                        return StaticBanksDBContext.BankBranches.OrderByDescending(res => res.RelatedRates.RuBuy).
                            First();
                    if (Best && !Buy)
                        return StaticBanksDBContext.BankBranches.OrderByDescending(res => res.RelatedRates.RuSell).
                            First();
                    if (!Best && !Buy)
                        return StaticBanksDBContext.BankBranches.OrderBy(res => res.RelatedRates.RuSell).
                            First();
                    break;
                case "USD":
                    if (Best && Buy)
                        return StaticBanksDBContext.BankBranches.OrderBy(res => res.RelatedRates.USDBuy).
                            First();
                    if (!Best && Buy)
                        return StaticBanksDBContext.BankBranches.OrderByDescending(res => res.RelatedRates.USDBuy).
                            First();
                    if (Best && !Buy)
                        return StaticBanksDBContext.BankBranches.OrderByDescending(res => res.RelatedRates.USDSell).
                            First();
                    if (!Best && !Buy)
                        return StaticBanksDBContext.BankBranches.OrderBy(res => res.RelatedRates.USDSell).
                            First();
                    break;
                default:
                    break;
            }
            return new BankBranch();
        }

        public static BankBranch GetNearestBranch(double LatitudeSearch,double LongitudeSearch)
        {

            //lattitude юг -90 север 90 Longitude запад -180 восток 180
            double BufForLatitudeSearch = 90 + LatitudeSearch;
            double BufForLongitudeSearch = 180 + LongitudeSearch;
            List<BranchWithSumDif> SumOfDif = new List<BranchWithSumDif>();

            foreach (BankBranch CurrentBranch in StaticBanksDBContext.BankBranches)
            {
                double BufForLatitude = 90 + (double)CurrentBranch.MapLocation.Latitude;
                double BufForLongitude = 180 + (double)CurrentBranch.MapLocation.Longitude;
                if (BufForLatitude > BufForLatitudeSearch && BufForLongitude > BufForLongitudeSearch)
                {
                    SumOfDif.Add(new BranchWithSumDif()
                    {
                        BranchName = CurrentBranch.BranchName,
                        SumDif = (BufForLatitude - BufForLatitudeSearch) +
                        (BufForLongitude - BufForLongitudeSearch)
                    });
                }
                else if ( BufForLatitude < BufForLatitudeSearch && BufForLongitude < BufForLongitudeSearch)
                {
                    SumOfDif.Add(new BranchWithSumDif()
                    {
                        BranchName = CurrentBranch.BranchName,
                        SumDif = (BufForLatitudeSearch - BufForLatitude) +
                        (BufForLongitudeSearch - BufForLongitude)
                    });
                }
                else if (BufForLatitude > BufForLatitudeSearch && BufForLongitude < BufForLongitudeSearch)
                {
                    SumOfDif.Add(new BranchWithSumDif()
                    {
                        BranchName = CurrentBranch.BranchName,
                        SumDif = (BufForLatitude - BufForLatitudeSearch) +
                        (BufForLongitudeSearch - BufForLongitude)
                    });
                }
                else if (BufForLatitude < BufForLatitudeSearch && BufForLongitude > BufForLongitudeSearch)
                {
                    SumOfDif.Add(new BranchWithSumDif()
                    {
                        BranchName = CurrentBranch.BranchName,
                        SumDif = (BufForLatitudeSearch - BufForLatitude) +
                        (BufForLongitude - BufForLongitudeSearch)
                    });
                }
            }
            BranchWithSumDif NearestBranchName = SumOfDif.OrderBy(res => res.SumDif).First();
            return StaticBanksDBContext.BankBranches.Where(res => res.BranchName == NearestBranchName.BranchName).
                First();
        }

        public static bool AddNewBankIfNecessary(string NewBankName)
        {
            if (!StaticBanksDBContext.Banks.Any(res => res.BankName == NewBankName))
            {
                StaticBanksDBContext.Banks.Add(new Bank() { BankName = NewBankName });
                StaticBanksDBContext.SaveChanges();
                return true;
            }
            return false;
        }

    }

    public class BranchWithSumDif
    {
        public double SumDif;
        public string BranchName;
    }
}

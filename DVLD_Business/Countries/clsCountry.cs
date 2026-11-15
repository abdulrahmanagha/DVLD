using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsCountry
    {
        enum enMode { AddNew = 0, Update = 1 };
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        private enMode Mode { get; set; }

        private clsCountry(int CountryId, string CountryName)
        {
            this.CountryId = CountryId;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }

        public clsCountry()
        {
            this.CountryId = -1;
            this.CountryName = string.Empty;
            Mode = enMode.AddNew;
        }

        private bool _AddNewCountry()
        {
            this.CountryId = clsCountryData.AddNewCountry(CountryName);
            return (CountryId != -1);
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(CountryId, CountryName);
        }

        public static clsCountry Find(int countryId)
        {
            string CountryName = null;
            if (clsCountryData.GetCountryInfoById(countryId, ref CountryName))
            {
                return new clsCountry(countryId, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryId = -1;
            if (clsCountryData.GetCountryInfoByName(ref CountryId, CountryName))
            {
                return new clsCountry(CountryId, CountryName);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewCountry())
                        {
                            this.Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enMode.Update:
                    {
                        if (_UpdateCountry())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
            }

            return false;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static bool DeleteCountry(int CountryId)
        {
            return clsCountryData.DeleteCountry(CountryId);
        }

        public static bool IsCountryExist(int countryId)
        {
            return clsCountryData.IsExistCountry(countryId);
        }

        public static bool IsCountryExist(string countryName)
        {
            return clsCountryData.IsExistCountry(countryName);
        }
    }

}

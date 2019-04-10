using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CatsApp.Models
{
    public class DataProviderFactory : IDataProviderFactory
    {
        public ICatDataProvider GetCatsDataProvider()
        {
            string dbProv = GetDbConfigKeyVal();

            if(!string.IsNullOrEmpty(dbProv))
            {
               switch(dbProv.ToUpper())
                {
                    case "XML":
                        return new XmlCatDataProvider();
                    case "SQL":
                        return new SqlCatDataProvider();
                    //case "CSV":
                     //   new CsvCatDataProvider();
                    default:
                        return new XmlCatDataProvider();
                }
            }

            throw new Exception("Specified DataProvider Not found or Implemented");
        }

        private string GetDbConfigKeyVal()
        {
            string retVal = ConfigurationManager.AppSettings["CatsDataProvider"];
            return string.IsNullOrEmpty(retVal) ? string.Empty : retVal;
        }
    }
}
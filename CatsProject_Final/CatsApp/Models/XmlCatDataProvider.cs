using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using CatsWebApi.Models;

namespace CatsApp.Models
{
    public class XmlCatDataProvider : ICatDataProvider
    {
        private List<Cat> catsInitData = new List<Cat>();
        private string pathToCatsXml;
        public XmlCatDataProvider()
        {
            if (string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["PathToCatsXml"]))
            {
                pathToCatsXml = "~/App_Data/Cats.xml";
            }
            else
            {
                pathToCatsXml = System.Web.Configuration.WebConfigurationManager.AppSettings["PathToCatsXml"];
            }

            
            catsInitData = deserializeIntoList();
        }

        public bool AddNewCat(Cat catToAdd)
        {
            var newId = catsInitData.Max(p => p.CatId) + 1;
            catToAdd.CatId = newId;

            catsInitData.Add(catToAdd);
            //persist Data to DataStore (xml file)
            SerializeListToXml(catsInitData);

            return true;
        }

        public bool DeleteCat(int CatId)
        {
            var catToDelete = catsInitData.FirstOrDefault((p) => p.CatId == CatId);
            if (catsInitData.Remove(catToDelete))
            {
                SerializeListToXml(catsInitData);
            }
            else
            {
                return false;
            }

            return true;
        }

        public List<Cat> GetCats()
        {
            return deserializeIntoList();
        }

        private List<Cat> deserializeIntoList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cats), new XmlRootAttribute("Cats"));
            string strReader = File.ReadAllText(HttpContext.Current.Server.MapPath(pathToCatsXml));
            StringReader stringReader = new StringReader(strReader);
            var list = (Cats)serializer.Deserialize(stringReader);

            return list.Items;
        }

        private void SerializeListToXml(List<Cat> catsList)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Cats));
            var listCats = new Cats
            {
                Items = catsList
            };
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(pathToCatsXml), FileMode.Create))
            {
                ser.Serialize(fs, listCats);
            }
        }

        public bool UpdateCat(Cat cat)
        {
            var catToUpdate = catsInitData.FirstOrDefault((p) => p.CatId == cat.CatId);

            copyCatData(cat, catToUpdate);

            //persist Data to DataStore (xml file)
            SerializeListToXml(catsInitData);
            return true;
        }


        private bool copyCatData(Cat copyFrom, Cat copyTo)
        {
            if (copyFrom != null && copyTo != null)
            {
                //copyTo.CatId = copyFrom.CatId;
                copyTo.CatAge = copyFrom.CatAge;
                copyTo.CatDob = copyFrom.CatDob;
                copyTo.CatGender = copyFrom.CatGender;
                copyTo.CatName = copyFrom.CatName;
                copyTo.CatBreed = copyFrom.CatBreed;
            }
            return true;
        }

        ////MockData for Testing....
        //private List<Cat> CreateCatsData(bool createXml = false)
        //{
        //    Cat[] cats = new Cat[]{
        // new Cat { CatId = 1, CatName = "Silly", CatDob =
        //    DateTime.Parse("2017-12-11").ToShortDateString(), CatGender='F', CatBreed="breed1"  },
        // new Cat { CatId = 2, CatName = "Fiesty", CatDob =
        //    DateTime.Parse("2018-10-09").ToShortDateString(), CatGender='M',  CatBreed="breed2"},
        //  new Cat { CatId = 3, CatName = "Robbie", CatDob =
        //    DateTime.Parse("2016-09-16").ToShortDateString(), CatGender='M',  CatBreed="breed3"},
        //  new Cat { CatId = 4, CatName = "Tutsie", CatDob =
        //    DateTime.Parse("2013-03-03").ToShortDateString(), CatGender='F', CatBreed="breed4"}
        //};
        //    if (createXml)
        //    {
        //        XmlSerializer ser = new XmlSerializer(typeof(Cats));
        //        var listCats = new Cats();
        //        listCats.Items = CreateCatsData();
        //        using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Cats.xml"), FileMode.Create))
        //        {
        //            ser.Serialize(fs, listCats);
        //        }
        //    }

        //    return cats.ToList<Cat>();
        //    //return cats;
        //}

    }
}
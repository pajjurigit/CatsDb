using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CatsWebApi.Models
{
    public class Cat
    {
        [XmlElement("CatId")]
        public int CatId { get; set; }
        [XmlElement("CatName")]
        public string CatName { get; set; }
        [XmlElement("CatAge")]
        public int CatAge { get; set; }
        [XmlElement("CatGender")]
        public char CatGender { get; set; }

        //[XmlElement("CatDob")]
        //public string CatDob { get; set; }

        private DateTime _catDob;
        [XmlElement("CatDob")]
        public string CatDob
        {
            get
            {
                if (_catDob != null)
                    return _catDob.ToString("yyyy-MM-dd");
                else
                    return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value)) _catDob = Convert.ToDateTime(value);
            }
        }

        [XmlElement("CatBreed")]
        public string CatBreed { get; set; }
    }

    [XmlRoot("Cats")]
    public class Cats
    {
        public Cats() { Items = new List<Cat>(); }
        [XmlElement("Cat")]
        public List<Cat> Items { get; set; }
    }
}
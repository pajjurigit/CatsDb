using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CatsWebApi.Models;

namespace CatsApp.Models
{
    public class SqlCatDataProvider : ICatDataProvider
    {
        //private string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["CatsDbConnStr"].ToString();
        private string connStr = ConfigurationManager.ConnectionStrings["CatsDbConnStr"].ConnectionString;

        public bool AddNewCat(Cat catToAdd)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string commandText = "INSERT INTO Cats(CatName, CatDob, CatGender, CatBreed) output INSERTED.CatId VALUES(@name,@dob,@gender,@breed)";
                con.Open();
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.Parameters.AddWithValue("@name", catToAdd.CatName);
                cmd.Parameters.AddWithValue("@dob", catToAdd.CatDob);
                cmd.Parameters.AddWithValue("@gender", catToAdd.CatGender);
                cmd.Parameters.AddWithValue("@breed", catToAdd.CatBreed);
                var newIdObj = cmd.ExecuteScalar();
                if (newIdObj != null)
                {
                    int newId = Convert.ToInt32(newIdObj);
                    catToAdd.CatId = newId;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteCat(int CatId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string commandText = "Delete From Cats where CatId = " + CatId;
                con.Open();
                SqlCommand cmd = new SqlCommand(commandText, con);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Cat> GetCats()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Cats", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt.ToSafeList<Cat>();
        }

        public bool UpdateCat(Cat catToUpdate)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string commandText = "UPDATE Cats SET CatName=@name, CatDob=@dob, CatGender=@gender, CatBreed=@breed  WHERE CatId = " + catToUpdate.CatId;
                con.Open();
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.Parameters.AddWithValue("@name", catToUpdate.CatName);
                cmd.Parameters.AddWithValue("@dob", catToUpdate.CatDob);
                cmd.Parameters.AddWithValue("@gender", catToUpdate.CatGender);
                cmd.Parameters.AddWithValue("@breed", catToUpdate.CatBreed);
                if (cmd.ExecuteNonQuery() > 0) return true;
            }
            return false;
        }
    }
}
using PrismWPFDemo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PrismWPFDemo.DbContext
{
    public class UserDbContext
    {
        public static List<GenderModel> genderList = new List<GenderModel>();
        public static List<CountryModel> countryList = new List<CountryModel>();
        public static List<StateModel> stateList = new List<StateModel>();
        public static List<CityModel> cityList = new List<CityModel>();
        private static readonly string conString = ConfigurationManager.ConnectionStrings["SampleDBConnection"].ConnectionString;
        public static void fetchAllData()
        {
            genderList.Clear();
            countryList.Clear();
            stateList.Clear();
            cityList.Clear();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = "select * from MainCity;Select * from MainCountry;Select * from MainState;Select * from EFDBFGender";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();
                adapter.TableMappings.Add("Table", "MainCity");
                adapter.TableMappings.Add("Table1", "MainCountry");
                adapter.TableMappings.Add("Table2", "MainState");
                adapter.TableMappings.Add("Table3", "EFDBFGender");

                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables["MainCity"].Rows)
                {
                    CityModel nob = new CityModel(Convert.ToInt32(row[0]), row[1].ToString());
                    cityList.Add(nob);
                }
                foreach (DataRow row in dataSet.Tables["EFDBFGender"].Rows)
                {
                    GenderModel nob = new GenderModel(Convert.ToInt32(row[0]), row[1].ToString());
                    genderList.Add(nob);
                }
                foreach (DataRow row in dataSet.Tables["MainCountry"].Rows)
                {
                    CountryModel nob = new CountryModel(Convert.ToInt32(row[0]), row[1].ToString());
                    countryList.Add(nob);
                }
                foreach (DataRow row in dataSet.Tables["MainState"].Rows)
                {
                    StateModel nob = new StateModel(Convert.ToInt32(row[0]), row[1].ToString());
                    stateList.Add(nob);
                }
            }
        }

        public static List<StateModel> GetStateList(int countryId)
        {
            List<StateModel> list = new List<StateModel>();
            string query = "Select * from MainState where countryid=" + countryId;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    StateModel nob = new StateModel(Convert.ToInt32(row[0]), row[1].ToString());
                    list.Add(nob);
                }
            }
            return list;
        }

        public static List<CityModel> GetCityList(int stateId)
        {
            List<CityModel> list = new List<CityModel>();
            string query = "Select * from MainCity where stateid=" + stateId;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    CityModel nob = new CityModel(Convert.ToInt32(row[0]), row[1].ToString());
                    list.Add(nob);
                }
            }
            return list;
        }

        #region UserRelatedSQL
        public static List<UsersModel> fetchUsers(int? id)
        {
            List<UsersModel> users = new List<UsersModel>();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = "SELECT m.*,con.country as CountryString," +
                    "st.StateName as StateString,ci.City as CityString," +
                    "gen.gender as GenderString " +
                    "FROM MainUsers as m,MainCountry as con," +
                    "MainState as st,MainCity as ci,EFDBFGender as gen " +
                    "WHERE m.Country = con.id AND m.State = st.id " +
                    "AND m.City = ci.id AND m.Gender = gen.id ";
                if (id != null)
                {
                    query += " AND m.id='" + id + "'";
                }
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    UsersModel user = new UsersModel();
                    user.FirstName = row["FirstName"].ToString();
                    user.LastName = row["LastName"].ToString();
                    user.Address = row["Address"].ToString();
                    user.country = Convert.ToInt32(row["country"]);
                    user.Gender = Convert.ToInt32(row["gender"]);
                    user.city = Convert.ToInt32(row["city"]);
                    user.state = Convert.ToInt32(row["state"]);
                    user.Id = Convert.ToInt32(row["id"]);
                    user.GenderString = row["GenderString"].ToString();
                    user.CountryString = row["CountryString"].ToString();
                    user.StateString = row["StateString"].ToString();
                    user.CityString = row["CityString"].ToString();
                    users.Add(user);
                }
            }
            return users;
        }

        public static void insertUser(UsersModel model)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into MainUsers(FirstName,LastName,Gender,Country,State,City,Address) " +
                    "values (@fname,@lname,@gen,@con,@state,@city,@address)";
                command.Parameters.AddWithValue("@fname", model.FirstName);
                command.Parameters.AddWithValue("@lname", model.LastName);
                command.Parameters.AddWithValue("@gen", model.Gender);
                command.Parameters.AddWithValue("@con", model.country);
                command.Parameters.AddWithValue("@state", model.state);
                command.Parameters.AddWithValue("@city", model.city);
                command.Parameters.AddWithValue("@address", model.Address);
                command.ExecuteNonQuery();
            }
        }
        public static void updateUser(UsersModel model)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE MainUsers SET FirstName=@fname,LastName=@lname,Gender=@gen,Country=@con,State=@state," +
                    "city=@city,address=@address where id=@id";
                command.Parameters.AddWithValue("@fname", model.FirstName);
                command.Parameters.AddWithValue("@lname", model.LastName);
                command.Parameters.AddWithValue("@gen", model.Gender);
                command.Parameters.AddWithValue("@con", model.country);
                command.Parameters.AddWithValue("@state", model.state);
                command.Parameters.AddWithValue("@city", model.city);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@id", model.Id);
                command.ExecuteNonQuery();
            }
        }
        public static void deleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand();
                conn.Open();
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE from MainUsers where id=@id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}

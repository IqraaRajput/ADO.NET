using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ADO.NET.Models;
using System.Drawing;

namespace ADO.NET.Controllers
{
    public class Birdsrepository
    {
        private static SqlConnection _con;
        private  static SqlCommand _cmd;
        private  SqlDataAdapter da;
        private  DataTable dt = new DataTable();
        private void connection()
        {
            string cons = "Data Source=DESKTOP-UO5LR5V;Initial Catalog=Classlecture;Integrated Security=True";//"User ID=sa;Password=Alaska@123;";
            _con = new SqlConnection(cons);
        }
        public List<BirdsModel> DB()
        {
            
            connection();
            //SqlConnection _Con = new SqlConnection()
            List<BirdsModel> BirdsList = new List<BirdsModel>();
            
            _con.Open();

            _cmd = new SqlCommand("select * from Birds", _con);

            SqlDataAdapter sda = new SqlDataAdapter(_cmd);

            sda.Fill(dt);


            _cmd.Dispose();

            _con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                BirdsList.Add(

                    new BirdsModel
                    {

                        ID = Convert.ToInt32(dr["ID"]),
                        BirdName = Convert.ToString(dr["BirdName"]),
                        TypeofBird = Convert.ToString(dr["TypeofBird"]),
                        ScientificName = Convert.ToString(dr["ScientificName"])

                    }
                    );
            }

            return BirdsList;


        }
        public bool UpdateBirds(BirdsModel NEWENTRY)
        {
            connection();

            
            _con.Open();

            _cmd = new SqlCommand("AddNewBirds", _con);
            _cmd.CommandType = CommandType.StoredProcedure;
            //_cmd.Parameters.AddWithValue("@ID", NEWENTRY.ID);
            _cmd.Parameters.AddWithValue("@NAME", NEWENTRY.BirdName);
            _cmd.Parameters.AddWithValue("@Type", NEWENTRY.TypeofBird);
            _cmd.Parameters.AddWithValue("@SCINAME", NEWENTRY.ScientificName);
            _con.Close();
            int i = _cmd.ExecuteNonQuery();
            
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }

            
        }

        public bool EditBirds(BirdsModel EditENTRY)
        {
            connection();
            _con.Open();

            _cmd = new SqlCommand("UpdateBirds", _con);
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.AddWithValue("@Id", EditENTRY.ID);
            _cmd.Parameters.AddWithValue("@Name", EditENTRY.BirdName);
            _cmd.Parameters.AddWithValue("@Type", EditENTRY.TypeofBird);
            _cmd.Parameters.AddWithValue("@Scientific", EditENTRY.ScientificName);

            int i = _cmd.ExecuteNonQuery();
            _con.Close();
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }


        }
        public bool DeleteBirds(int Id)
        {

            connection();
            SqlCommand _com = new SqlCommand("DeleteBirdById", _con);

            _com.CommandType = CommandType.StoredProcedure;
            _com.Parameters.AddWithValue("@Id", Id);

            _con.Open();
            int i = _com.ExecuteNonQuery();
            _con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }


}

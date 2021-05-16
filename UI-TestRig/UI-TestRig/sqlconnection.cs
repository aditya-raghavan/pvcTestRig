using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace UI_TestRig
{
    class sqlconnection
    {
        SqlConnection conn;
        SqlCommand command;
        String constring = "Server=LAPTOP-P2L7DRK2;Database=uitestrig;integrated security = true;";

        public void insertData(String user, String model, String op, String diodecode, String customercode, String additionalcode, String jbserialno, String batchno)
        {
            conn = new SqlConnection(constring);
            conn.Open();
            String query = "insert into testriginput(username,model,operator,diodecode,customercode,additionalcode,jbno,batchnumber,date,time)values(" +
                "'" + user.Trim() + "'," +
                "'" + model.Trim() + "'," +
                "'" + op.Trim() + "'," +
                "'" + diodecode.Trim() + "'," +
                "'" + customercode.Trim() + "'," +
                "'" + additionalcode.Trim() + "'," +
                "'" + jbserialno.Trim() + "'," +
                "'" + batchno.Trim() + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd").Trim() + "'," +
                "'" + DateTime.Now.ToString("HH:mm:ss tt").Trim() + "');";

            command = new SqlCommand(query, conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
            finally
            {
                conn.Close();
            }
               
        }
    }
}

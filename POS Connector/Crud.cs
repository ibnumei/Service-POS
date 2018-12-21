using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace POS_Connector
{
    class Crud
    {
        Connection cont = new Connection();
        Connection4 ckon4 = new Connection4();
        //=================METNHOD FOR CRUD DATA=======================================================
        public void NonReturn(String query)
        {
            //cont.con.Open();
            //cont.cmd = new MySqlCommand(query, cont.con);
            //cont.cmd.ExecuteNonQuery();
            //cont.con.Close();
            ckon4.con4.Open();
            ckon4.cmd4 = new MySqlCommand(query, ckon4.con4);
            ckon4.cmd4.ExecuteNonQuery();
            ckon4.con4.Close();
        }
        //=============================================================================================
        public void NonReturn2(String query)
        {
            cont.con.Open();
            cont.cmd = new MySqlCommand(query, cont.con);
            cont.cmd.ExecuteNonQuery();
            cont.con.Close();
        }
    }
}

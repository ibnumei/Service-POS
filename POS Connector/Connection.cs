using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.Windows.Forms;

namespace POS_Connector
{

    public class Connection
    {
        public static String  a = "";
        public Connection()
        {
            get_pass_db();
        }
        public void get_pass_db()
        {
           
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:/Program Files/BIENSI POS V 1.1.E/xmlConn.xml");
            //xmlDoc.LoadXml("product.xml");

            string xpath = "Table/Product";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                a = childrenNode.SelectSingleNode("pass_db").InnerText;
                conString = "Server=localhost;Database=biensi_pos_db;Uid=root;Pwd=" + a + ";";
                //MessageBox.Show(a.ToString());
                //HttpContext.Current.Response.Write(childrenNode.SelectSingleNode("//Product_name").Value);
            }
        }
        public static string conString = "";
        public MySqlConnection con = new MySqlConnection(conString);
        public MySqlCommand cmd;
        public MySqlDataAdapter adapter;
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public MySqlDataReader myReader;
    }


    public class Connection2
    {
        public static String a = "";
        public Connection2()
        {
            get_pass_db();           
        }
        public void get_pass_db()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:/Program Files/POS Connector/xmlConn.xml");
            //xmlDoc.LoadXml("product.xml");

            string xpath = "Table/Product";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                a = childrenNode.SelectSingleNode("pass_db").InnerText;
                conString2 = "Server=localhost;Database=biensi_pos_db;Uid=root;Pwd=" + a + ";";
                //MessageBox.Show(a.ToString());
                //HttpContext.Current.Response.Write(childrenNode.SelectSingleNode("//Product_name").Value);
            }
        }

        public static string conString2 = "";
        public MySqlConnection con2 = new MySqlConnection(conString2);
        public MySqlCommand cmd2;
        public MySqlDataAdapter adapter2;
        public DataTable dt2 = new DataTable();
        public DataSet ds2 = new DataSet();
        public MySqlDataReader myReader2;
    }

    public class Connection3
    {
        public static String a = "";
        public Connection3()
        {
            get_pass_db();
        }
        public void get_pass_db()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:/Program Files/POS Connector/xmlConn.xml");
            //xmlDoc.LoadXml("product.xml");

            string xpath = "Table/Product";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                a = childrenNode.SelectSingleNode("pass_db").InnerText;
                conString3 = "Server=localhost;Database=biensi_pos_db;Uid=root;Pwd=" + a + ";";
                //MessageBox.Show(a.ToString());
                //HttpContext.Current.Response.Write(childrenNode.SelectSingleNode("//Product_name").Value);
            }
        }

        public static string conString3 = "";
        public MySqlConnection con3 = new MySqlConnection(conString3);
        public MySqlCommand cmd3;
        public MySqlDataAdapter adapter3;
        public DataTable dt3 = new DataTable();
        public DataSet ds3 = new DataSet();
        public MySqlDataReader myReader3;
    }

    public class Connection4
    {
        public static String a = "";
        public Connection4()
        {

            get_pass_db();
        }
        public void get_pass_db()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:/Program Files/POS Connector/xmlConn.xml");
            //xmlDoc.LoadXml("product.xml");

            string xpath = "Table/Product";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                a = childrenNode.SelectSingleNode("pass_db").InnerText;
                conString4 = "Server=localhost;Database=biensi_pos_db;Uid=root;Pwd=" + a + ";";
                //MessageBox.Show(a.ToString());
                //HttpContext.Current.Response.Write(childrenNode.SelectSingleNode("//Product_name").Value);
            }
        }

        public static string conString4 = "";
        public MySqlConnection con4 = new MySqlConnection(conString4);
        public MySqlCommand cmd4;
        public MySqlDataAdapter adapter4;
        public DataTable dt4 = new DataTable();
        public DataSet ds4 = new DataSet();
        public MySqlDataReader myReader4;
    }
}

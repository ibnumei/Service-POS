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
    public class LinkApi
    {
        public static String b = "";
        public string aLink;
        public LinkApi()
        {
            get_link();
        }
        public void get_link()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:/Program Files/BIENSI POS V 1.1.E/xmlConn.xml");
            //xmlDoc.LoadXml("product.xml");

            string xpath = "Table/Product";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                b = childrenNode.SelectSingleNode("link_api").InnerText;
                //MessageBox.Show(a.ToString());
                //HttpContext.Current.Response.Write(childrenNode.SelectSingleNode("//Product_name").Value);
            }
            aLink = b;
        }
    }
}

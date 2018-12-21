using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization.Json;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace POS_Connector
{
    class ControlReturnOrder
    {
        LinkApi link = new LinkApi();
        Connection ckon1 = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        //========================VARIABLE FOR ARTICLE ======== =========================================
        String id_from_article2, articleName2, brand2, color2, department2, dept_type2, gender2, size2, unit2,art_id_alias;
        int id_article2, price_article2;
        //========================VARIABLE FOR RETURN ORDER LINE=========================================
        int id_RO_Line2, qty2, Ro_id_Fk2, id_article_Fk2;
        String Ro_id2, unit_ro;
        //Double id_article_Fk2;
        //========================VARIABLE FOR RETURN ORDER HEADER=======================================
        String store_code2, date2, remark2, id_r_o2, time2, timestamp2, warehouseid2, sequence_number, no_sj;
        int id_Ro2, status2, totalqty2;
        //===============================================================================================
        String real_article_id, link_api;

        //============================================================================================================
        public async Task Post_Return_Order()
        {
            link_api = link.aLink;
            ServicePOS.LogService("Running Return Order");

            RetrunOrder ro_new2 = new RetrunOrder();
            //ro_new2.returnOrderLines = new List<ReturnOrderLine>();
            //=========================CODE FOR POST API FROM DATABASE WITH LOOPING====================================
            String sql = "SELECT * FROM returnorder WHERE STATUS_API=0 AND STATUS=1";
            ckon1.cmd = new MySqlCommand(sql, ckon1.con);
            ckon1.con.Open();
            ckon1.myReader = ckon1.cmd.ExecuteReader();
            if (ckon1.myReader.HasRows)
            {
                while (ckon1.myReader.Read())
                {
                    //==============GET DATA FROM RET_ORDER HEADER========
                    id_r_o2 = ckon1.myReader.GetString("RETURN_ORDER_ID");
                    sequence_number = id_r_o2.Substring(12);
                    Ro_id_Fk2 = ckon1.myReader.GetInt32("_id");
                    store_code2 = ckon1.myReader.GetString("STORE_CODE");
                    date2 = ckon1.myReader.GetString("DATE");
                    id_Ro2 = ckon1.myReader.GetInt32("_id");
                    remark2 = ckon1.myReader.GetString("REMARK");
                    status2 = ckon1.myReader.GetInt32("STATUS");
                    time2 = ckon1.myReader.GetString("TIME");
                    timestamp2 = ckon1.myReader.GetString("TIME_STAMP");
                    totalqty2 = ckon1.myReader.GetInt32("TOTAL_QTY");
                    warehouseid2 = ckon1.myReader.GetString("WAREHOUSE_ID");
                    no_sj = ckon1.myReader.GetString("NO_SJ");
                    //==============SEARCH BY RETURN ORDER ID=============
                    String sql2 = "SELECT * FROM returnorder_line WHERE RETURN_ORDER_ID='" + id_r_o2 + "'";
                    ro_new2.returnOrderLines = new List<ReturnOrderLine>();
                    ckon2.cmd2 = new MySqlCommand(sql2, ckon2.con2);
                    ckon2.con2.Open();
                    ckon2.myReader2 = ckon2.cmd2.ExecuteReader();
                    while (ckon2.myReader2.Read())
                    {
                        //===============GET ARTICLE ID FROM RET_ORDER LINE===============================
                        real_article_id = ckon2.myReader2.GetString("ARTICLE_ID");
                        //=====================SEARCH ARTICLE BY ARTICLE ID===============================
                        String sql3 = "SELECT * FROM article WHERE ARTICLE_ID='" + real_article_id + "'";
                        ckon3.cmd3 = new MySqlCommand(sql3, ckon3.con3);
                        ckon3.con3.Open();
                        ckon3.myReader3 = ckon3.cmd3.ExecuteReader();
                        while (ckon3.myReader3.Read())
                        {
                            id_article2 = ckon3.myReader3.GetInt32("_id");
                            id_from_article2 = ckon3.myReader3.GetString("ARTICLE_ID");
                            articleName2 = ckon3.myReader3.GetString("ARTICLE_NAME");
                            brand2 = ckon3.myReader3.GetString("BRAND");
                            gender2 = ckon3.myReader3.GetString("GENDER");
                            department2 = ckon3.myReader3.GetString("DEPARTMENT");
                            dept_type2 = ckon3.myReader3.GetString("DEPARTMENT_TYPE");
                            size2 = ckon3.myReader3.GetString("SIZE");
                            color2 = ckon3.myReader3.GetString("COLOR");
                            unit2 = ckon3.myReader3.GetString("UNIT");
                            price_article2 = ckon3.myReader3.GetInt32("PRICE");
                            art_id_alias = ckon3.myReader3.GetString("ARTICLE_ID_ALIAS");
                        }
                        ckon3.con3.Close();
                        //===============================END OF ARTICLE DATA==============================
                        id_article_Fk2 = id_article2;
                        id_RO_Line2 = ckon2.myReader2.GetInt32("_id");
                        qty2 = ckon2.myReader2.GetInt32("QUANTITY");
                        Ro_id2 = ckon2.myReader2.GetString("RETURN_ORDER_ID");
                        unit_ro = ckon2.myReader2.GetString("UNIT");

                        //=========================GET VARIABLE FOR API==================================
                        ReturnOrderLine roLine = new ReturnOrderLine()
                        {
                            article = new Article
                            {
                                articleId = id_from_article2,
                                articleName = articleName2,
                                brand = brand2,
                                color = color2,
                                department = department2,
                                departmentType = dept_type2,
                                gender = gender2,
                                size = size2,
                                unit = unit2,
                                id = id_article2,
                                price = price_article2,
                                articleIdAlias = art_id_alias
                            },
                            articleIdFk = id_article_Fk2,
                            id = id_RO_Line2,
                            quantity = qty2,
                            returnOrderId = Ro_id2,
                            returnOrderIdFk = Ro_id_Fk2,
                            unit = unit_ro
                        };
                        ro_new2.returnOrderLines.Add(roLine);
                        //=========================END GET RET ORDER LINE================================
                    }
                    ckon2.con2.Close();
                    //============================END OF WHILE IN RET_ORDER LINE===========================

                    //==================================API FOR RET ORDER HEADER==============================
                    RetrunOrder ro_new = new RetrunOrder()
                    {
                        storeCode = store_code2,
                        SequenceNumber = sequence_number,
                        date = date2,
                        id = id_Ro2,
                        remark = remark2,
                        returnOrderId = id_r_o2,
                        returnOrderLines = ro_new2.returnOrderLines,
                        status = status2,
                        time = time2,
                        timeStamp = timestamp2,
                        totalQty = totalqty2,
                        warehouseId = warehouseid2,
                        oldSJ = no_sj

                    };
                    //var json2 = new JavaScriptSerializer().Serialize(ro_new);
                    var stringPayload = JsonConvert.SerializeObject(ro_new);
                    var credentials = new NetworkCredential("username", "password");
                    var handler = new HttpClientHandler { Credentials = credentials };
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient(handler))
                    {
                         try
                        {
                            HttpResponseMessage message = client.PostAsync(link_api+ "/api/ReturnOrder", httpContent).Result;
                            //HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/ReturnOrder", httpContent).Result;
                            if(message.IsSuccessStatusCode)
                            {
                                String query = "UPDATE returnorder SET STATUS_API='1' WHERE RETURN_ORDER_ID='" + id_r_o2 + "'";
                                Crud input = new Crud();
                                input.NonReturn2(query);
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                    }
                    //==================================END OF API FOR RET ORDER HEADER=======================

                }
                //================================END OF WHILE IN RET_ORDER HEADER=========================
            }
            ckon1.con.Close();
        }
    }
}

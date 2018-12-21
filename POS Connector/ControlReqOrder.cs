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
    class ControlReqOrder
    {
        LinkApi link = new LinkApi();
        Connection ckon1 = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        //========================VARIABLE FOR ARTICLE ======== =========================================
        String id_from_article2, articleName2, brand2, color2, department2, dept_type2, gender2, size2, unit2,art_id_alias;
        int id_article2, price_article2;
        //========================VARIABLE FOR REQUEST ORDER LINE=========================================
        int id_RO_Line2, qty2, Ro_id_Fk2, id_article_Fk2;
        String Ro_id2, unit_ro;
        //========================VARIABLE FOR REQUEST ORDER HEADER=======================================
        String store_code2, date2, req_dev_date2, req_order_id2, time2, timestamp2, warehouseid2, cust_id_Store, epy_id, epy_name, sequence_number, no_sj;
        int id_order2, status2, total_qty2;
        //===============================================================================================
        String real_article_id, link_api;

        public async Task Post_request_order()
        {
            link_api = link.aLink;
            ServicePOS.LogService("Running Request Order");

            RequestOrder req_o = new RequestOrder();
            //req_o.requestOrderLines = new List<RequestOrderLine>();
            //=========================CODE FOR POST API FROM DATABASE WITH LOOPING====================================
            String sql = "SELECT * FROM requestorder WHERE STATUS_API='0' AND STATUS='1'";
            ckon1.cmd = new MySqlCommand(sql, ckon1.con);
            ckon1.con.Open();
            ckon1.myReader = ckon1.cmd.ExecuteReader();
            if (ckon1.myReader.HasRows)
            {
                while (ckon1.myReader.Read())
                {
                    //==============GET DATA FROM RET_ORDER HEADER========
                    req_order_id2 = ckon1.myReader.GetString("REQUEST_ORDER_ID");
                    sequence_number = req_order_id2.Substring(12);
                    Ro_id_Fk2 = ckon1.myReader.GetInt32("_id");
                    store_code2 = ckon1.myReader.GetString("STORE_CODE");
                    date2 = ckon1.myReader.GetString("DATE");
                    id_order2 = ckon1.myReader.GetInt32("_id");
                    req_dev_date2 = ckon1.myReader.GetString("REQUEST_DELIVERY_DATE");
                    status2 = ckon1.myReader.GetInt32("STATUS");
                    time2 = ckon1.myReader.GetString("TIME");
                    timestamp2 = ckon1.myReader.GetString("TIME_STAMP");
                    total_qty2 = ckon1.myReader.GetInt32("TOTAL_QTY");
                    warehouseid2 = ckon1.myReader.GetString("WAREHOUSE_ID");
                    cust_id_Store = ckon1.myReader.GetString("CUST_ID_STORE");
                    epy_id = ckon1.myReader.GetString("EMPLOYEE_ID");
                    epy_name = ckon1.myReader.GetString("EMPLOYEE_NAME");
                    no_sj = ckon1.myReader.GetString("NO_SJ");
                    //==============SEARCH BY RETURN ORDER ID=============
                    String sql2 = "SELECT * FROM requestorder_line WHERE REQUEST_ORDER_ID='" + req_order_id2 + "'";
                    req_o.requestOrderLines = new List<RequestOrderLine>();
                    ckon2.cmd2 = new MySqlCommand(sql2, ckon2.con2);
                    ckon2.con2.Open();
                    ckon2.myReader2 = ckon2.cmd2.ExecuteReader();
                    while (ckon2.myReader2.Read())
                    {
                        //===============GET ARTICLE ID FROM REQ_ORDER LINE===============================
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
                        Ro_id2 = ckon2.myReader2.GetString("REQUEST_ORDER_ID");
                        unit_ro = ckon2.myReader2.GetString("UNIT");

                        //===============================GET VARIABLE FOR API=============================
                        RequestOrderLine rq_line = new RequestOrderLine()
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
                            requestOrderId = Ro_id2,
                            requestOrderIdFk = Ro_id_Fk2,
                            unit = unit_ro
                        };
                        req_o.requestOrderLines.Add(rq_line);
                        //=========================END GET API FOR REQ LINE===============================
                    }
                    ckon2.con2.Close();
                    //============================END OF WHILE IN REQ_ORDER LINE===========================

                    //================================API FOR REQ ORDER HEADER=================================
                    RequestOrder req_order = new RequestOrder()
                    {
                        storeCode = store_code2,
                        SequenceNumber = sequence_number,
                        date = date2,
                        id = id_order2,
                        requestDeliveryDate = req_dev_date2,
                        requestOrderId = req_order_id2,
                        requestOrderLines = req_o.requestOrderLines,
                        status = status2,
                        time = time2,
                        timeStamp = timestamp2,
                        totalQty = total_qty2,
                        warehouseId = warehouseid2,
                        customerIdStore = cust_id_Store,
                        employeeId = epy_id,
                        employeeName = epy_name,
                        oldSJ = no_sj
                    };
                    //var json2 = new JavaScriptSerializer().Serialize(req_order);
                    var stringPayload = JsonConvert.SerializeObject(req_order);
                    var credentials = new NetworkCredential("username", "password");
                    var handler = new HttpClientHandler { Credentials = credentials };
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient(handler))
                    {
                        try
                        {
                            HttpResponseMessage message = client.PostAsync(link_api+"/api/RequestOrder", httpContent).Result;
                            //HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/RequestOrder", httpContent).Result;
                            if(message.IsSuccessStatusCode)
                            {
                                String query = "UPDATE requestorder SET STATUS_API='1' WHERE REQUEST_ORDER_ID='" + req_order_id2 + "'";
                                Crud input = new Crud();
                                input.NonReturn2(query);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                    }
                    //================================END API FOR REQ ORDER HEADER=============================

                }
                //================================END OF WHILE IN RET_ORDER HEADER=========================
            }
            ckon1.con.Close();
        }
    }
}

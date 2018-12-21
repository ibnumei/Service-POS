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
    class ControlDoPutcs
    {
        LinkApi link = new LinkApi();
        Connection ckon1 = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        //========================VARIABLE FOR ARTICLE ======== =========================================
        String id_from_article2, articleName2, brand2, color2, department2, dept_type2, gender2, size2, unit2,art_id_alias;
        int id_article2, price_article2;
        //========================VARIABLE FOR DELIVERY ORDER LINE ======== =============================
        int id_article_Fk2, dev_orderid2_Fk, id_DO_Line2, qty_dev2, qty_rev2,amount2;
        String dev_orderid2;
        //========================VARIABLE FOR DELIVERY ORDER HEADER======= =============================
        String date2, dev_date2, dev_orderId2, dev_time2, time2, timestamp2, store_code2, war_from2, war_to2, cust_id_store, epy_id, epy_name, packing_number;
        int id_Do2, status2, totalqty2, tot_amount2;
        //===============================================================================================
        String link_api;
        public async Task Put_Dev_Order()
        {
            link_api = link.aLink;
            ServicePOS.LogService("Running DO PUT");

            DeliveryOrder do2 = new DeliveryOrder();
            //do2.deliveryOrderLines = new List<DeliveryOrderLine>();
            String sql = "SELECT * FROM deliveryorder WHERE STATUS_API=0 AND STATUS='1'";
            ckon1.cmd = new MySqlCommand(sql, ckon1.con);
            ckon1.con.Open();
            ckon1.myReader = ckon1.cmd.ExecuteReader();
            if (ckon1.myReader.HasRows)
            {
                while (ckon1.myReader.Read())
                {
                    //=================AMBIL NILAI DO HEADER====================
                    dev_orderId2 = ckon1.myReader.GetString("DELIVERY_ORDER_ID");
                    dev_orderid2_Fk = ckon1.myReader.GetInt32("_id");
                    date2 = ckon1.myReader.GetString("DATE");
                    dev_date2 = ckon1.myReader.GetString("DELIVERY_DATE");
                    dev_time2 = ckon1.myReader.GetString("TIME");
                    id_Do2 = ckon1.myReader.GetInt32("_id");
                    status2 = ckon1.myReader.GetInt32("STATUS");
                    time2 = ckon1.myReader.GetString("TIME");
                    timestamp2 = ckon1.myReader.GetString("TIME_STAMP");
                    totalqty2 = ckon1.myReader.GetInt32("TOTAL_QTY");
                    store_code2 = ckon1.myReader.GetString("STORE_CODE");
                    war_from2 = ckon1.myReader.GetString("WAREHOUSE_FROM");
                    war_to2 = ckon1.myReader.GetString("WAREHOUSE_TO");
                    cust_id_store = ckon1.myReader.GetString("CUST_ID_STORE");
                    epy_id = ckon1.myReader.GetString("EMPLOYEE_ID");
                    epy_name = ckon1.myReader.GetString("EMPLOYEE_NAME");
                    tot_amount2 = ckon1.myReader.GetInt32("TOTAL_AMOUNT");
                    //===========SEARCH DO LINE BY DO ID========================
                    String sql2 = "SELECT * FROM deliveryorder_line WHERE DELIVERY_ORDER_ID = '" + dev_orderId2 + "'";
                    do2.deliveryOrderLines = new List<DeliveryOrderLine>();
                    ckon2.cmd2 = new MySqlCommand(sql2, ckon2.con2);
                    ckon2.con2.Open();
                    ckon2.myReader2 = ckon2.cmd2.ExecuteReader();
                    while (ckon2.myReader2.Read())
                    {
                        //====================GET VALUE FROM DO LINE======================================
                        id_article_Fk2 = ckon2.myReader2.GetInt32("ARTICLE_ID");
                        //=============SEARCH DATA ARTICLE BY ARTICLE ID================================
                        String sql3 = "SELECT * FROM article WHERE ARTICLE_ID='" + id_article_Fk2 + "'";
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
                        //===============================END OF ARTICLE DATA============================
                        dev_orderid2 = ckon2.myReader2.GetString("DELIVERY_ORDER_ID");
                        id_DO_Line2 = ckon2.myReader2.GetInt32("_id");
                        qty_dev2 = ckon2.myReader2.GetInt32("QTY_DELIVER");
                        qty_rev2 = ckon2.myReader2.GetInt32("QTY_RECEIVE");
                        amount2 = ckon2.myReader2.GetInt32("AMOUNT");
                        packing_number = ckon2.myReader2.GetString("PACKING_NUMBER");
                        DeliveryOrderLine do_line = new DeliveryOrderLine()
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
                            deliveryOrderId = dev_orderid2,
                            deliveryOrderIdFk = dev_orderid2_Fk,
                            id = id_DO_Line2,
                            qtyDeliver = qty_dev2,
                            qtyReceive = qty_rev2,
                            amount = amount2,
                            packingNumber = packing_number
                        };

                        do2.deliveryOrderLines.Add(do_line);
                    }
                    ckon2.con2.Close();
                    //======================END WHILE CKON2 GET DO LINE DATA===============================

                    DeliveryOrder dev_order = new DeliveryOrder()
                    {
                        date = date2,
                        deliveryDate = dev_date2,
                        deliveryOrderId = dev_orderId2,
                        deliveryOrderLines = do2.deliveryOrderLines,
                        deliveryTime = dev_time2,
                        id = id_Do2,
                        status = status2,
                        time = time2,
                        timeStamp = timestamp2,
                        totalQty = totalqty2,
                        storeCode = store_code2,
                        warehouseFrom = war_from2,
                        warehouseTo = war_to2,
                        CustomerIdStore = cust_id_store,
                        employeeId = epy_id,
                        employeeName = epy_name,
                        totalAmount = tot_amount2
                    };
                    //var json2 = new JavaScriptSerializer().Serialize(dev_order);
                    var stringPayload = JsonConvert.SerializeObject(dev_order);
                    //String response = "";
                    var credentials = new NetworkCredential("username", "password");
                    var handler = new HttpClientHandler { Credentials = credentials };
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient(handler))
                    {
                        try
                        {
                            HttpResponseMessage message = client.PutAsync(link_api+"/api/DeliveryOrder", httpContent).Result;
                            //HttpResponseMessage message = client.PutAsync("http://retailbiensi.azurewebsites.net/api/DeliveryOrder", httpContent).Result;
                            if(message.IsSuccessStatusCode)
                            {
                                String query = "UPDATE deliveryorder SET STATUS_API='1' WHERE DELIVERY_ORDER_ID='" + dev_orderId2 + "'";
                                Crud input = new Crud();
                                input.NonReturn2(query);
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }


                    }

                }
                //=========================END WHILE CKON1 GET DO HEADER DATA==============================
            }
            ckon1.con.Close();
            //=============================END IF HAS ROWS DATA=============================================
        }
    }
}

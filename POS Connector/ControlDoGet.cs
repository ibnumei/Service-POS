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
using Newtonsoft.Json;

namespace POS_Connector
{
    class ControlDoGet
    {
        LinkApi link = new LinkApi();
        Connection ckon = new Connection();
        String store_code,link_api;
        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                store_code = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();
        }
        //==============================================================
        //==============================================================================================================================
        public async Task get_do()
        {
            link_api = link.aLink;
            ServicePOS.LogService("Running DO GET");

            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials }; // for validation
                                                                               //    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };// allow domain checker
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage message = client.GetAsync(link_api+"/api/DeliveryOrder?StoreCode=" + store_code).Result;
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/DeliveryOrder?StoreCode=" + store_code).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<DeliveryOrder>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<DeliveryOrder> resultData = serializer.ReadObject(stream) as List<DeliveryOrder>;
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                String sql1 = "INSERT INTO deliveryorder (_id, DELIVERY_ORDER_ID, STORE_CODE, WAREHOUSE_FROM, WAREHOUSE_TO, DELIVERY_DATE, DELIVERY_TIME, TOTAL_QTY, STATUS, DATE, TIME,CUST_ID_STORE,TOTAL_AMOUNT) VALUES ('" + resultData[i].id + "', '" + resultData[i].deliveryOrderId + "', '" + resultData[i].storeCode + "', '" + resultData[i].warehouseFrom + "', '" + resultData[i].warehouseTo + "', '" + resultData[i].deliveryDate + "', '" + resultData[i].deliveryTime + "', '" + resultData[i].totalQty + "', '" + resultData[i].status + "', '" + resultData[i].date + "', '" + resultData[i].time + "','" + resultData[i].CustomerIdStore + "','"+ resultData[i].totalAmount +"')";
                                Crud input2 = new Crud();
                                input2.NonReturn2(sql1);

                                foreach (var c in resultData[i].deliveryOrderLines)
                                {
                                    String sql = "INSERT INTO deliveryorder_line (_id, DELIVERY_ORDER_ID, ARTICLE_ID, QTY_DELIVER, QTY_RECEIVE, AMOUNT, PACKING_NUMBER) VALUES ('" + c.id + "','" + c.deliveryOrderId + "', '" + c.articleIdFk + "', '" + c.qtyDeliver + "', '" + c.qtyDeliver + "','" + c.amount + "','" + c.packingNumber + "')";
                                    Crud input = new Crud();
                                    input.NonReturn2(sql);

                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());
                            }
                            //===========FOR LOOPING DO_LINE AND INSERT DATABASE=======================================

                        }
                        //======================END FOR GET DO DATA======================================
                    }
                    else
                    {
                        response = "Fail";
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}

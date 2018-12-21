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
    class ControlInventory
    {
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        String store_code;

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
        //===============================================================================================================
        public async Task get_inv()
        {
            ServicePOS.LogService("Running Inventory");

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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Inventory?warehouseId=" + store_code).Result;
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Inventory?warehouseId="+store_code).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Inventory>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Inventory> resultData = serializer.ReadObject(stream) as List<Inventory>;


                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                //int n = dataGridView1.Rows.Add();
                                //dataGridView1.Rows[n].Cells[0].Value = resultData[i].id;
                                //dataGridView1.Rows[n].Cells[1].Value = resultData[i].articleId;
                                //dataGridView1.Rows[n].Cells[2].Value = resultData[i].goodQty;
                                //dataGridView1.Rows[n].Cells[3].Value = resultData[i].rejectQty;
                                //dataGridView1.Rows[n].Cells[4].Value = resultData[i].whGoodQty;
                                //dataGridView1.Rows[n].Cells[5].Value = resultData[i].whRejectQty;
                                //dataGridView1.Rows[n].Cells[6].Value = resultData[i].status;

                                String sql = "INSERT INTO inventory (_id ,ARTICLE_ID, GOOD_QTY, REJECT_QTY, WH_GOOD_QTY, WH_REJECT_QTY, STATUS) VALUES('" + resultData[i].id + "' ,'" + resultData[i].articleId + "', '" + resultData[i].goodQty + "', '" + resultData[i].rejectQty + "', '" + resultData[i].whGoodQty + "', '" + resultData[i].whRejectQty + "', '" + resultData[i].status + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                            }
                            catch
                            {
                                //String sql = "INSERT INTO inventory (_id ,ARTICLE_ID, GOOD_QTY, REJECT_QTY, WH_GOOD_QTY, WH_REJECT_QTY, STATUS) VALUES('" + resultData[i].id + "' ,'" + resultData[i].articleId + "', '" + resultData[i].goodQty + "', '" + resultData[i].rejectQty + "', '" + resultData[i].whGoodQty + "', '" + resultData[i].whRejectQty + "', '" + resultData[i].status + "')";
                                String sql2 = "UPDATE inventory SET GOOD_QTY='" + resultData[i].goodQty + "', REJECT_QTY='" + resultData[i].rejectQty + "', WH_GOOD_QTY='" + resultData[i].whGoodQty + "', WH_REJECT_QTY='" + resultData[i].whRejectQty + "', STATUS='" + resultData[i].status + "' WHERE _id='" + resultData[i].id + "'";
                                Crud input = new Crud();
                                input.NonReturn2(sql2);
                            }

                        }
                    }
                    else
                    {
                        response = "Fail";
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                }
            }
        }
    }
}

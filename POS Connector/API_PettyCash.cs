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
    class API_PettyCash
    {
        LinkApi link = new LinkApi();
        Connection ckon1 = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        //=========================PETTY CASH LINE===================
        String exp_name_line, petty_cash_line;
        int id_line, price_line, qty_line, total_line, pettyCashIdFk2;
        //=========================PETTY HEADER=======================
        String store_code, date, desc, exp_ctg_id, exp_ctg, exp_date, petty_Cash_id, time, time_stamp,cust_id_Store, sequence_number;
        int id, status, total_exp;
        //========================================================================================================================
        String link_api;
        public async Task post_pettyCAsh()
        {
            link_api = link.aLink;
            ServicePOS.LogService("Running Petty Cash");

            PettyCash petty_line2 = new PettyCash();
            //petty_line2.pettyCashLine = new List<PettyCashLine>();
            String sql = "SELECT * FROM pettycash WHERE STATUS='1' AND STATUS_API='0'";
            ckon1.cmd = new MySqlCommand(sql, ckon1.con);
            ckon1.con.Open();
            ckon1.myReader = ckon1.cmd.ExecuteReader();
            if (ckon1.myReader.HasRows)
            {
                while (ckon1.myReader.Read())
                {
                    id = ckon1.myReader.GetInt32("_id");
                    petty_Cash_id = ckon1.myReader.GetString("PETTY_CASH_ID");
                    sequence_number = petty_Cash_id.Substring(12);
                    store_code = ckon1.myReader.GetString("STORE_CODE");
                    exp_ctg_id = ckon1.myReader.GetString("EXPENSE_CATEGORY_ID");
                    exp_ctg = ckon1.myReader.GetString("EXPENSE_CATEGORY");
                    exp_date = ckon1.myReader.GetString("EXPENSE_DATE");
                    desc = ckon1.myReader.GetString("DESCRIPTION");
                    total_exp = ckon1.myReader.GetInt32("TOTAL_EXPENSE");
                    status = ckon1.myReader.GetInt32("STATUS");
                    date = ckon1.myReader.GetString("DATE");
                    time = ckon1.myReader.GetString("TIME");
                    time_stamp = ckon1.myReader.GetString("TIME_STAMP");
                    pettyCashIdFk2 = ckon1.myReader.GetInt32("_id");
                    cust_id_Store = ckon1.myReader.GetString("CUST_ID_STORE");
                    //=====================SEARCH BY TRANSACTION_ID======================================
                    String sql2 = "SELECT * FROM pettycash_line WHERE PETTY_CASH_ID='" + petty_Cash_id + "'";
                    petty_line2.pettyCashLine = new List<PettyCashLine>();
                    ckon2.cmd2 = new MySqlCommand(sql2, ckon2.con2);
                    ckon2.con2.Open();
                    ckon2.myReader2 = ckon2.cmd2.ExecuteReader();
                    while (ckon2.myReader2.Read())
                    {
                        exp_name_line = ckon2.myReader2.GetString("EXPENSE_NAME");
                        id_line = ckon2.myReader2.GetInt32("_id");
                        petty_cash_line = ckon2.myReader2.GetString("PETTY_CASH_ID");
                        price_line = ckon2.myReader2.GetInt32("PRICE");
                        qty_line = ckon2.myReader2.GetInt32("QUANTITY");
                        total_line = ckon2.myReader2.GetInt32("TOTAL");


                        PettyCashLine petty_line = new PettyCashLine()
                        {

                            expenseName = exp_name_line,
                            id = id_line,
                            pettyCashId = petty_cash_line,
                            pettyCashIdFk = pettyCashIdFk2,
                            price = price_line,
                            quantity = qty_line,
                            total = total_line,
                        };
                        petty_line2.pettyCashLine.Add(petty_line);
                    }
                    ckon2.con2.Close();

                    PettyCash petty_header = new PettyCash()
                    {
                        storeCode = store_code,
                        SequenceNumber = sequence_number,
                        customerIdStore = cust_id_Store,
                        date = date,
                        description = desc,
                        expenseCategoryId = exp_ctg_id,
                        expenseCategory = exp_ctg,
                        expenseDate = exp_date,
                        id = id,
                        pettyCashId = petty_Cash_id,
                        status = status,
                        time = time,
                        timeStamp = time_stamp,
                        totalExpense = total_exp,
                        pettyCashLine = petty_line2.pettyCashLine

                    };
                    var stringPayload = JsonConvert.SerializeObject(petty_header);
                    String response = "";
                    var credentials = new NetworkCredential("username", "password");
                    var handler = new HttpClientHandler { Credentials = credentials };
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient(handler))
                    {
                     try
                        {
                            HttpResponseMessage message = client.PostAsync(link_api+"/api/PettyCash", httpContent).Result;
                            //HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/PettyCash", httpContent).Result;
                            if(message.IsSuccessStatusCode)
                            {
                                String query = "UPDATE pettycash SET STATUS_API='1' WHERE PETTY_CASH_ID='" + petty_Cash_id + "'";
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

            }
            ckon1.con.Close();
        }
        //=================================================================================================
    }
}

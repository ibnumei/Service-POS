using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace POS_Connector
{
    public partial class ServicePOS : ServiceBase
    {

        System.Timers.Timer timeDelay;
        int count;
        public ServicePOS()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer(120000);
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }
        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime date = new DateTime();
            string process = "Timer Tick " + count;
            LogService(process);
            API_PettyCash PETTY = new API_PettyCash();
            Controller c = new Controller();
            ControlMutOrder mut_order = new ControlMutOrder();
            ControlReqOrder req_order = new ControlReqOrder();
            ControlReturnOrder ret_order = new ControlReturnOrder();
            ControlDoPutcs do_put = new ControlDoPutcs();
            ControlDoGet do_get = new ControlDoGet();
            //ControlInventory inv = new ControlInventory();
            PETTY.post_pettyCAsh();
            //==============================
            do_get.get_cust_id();
            do_get.get_do();
            //========================
            do_put.Put_Dev_Order();
            //========================
            ret_order.Post_Return_Order();
            //========================
            req_order.Post_request_order();
            //========================
            mut_order.Post_Mutasi_Order();
            //========================
            c.PostTransaction();
            //========================
            
            count++;
        }
        protected override void OnStart(string[] args)
        {
            LogService("Service is Started");
            timeDelay.Enabled = true;
        }
        protected override void OnStop()
        {
            LogService("Service Stoped");
            timeDelay.Enabled = false;
        }
        public static void LogService(string content)
        {
            FileStream fs = new FileStream(@"E:\ServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private HttpListener listener;
        private Thread listenerThread;

        static string strComputerName = Environment.MachineName.ToString();
        static string computed_server_name = strComputerName + @"\SQLSERVER2012";
        public static string server_database_conn_string = "Data Source=" + computed_server_name + ";Initial Catalog=Tes2;Integrated Security=True;TrustServerCertificate=True";


        protected override void OnStart(string[] args)
        {
            listener = new HttpListener();
            string ip_address = "127.0.0.1";
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);  // Google's public DNS server
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    ip_address = endPoint?.Address.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting local IP address: {ex.Message}");

            }
            listener.Prefixes.Add("http://" + ip_address + ":9000/");
            listener.Prefixes.Add("http://127.0.0.1:9000/");
            // Change the port as needed
            listener.Start();

            listenerThread = new Thread(ListenForRequests);
            listenerThread.Start();
        }


        private void ListenForRequests()
        {
            try
            {
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    if (request.RawUrl == "/Connection/")
                    {
                        //Return the connection string 
                        string responseString = server_database_conn_string;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                        HttpListenerResponse response = context.Response;
                        response.ContentType = "text/plain";
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        response.Close();
                    }
                    else
                    {
                        // Return the type of state of the application 
                        string responseString = "Server State";
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                        HttpListenerResponse response = context.Response;
                        response.ContentType = "text/plain";
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        response.Close();
                    }



                }
            }
            catch (ThreadAbortException)
            {
                // Thread is being aborted, handle accordingly
            }
        }

        protected override void OnStop()
        {
            listenerThread.Abort();
            listener.Close();
        }
    }
}

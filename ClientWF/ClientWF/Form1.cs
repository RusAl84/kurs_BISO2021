using ClassLib;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWF
{
  public partial class Form1 : Form
  {
    public int pos = 0;
    public string baseUrl = "http://localhost:5000";
    public string token;
    public Form1()
    {
      InitializeComponent();
    }


    private bool check_allow_server(string ip, int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                socket.Connect(point);
                return true;
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 10061)
                    return false;
                else 
                if (e.ErrorCode == 10060)
                    return false;
                //    Console.WriteLine("TimeOut"); 
                else
                    return false;
                //Console.WriteLine(e.Message);
            }
            return false;

        }

    private void button2_Click(object sender, EventArgs e)
    {

    }
        private string  drawEmoji(string str1)
        {
            int i = 0;
            string str2 = "";
            while(i<str1.Length)
            {
                if (str1[i] == '\\') 
                {
                    string tmp = "\\";
                    int j = i + 1;
                    while ((j < str1.Length) && (str1[j] != ' '))
                    {
                        tmp += str1[j];
                        j++;
                    }
                    tmp = tmp.Replace("\\u", "");
                    var personWithBlondHair = ""
                        + (char)int.Parse(tmp.Substring(0, 4), NumberStyles.HexNumber)
                        + (char)int.Parse(tmp.Substring(4, 4), NumberStyles.HexNumber);

                    str2 += personWithBlondHair;
                    i = j;
                }
                else
                {
                    str2 += str1[i];
                }
                i++;    
            }
            return str2;
        }
    private void timer1_Tick(object sender, EventArgs e)
    {
      string res = "";
      if (check_allow_server("127.0.0.1",5000))
          while (res != "Not found")
          {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("api/GetMessage/" + pos, Method.GET);
            var queryResult = client.Execute(request);
            res = queryResult.Content;
            res = res.Trim('\"');
            if (res != "Not found")
            {
              res= drawEmoji(res);
              listBox1.Items.Add(res);
              pos++;
            }
          }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var client = new RestClient(baseUrl);
      var request = new RestRequest("api/sendmessage", Method.POST);
      request.RequestFormat = DataFormat.Json;
      ClassLib.MessageClass mes = new ClassLib.MessageClass();
      mes.userName = textBox1.Text;
      mes.messageText = textBox2.Text;
      mes.timeStamp = DateTime.Now.ToString();
      mes.token = token;
      request.AddBody(mes);
      client.Execute(request);
    }
        private void button2_Click_1(object sender, EventArgs e)
        {
            string url = baseUrl + "/api/login";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                LoginClass lg = new LoginClass();
                lg.login = loginTB.Text.ToLower();
                lg.password = CryptClass.GetSHA256(passwordTB.Text);
                string jsonString = JsonConvert.SerializeObject(lg, Formatting.Indented);
                streamWriter.Write(jsonString);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string strdata = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                strdata = streamReader.ReadToEnd();
            }

            token = strdata;

            //getLoginByToken
            string url2 = baseUrl + "/api/getLoginByToken";
            var httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url2);
            httpWebRequest2.ContentType = "application/json";
            httpWebRequest2.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest2.GetRequestStream()))
            {
                streamWriter.Write("\""+token+ "\"");
            }

            var httpResponse2 = (HttpWebResponse)httpWebRequest2.GetResponse();
            string strdata2 = "";
            using (var streamReader = new StreamReader(httpResponse2.GetResponseStream()))
            {
                strdata2 = streamReader.ReadToEnd();
            }
            textBox1.Text = strdata2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = baseUrl + "/api/reg";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                LoginClass lg = new LoginClass();
                lg.login = loginTB.Text.ToLower();
                lg.password = CryptClass.GetSHA256(passwordTB.Text);
                string jsonString = JsonConvert.SerializeObject(lg, Formatting.Indented);
                streamWriter.Write(jsonString);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string strdata = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                strdata = streamReader.ReadToEnd();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index > 0)
            {
                textBox2.Text= "цитата: ^"+listBox1.Items[index].ToString()+"^";
            }
            
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked)
            //{
            //    MessageBox.Show("Bla");
            //}


        }

        private void checkBox1_CheckStateChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.BackColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text+= "👍";
            //Image image = Image.FromFile(@"d:\ico\1.png");

            ////Устанавливаешь ей свойство Source: image.Source=...
            //listBox1.Items.Add("😃😍✌✌😂😂");
            //string unicodeString = "\u1F642";
            //listBox1.Items.Add(unicodeString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

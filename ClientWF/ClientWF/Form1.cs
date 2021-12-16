using ClassLib;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      string res = "";
      while (res != "Not found")
      {
        var client = new RestClient(baseUrl);
        var request = new RestRequest("api/GetMessage/" + pos, Method.GET);
        var queryResult = client.Execute(request);
        res = queryResult.Content;
        res = res.Trim('\"');
        if (res != "Not found")
        {
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

namespace ClassLib
{
    [Serializable]
    public class LoginClass
    {
        public string login { get; set; }
        public string password { get; set; }
        private string token;
        private DateTime timeStamp;
        private DateTime elapsedRoundTime;
        private bool round;
        public void UpdateElapsedRoundTime(DateTime _eRT, bool _round)
        {
            elapsedRoundTime = _eRT;
            round = _round;
        }

        public string getElapsedTime()
        {
            elapsedRoundTime.ToString("mm:ss");
            return elapsedRoundTime.ToString("mm:ss"); ;
        }
        public LoginClass(string _login, string _password)
        {
            this.login = _login;
            this.password = _password;
        }
        public LoginClass()
        {
        }
        public string GenToken()
        {
            string salt = "СОЛЬ ЧТОБЫ НИКТО НЕ ДОГАДАЛСЯ;)";
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }
        public string GetToken()
        {
            return this.token;
        }
        public DateTime setTimeStamp()
        {
            timeStamp = DateTime.Now;
            return timeStamp;
        }
        public override string ToString()
        {
            return String.Format($"login:{login} password:{password} token:{token} timeStamp: {timeStamp}");
        }

        
    }
}

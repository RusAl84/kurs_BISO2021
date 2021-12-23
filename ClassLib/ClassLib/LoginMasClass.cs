using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassLib
{
    [Serializable]
    public class LoginMasClass
    {
        public string LoginsFileName;
        public List<LoginClass> ListOfLogins = new List<LoginClass>();
        public LoginMasClass(string _loginsFileName)
        {
            LoginsFileName = _loginsFileName;
        }
        /// <summary>
        /// Добавление новых логинов и паролей в файл
        /// </summary>
        /// <param name="lg"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool AddUser(LoginClass lg, out string token)
        {
            bool existUser = false;
            foreach (LoginClass item in ListOfLogins)
            {
                if (item.login == lg.login)
                    existUser = true;
            }
            if (!existUser)
            {
                lg.setTimeStamp();
                token = lg.GenToken();
                ListOfLogins.Add(lg);
                return true;
            }
            else
            {
                Console.WriteLine($"Error login: {lg.login} already exists");
                token = "";
                return false;
            }

        }
        public string CheckLoginPassword(LoginClass lg)
        {
            //ListOfLogins.Find((LoginClass item) => item.login == lg.login)
            for (int i = 0; i < ListOfLogins.Count(); i++)
            {
                if (lg.login == ListOfLogins[i].login)
                {
                    if (lg.password == ListOfLogins[i].password)
                    {
                        ListOfLogins[i].setTimeStamp();
                        return ListOfLogins[i].GenToken();
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="_login"></param>
        /// <param name="_password"></param>
        /// <returns></returns>
        public string RegUser(string _login, string _password)
        {

            LoginClass lg = new LoginClass(_login.ToLower(),
                                            _password);  //! уже в SHA256
            if (AddUser(lg, out string token))
            {
                SaveLogins();
                return token;
            }
            else
                return "";

        }
        public string getLoginByToken(string _token)
        {
            foreach (LoginClass item in ListOfLogins)
            {
                if (item.GetToken() == _token)
                    return item.login;
            }
            return "";
        }
        /// <summary>
        /// Функция загружает логины и пароли из файла
        /// </summary>
        public void LoadLogins()
        {
            if (File.Exists(LoginsFileName))
            {
                string restoredJsonString = File.ReadAllText(LoginsFileName);
                this.ListOfLogins = JsonConvert.DeserializeObject<List<LoginClass>>(restoredJsonString);
            }
        }
        /// <summary>
        /// Функция сохраняет логины и пароли в файл
        /// </summary>
        public void SaveLogins()
        {
            string jsonString = JsonConvert.SerializeObject(ListOfLogins, Formatting.Indented);
            File.WriteAllText(LoginsFileName, jsonString);
        }
        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="oldToken"></param>
        /// <returns></returns>
        public string refreshToken(string oldToken)
        {
            LoginClass lg = new LoginClass();
            int ind = ListOfLogins.FindIndex((LoginClass item) => item.GetToken() == oldToken);
            if (ind >= 0)
                return ListOfLogins[ind].GenToken();
            else
                return "";
        }
        public List<String> GetContacts()
        {
            List<String> contacts = new List<String>();
            foreach (LoginClass item in ListOfLogins)
            {
                contacts.Add(item.login);
            }
            return contacts;
        }
        /// <summary>
        /// Вывод логинов и паролей на экран
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str1 = "";
            foreach (LoginClass item in ListOfLogins)
                str1 += "\n" + item;
            return str1;
        }
       
        
        
    }
}

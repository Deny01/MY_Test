using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace TGZJ.Base
{


    public class Simple3Des
    {
        private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

        private Byte[] TruncateHash(string key, Int32 length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();


            Byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            Byte[] hash = sha1.ComputeHash(keyBytes);


            int k = length - 4;
            Byte[] hash2 = new Byte[24];
            for (Int32 ii = 0; ii < k; ii++)
            {
                hash2.SetValue(hash[ii], ii);


            }
            return hash2;
        }

        private Byte[] TruncateHash2(string key, Int32 length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();


            Byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            Byte[] hash = sha1.ComputeHash(keyBytes);


            int k = 7;
            Byte[] hash2 = new Byte[8];
            for (Int32 ii = 0; ii < k; ii++)
            {
                hash2.SetValue(hash[ii], ii);


            }
            return hash2;
        }

        public Simple3Des(string key)
        {
            TripleDes.Key = TruncateHash(key, (TripleDes.KeySize - TripleDes.KeySize % 8) / 8);
            TripleDes.IV = TruncateHash2("", (TripleDes.KeySize - TripleDes.KeySize % 8) / 8);

        }

        public string EncryptData(string plaintext)
        {


            Byte[] plaintextBytes = Encoding.Unicode.GetBytes(plaintext);

            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptData(string encryptedtext)
        {
            string here_encryptedtext = encryptedtext;

            if (null == here_encryptedtext || here_encryptedtext.Length == 0)
            {
                here_encryptedtext = "46Vq98UDzH06N+3BDxW3oxHEtqBEU4/BJ0u3p+H7EJKD93MRSiRiYGxgHwPfkIHZszJlHDFTZu8tgSnFr64yDAfd4411w+QU";
            
            }

            Byte[] encryptedBytes = Convert.FromBase64String(here_encryptedtext);


            MemoryStream ms = new MemoryStream();

            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), CryptoStreamMode.Write);


            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();


            return Encoding.Unicode.GetString(ms.ToArray());
        }





    }


    [XmlRoot("Dataconn"), Serializable]
    public class Mydataconn
    {
        private static XmlSerializer xmlDataDef = new XmlSerializer(typeof(Mydataconn));
        public Mydataconn()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //private static volatile Mydataconn instance = null;

        //private static object lockHelper = new object();

        //private Mydataconn()
        //{

        //}

        //public static Mydataconn Getinstance()
        //{
        //    if (instance == null)
        //    {
        //        lock (lockHelper)
        //        {
        //            if (instance == null)
        //            {

        //                instance = new Mydataconn();
        //            }

        //        }
        //    }
        //    return instance;
        //}
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public static Mydataconn Load()
        {
            XmlTextReader xmlReader = null;
            Mydataconn pd = null;
            //Directory.GetCurrentDirectory
            try
            {

                if (File.Exists(Directory.GetCurrentDirectory() + "\\SYS_Par"))
                {
                    xmlReader = new XmlTextReader(Directory.GetCurrentDirectory() + "\\SYS_Par");
                    pd = (Mydataconn)xmlDataDef.Deserialize(xmlReader);
                    xmlReader.Close();
                }
                else
                    return null;

                //UpdatePortalDefinitionProperties(pd.tabs, null);
            }
            catch (Exception e)
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                throw new Exception(e.Message, e);
            }


            return pd;
        }

        public void Save()
        {
            XmlTextWriter xmlWriter = null;
            try
            {
                xmlWriter = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\SYS_Par", System.Text.Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlDataDef.Serialize(xmlWriter, this);
                xmlWriter.Close();
            }
            catch (Exception e)
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Close();
                }
                throw new Exception(e.Message, e);
            }
        }
        //		public string Name
        //		{
        //			get 
        //			{
        //				return myName; 
        //			}
        //			set 
        //			{
        //				myName = value; 
        //			}
        //		}


        //		public Tab GetTab(string reference)
        //		{
        //			if(reference == null || reference == "")
        //			{
        //				return (Tab)tabs[0];
        //			}
        //
        //			return InternalGetTab(tabs, reference);
        //		}

        public string GetConnectString()
        {

            string constring;

            constring = "Data Source=" + Sqlser + ";Initial Catalog=" + Databs + ";User ID=" + SqlserUser + ";Password= " + ServerKey;

            return constring;
        }


        [XmlElement("Sqlser")]
        public string Sqlser = "";
        [XmlElement("SqlserUser")]
        public string SqlserUser = "";
        [XmlElement("ServerKey")]
        public string ServerKey = "";
        [XmlElement("UserID")]
        public string UserID = "";
        [XmlElement("Username")]
        public string Username = "";
        [XmlElement("Passwd")]
        public string Passwd = "";
        [XmlElement("Databs")]
        public string Databs = "";
        [XmlElement("RegisterCode")]
        public string RegisterCode = "";
    }
}
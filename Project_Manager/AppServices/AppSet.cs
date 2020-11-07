using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient ;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.IO;
using HappyYF.Infrastructure.Repositories;

namespace Project_Manager.AppServices
{
    
    
        [XmlRoot("AppSet"), Serializable]
        public  class AppSet
        {
            private static XmlSerializer xmlDataDef = new XmlSerializer(typeof(AppSet));
            public AppSet()
            {
               
            }


            public static AppSet Load()
            {
                XmlTextReader xmlReader = null;
                AppSet appset = null;
                //Directory.GetCurrentDirectory
                try
                {

                    if (File.Exists(Directory.GetCurrentDirectory() + "\\App_Set"))
                    {
                        xmlReader = new XmlTextReader(Directory.GetCurrentDirectory() + "\\App_Set");
                        appset = (AppSet)xmlDataDef.Deserialize(xmlReader);
                        xmlReader.Close();
                    }
                    else
                        return new AppSet ();

                    
                }
                catch (Exception e)
                {
                    if (xmlReader != null)
                    {
                        xmlReader.Close();
                    }
                    throw new Exception(e.Message, e);
                }

                //参数数据库部分////////////////////



                string cString = SQLDatabase.Connectstring;
                string selAllString = "SELECT top 1 companyName,isnull(companySimpleName,'') as companySimpleName, isnull(countday,'') as countday,getdayaccrual,isnull(use_postponed,0) as use_postponed,isnull(auto_postponed,0) as auto_postponed,isnull(countline_Date,getdate()) as countline_Date  FROM T_syspar  ";


                SqlDataAdapter sysAdapter = new SqlDataAdapter(selAllString, cString);

                DataSet sys_par = new DataSet();

                try
                {
                    sysAdapter.Fill(sys_par);

                    companyName = sys_par.Tables[0].Rows[0][0].ToString();
                    companySimpleName = sys_par.Tables[0].Rows[0][1].ToString();
                    countday = sys_par.Tables[0].Rows[0][2].ToString();
                    getdayaccrual = sys_par.Tables[0].Rows[0][3].ToString();

                    use_postponed = (bool)sys_par.Tables[0].Rows[0][4];
                    auto_postponed = (bool)sys_par.Tables[0].Rows[0][5];

                    countlineDate = (DateTime)sys_par.Tables[0].Rows[0][6]  ;
                }
                catch (SqlException sqle)
                {
                    throw new Exception(sqle.Message, sqle);
                
                }

                ////////////////////////


                return appset;
            }

            public void Save()
            {
                XmlTextWriter xmlWriter = null;
                try
                {
                    xmlWriter = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\App_Set", System.Text.Encoding.UTF8);
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

                //参数数据库部分////////////////////


                //数据库部分//////////////////////

                string updString = " UPDATE T_syspar SET  companyName = '" + companyName + "',companySimpleName = '" + companySimpleName + "',countday = '" + countday + "',countline_date = '" + countlineDate.Date.ToString() + "',getdayaccrual = '" + getdayaccrual + "',use_postponed ='" + use_postponed + "',auto_postponed ='" + auto_postponed + "'";

                SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);

                SqlCommand myCom = new SqlCommand(updString, myConn);
                myCom.CommandType = CommandType.Text;

                try
                {
                    myCom.Connection.Open();
                    myCom.ExecuteNonQuery();
                    myConn.Close();


                }
                catch (SqlException e)
                {

                    throw new Exception(e.Message, e);
                }

            

                /////////////////////////

              //////////////////////////////////////
            }


            public static string companyName = "";
            public static string companySimpleName = "";
            public static string countday = "";
            public static string getdayaccrual = "";

            public static  Boolean use_postponed;
            public static Boolean auto_postponed;


            public static DateTime  countlineDate;
            //=DateTime .Today ;
          
            //[XmlElement("CompanyName")]
            //public string CompanyName = "";
            [XmlElement("AppearanceStyle")]
            public string AppearanceStyle = "";
            [XmlElement("KeyboardInputIndex")]
            public int  KeyboardInputIndex ;

            
           
        }
    }


using System;
using System.Collections.Generic;
using HappyYF.Infrastructure.RepositoryFramework;
using HappyYF.Infrastructure.DomainBase;
//using Microsoft.Practices.EnterpriseLibrary.Data.SqlCe;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data .Sql;
using System.Data.SqlClient ;
using System.Data.Common;
using HappyYF.Infrastructure.EntityFactoryFramework;
using System.Management;
using System.Windows.Forms;
using System.Transactions;

namespace HappyYF.Infrastructure.Repositories
{

    public class SQLDatabase
    {
        private SqlCommand sqlcommand;
        private SqlConnection sqlconnection;

        private static string nowUserID;

        public static string NowUserID
        {
            get { return SQLDatabase.nowUserID; }
            set { SQLDatabase.nowUserID = value; }
        }

        private static string nowmemory_rec;

        public static string Nowmemory_rec
        {
            get { return SQLDatabase.nowUserID + "_" + SQLDatabase.nowMachineName() + "_" + SQLDatabase.nowMachinecode(); }
           
        }

        public  static string nowUserName()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            string nowUser = "";


            cmd.CommandText = "SELECT yhmc FROM   T_users WHERE yhbm ='" + NowUserID +"'";
            cmd.CommandType = CommandType.Text ;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
                nowUser = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return nowUser;
        
        }

        public static string nowSalesregioncode()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            string Salesregioncode = "";


            cmd.CommandText = "SELECT salesregion_code FROM   T_users WHERE yhbm ='" + NowUserID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Salesregioncode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return Salesregioncode;

        }

        public static string nowUserDepartment()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            string UserDepartmentcode = "";


            cmd.CommandText = "SELECT bumen FROM   T_users WHERE yhbm ='" + NowUserID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            UserDepartmentcode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return UserDepartmentcode;

        }


        public static string GetUserDpt(string UID)
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            string UserDepartmentcode = "";


            cmd.CommandText = "SELECT bumen FROM   T_users WHERE yhbm ='" + UID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            UserDepartmentcode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return UserDepartmentcode;

        }










        public static string nowUserDepartmentBig()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            string UserDepartmentcode = "";


            cmd.CommandText = "SELECT case when len(bumen)>=4 then left(bumen,4)  else left(bumen,2) end  as bumen FROM   T_users WHERE yhbm ='" + NowUserID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            UserDepartmentcode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();


            if("999"==NowUserID)
            {
              UserDepartmentcode="0003";
            }
            return UserDepartmentcode;

        }
        public static string nowMachinecode()
        {


            System.Management.ManagementClass mcpu = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mcpu.GetInstances();

            string nowCPU_ID = "";

            foreach (ManagementObject mo in moc)
            {
                //MessageBox.Show(mo["processorid"].ToString());
                nowCPU_ID = nowCPU_ID + mo["processorid"].ToString();

            }
            string computer_name = System.Environment.GetEnvironmentVariable("ComputerName");

            return nowCPU_ID;

        }

        public static string nowMachineName()
        {


           
            string computer_name = System.Environment.GetEnvironmentVariable("ComputerName");

            return computer_name;

        }

        /// <summary>
        /// 根据物资编号  动态计算移动单价
        /// </summary>
        /// <param name="wzbh"></param>
        /// <returns></returns>
        public static decimal Get_AverageCostByCode(string wzbh)
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            decimal AverageCost = 0;


            cmd.CommandText = " select top 1 isnull(average_price,0) from f_Item_dynamicprice('" + wzbh+"') order by id_num desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            AverageCost = decimal.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return AverageCost;
        }



        /// <summary>
        /// 根据物资编号  装配成本
        /// </summary>
        /// <param name="wzbh"></param>
        /// <returns></returns>
        public static decimal Get_AssemblyCostByCode(string wzbh)
        {

            //SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            //SqlCommand cmd = new SqlCommand();

            decimal AssemblyCost = 0;


            //cmd.CommandText = " select top 1 isnull(average_price,0) from f_Item_dynamicprice('" + wzbh + "') order by id_num desc";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();
            //AverageCost = decimal.Parse(cmd.ExecuteScalar().ToString());
            //sqlConnection1.Close();

            return AssemblyCost;
        }



        /// <summary>
        /// 根据物资编号  机械加工成本
        /// </summary>
        /// <param name="wzbh"></param>
        /// <returns></returns>
        public static decimal Get_MachineCostByCode(string wzbh)
        {

            //SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            //SqlCommand cmd = new SqlCommand();

            decimal MachineCost = 0;


            //cmd.CommandText = " select top 1 isnull(average_price,0) from f_Item_dynamicprice('" + wzbh + "') order by id_num desc";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();
            //AverageCost = decimal.Parse(cmd.ExecuteScalar().ToString());
            //sqlConnection1.Close();

            return MachineCost;
        }



        /// <summary>
        /// 根据物资编号  外协加工成本
        /// </summary>
        /// <param name="wzbh"></param>
        /// <returns></returns>
        public static decimal Get_OutsourceCostByCode(string wzbh)
        {

            //SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            //SqlCommand cmd = new SqlCommand();

            decimal OutsourceCost = 0;


            //cmd.CommandText = " select top 1 isnull(average_price,0) from f_Item_dynamicprice('" + wzbh + "') order by id_num desc";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();
            //AverageCost = decimal.Parse(cmd.ExecuteScalar().ToString());
            //sqlConnection1.Close();

            return OutsourceCost;
        }





        public static decimal  nowUserdiscount()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            decimal Userdiscount = 0;


            cmd.CommandText = "SELECT isnull(b.diacount_value,0) as diacount_value FROM   T_users a left join ly_salesdiscount b on a.diacount_code=b.discount_code WHERE a.yhbm ='" + NowUserID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Userdiscount =decimal .Parse ( cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Userdiscount;

        }
        public static decimal nowStardandFax()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            decimal fax_rate = 0; //


            cmd.CommandText = "SELECT top 1 isnull(vat,0) FROM   T_syspar ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            fax_rate = decimal.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return fax_rate;

        }

        public static int  nowUserDrawinglevel()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            int  Userlevel = 0;


            cmd.CommandText = "SELECT isnull(a.drawing_level,0) as drawing_level FROM   T_users a  WHERE a.yhbm ='" + NowUserID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Userlevel = int.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Userlevel;

        }
        public static int nowUserMachineDrawinglevel(string nowitemno)
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            int Userlevel = 0;


            cmd.CommandText = "SELECT dbo.f_MachineDrawinglevel( '" + nowitemno + "','"+ NowUserID  + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Userlevel = int.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Userlevel;

        }


        public static decimal nowUserRepairdiscount()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            decimal UserRepdiscount = 0;


            cmd.CommandText = "SELECT isnull(a.discount_repair,0) as diacount_value FROM   T_users a  WHERE a.yhbm ='" + NowUserID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            UserRepdiscount = decimal.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return UserRepdiscount;

        }

        public static void  dataChangeREC( string  ori_id,string from_table,string change_style,string remark)
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            


            cmd.CommandText = " INSERT INTO ly_datachange_record (ori_id,from_table ,change_style ,memory_rec,remark ) " +
                              " VALUES ( " + ori_id + " ,'" + from_table + "' ,'" + change_style + "' ,'" + SQLDatabase.Nowmemory_rec + "' ,'" + remark + "')";
            
            
            
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



           

        }

        public static void drawingChangeREC(string itemno, int safe_level, string change_style, string memory_rec)
        {

            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();  




            cmd.CommandText = " INSERT INTO ly_drawing_record (itemno ,safe_level,change_style ,memory_rec,UserID ,Username,MachineName,Machinecode ) " +
                              " VALUES ( '" + itemno + "' ," + safe_level + " ,'" + change_style + "' ,'" + SQLDatabase.Nowmemory_rec + "','" + SQLDatabase.nowUserID + "','" + SQLDatabase.nowUserName() + "','" + SQLDatabase.nowMachineName() + "','" + SQLDatabase.nowMachinecode() + "')";



            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();





        }

        public static bool  updateMainplan(string materialplannum, string nowcolunm , string newvalue, string nowoprerator)
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@materialplannum", SqlDbType.VarChar);
            cmd.Parameters["@materialplannum"].Value = materialplannum;

            cmd.Parameters.Add("@nowcolunm", SqlDbType.VarChar);
            cmd.Parameters["@nowcolunm"].Value = nowcolunm;

            cmd.Parameters.Add("@newvalue", SqlDbType.VarChar);
            cmd.Parameters["@newvalue"].Value = newvalue;

            cmd.Parameters.Add("@nowoprerator", SqlDbType.VarChar);
            cmd.Parameters["@nowoprerator"].Value = nowoprerator;


            cmd.CommandText = "LY_UpdateMainplan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            int resul = 0;

            //using (TransactionScope scope = new TransactionScope())
            //{

                sqlConnection1.Open();

               

                try
                {
                    cmd.ExecuteNonQuery();
                    resul = 1;
                }

                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.ToString());
                    resul = 0;
                }


                finally
                {
                    sqlConnection1.Close();


                }
            //}

            if (1 == resul)
            {
                return true;
            }
            else
            {
                return false;
            }
            //sqlConnection1.Close();

         /////////////////////

            //string updstr = " update ly_production_order_inspection  " +
            //               "  set qualified_count=  " + queryForm.NewValue + " , waste_count=inspect_count-" + queryForm.NewValue
            //               + " where  detail_id=" + detail_Id.ToString();


            //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd = new SqlCommand();

            //cmd.CommandText = updstr;
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //int temp = 0;

            //using (TransactionScope scope = new TransactionScope())
            //{

            //    sqlConnection1.Open();
            //    try
            //    {

            //        cmd.ExecuteNonQuery();



            //        scope.Complete();
            //        temp = 1;


            //    }
            //    catch (SqlException sqle)
            //    {


            //        MessageBox.Show(sqle.Message.Split('*')[0]);
            //    }


            //    finally
            //    {
            //        sqlConnection1.Close();


            //    }
            //}




        //////////////////////////////


        }

        public static bool updatePurchaseContractmain(string nowcontractcode, string nowcolunm, string newvalue, string nowoprerator)
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@contractcode", SqlDbType.VarChar);
            cmd.Parameters["@contractcode"].Value = nowcontractcode;

            cmd.Parameters.Add("@nowcolunm", SqlDbType.VarChar);
            cmd.Parameters["@nowcolunm"].Value = nowcolunm;

            cmd.Parameters.Add("@newvalue", SqlDbType.VarChar);
            cmd.Parameters["@newvalue"].Value = newvalue;

            cmd.Parameters.Add("@nowoprerator", SqlDbType.VarChar);
            cmd.Parameters["@nowoprerator"].Value = nowoprerator;


            cmd.CommandText = "LY_UpdatePurchase_Contractmain";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            int resul = 0;

            //using (TransactionScope scope = new TransactionScope())
            //{

            sqlConnection1.Open();



            try
            {
                cmd.ExecuteNonQuery();
                resul = 1;
            }

            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message.Split('*')[1]);
                resul = 0;
            }


            finally
            {
                sqlConnection1.Close();


            }
            //}

            if (1 == resul)
            {
                return true;
            }
            else
            {
                return false;
            }
            //sqlConnection1.Close();

            /////////////////////

            //string updstr = " update ly_production_order_inspection  " +
            //               "  set qualified_count=  " + queryForm.NewValue + " , waste_count=inspect_count-" + queryForm.NewValue
            //               + " where  detail_id=" + detail_Id.ToString();


            //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd = new SqlCommand();

            //cmd.CommandText = updstr;
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //int temp = 0;

            //using (TransactionScope scope = new TransactionScope())
            //{

            //    sqlConnection1.Open();
            //    try
            //    {

            //        cmd.ExecuteNonQuery();



            //        scope.Complete();
            //        temp = 1;


            //    }
            //    catch (SqlException sqle)
            //    {


            //        MessageBox.Show(sqle.Message.Split('*')[0]);
            //    }


            //    finally
            //    {
            //        sqlConnection1.Close();


            //    }
            //}




            //////////////////////////////


        }

        public static bool updateOutsourceContractmain(string nowcontractcode, string nowcolunm, string newvalue, string nowoprerator)
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@contractcode", SqlDbType.VarChar);
            cmd.Parameters["@contractcode"].Value = nowcontractcode;

            cmd.Parameters.Add("@nowcolunm", SqlDbType.VarChar);
            cmd.Parameters["@nowcolunm"].Value = nowcolunm;

            cmd.Parameters.Add("@newvalue", SqlDbType.VarChar);
            cmd.Parameters["@newvalue"].Value = newvalue;

            cmd.Parameters.Add("@nowoprerator", SqlDbType.VarChar);
            cmd.Parameters["@nowoprerator"].Value = nowoprerator;


            cmd.CommandText = "LY_UpdateOutsource_Contractmain";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            int resul = 0;

            //using (TransactionScope scope = new TransactionScope())
            //{

            sqlConnection1.Open();



            try
            {
                cmd.ExecuteNonQuery();
                resul = 1;
            }

            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message.Split('*')[1]);
                resul = 0;
            }


            finally
            {
                sqlConnection1.Close();


            }
            //}

            if (1 == resul)
            {
                return true;
            }
            else
            {
                return false;
            }
            //sqlConnection1.Close();

            /////////////////////

            //string updstr = " update ly_production_order_inspection  " +
            //               "  set qualified_count=  " + queryForm.NewValue + " , waste_count=inspect_count-" + queryForm.NewValue
            //               + " where  detail_id=" + detail_Id.ToString();


            //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd = new SqlCommand();

            //cmd.CommandText = updstr;
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //int temp = 0;

            //using (TransactionScope scope = new TransactionScope())
            //{

            //    sqlConnection1.Open();
            //    try
            //    {

            //        cmd.ExecuteNonQuery();



            //        scope.Complete();
            //        temp = 1;


            //    }
            //    catch (SqlException sqle)
            //    {


            //        MessageBox.Show(sqle.Message.Split('*')[0]);
            //    }


            //    finally
            //    {
            //        sqlConnection1.Close();


            //    }
            //}




            //////////////////////////////


        }

        /// <summary>
        /// //////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        public static DateTime GetNowdate()
        {
            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            DateTime Nowdate = DateTime.Now.Date;

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "select getdate() ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Nowdate = DateTime.Parse(cmd.ExecuteScalar().ToString()).Date;
            sqlConnection1.Close();



            return Nowdate;

        }

        public static DateTime GetNowtime()
        {
            SqlConnection sqlConnection1 = new SqlConnection(Connectstring);
            SqlCommand cmd = new SqlCommand();

            DateTime Nowdate = DateTime.Now.Date;

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "select getdate() ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Nowdate = DateTime.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Nowdate;

        }
        
        SqlDataReader nowReader;

        public SqlDataReader NowReader
        {
            get { return nowReader; }
            set { nowReader = value; }
        }

        private static string connectstring;

        public static string Connectstring
        {
            get { return SQLDatabase.connectstring; }
            set { SQLDatabase.connectstring = value; }
        }

        public SQLDatabase()
        {
            sqlcommand = new SqlCommand();
            sqlconnection = new SqlConnection(SQLDatabase.Connectstring);
            sqlcommand.Connection = sqlconnection;
        }

        //public SqlCommand Sqlcommand
        //{
        //    get { return sqlcommand; }
        //    set { sqlcommand = value; }
        //}
       
        
        public SqlCommand GetSqlStringCommand(string sql)
       {
           sqlcommand.CommandText = sql;
           //sqlcommand.Connection.ConnectionString = SQLDatabase.connectstring;
          
            return sqlcommand;
       
       }

        public SqlDataReader ExecuteReader(SqlCommand command)
        {
          
            //sqlconnection.Open();
            if (sqlconnection.State == ConnectionState.Open) sqlconnection.Close(); 
            command.Connection.Open();
            SqlDataReader NowReader1 = command.ExecuteReader();
           
            return NowReader1;
            //command.Connection.Close();
        }

        public int ExecuteNonQuery(DbCommand command)
        {

            if (sqlconnection.State == ConnectionState.Open) sqlconnection.Close(); 
            sqlconnection.Open();
                int ii=command.ExecuteNonQuery();
            sqlconnection.Close ();

            return ii;

        }
        public static bool CheckHaveRight(string yh, string nowMenu)
        {
            string cString = Connectstring;



            string selAllString = "SELECT    *  from  T_gongnengshouquan  where yonghu_ID = '" + yh + "' and control_name = '" + nowMenu + "'";


            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

            if (nowData.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;




        }

        public static bool CheckBOMConfliect(string nowparent , string nowchild)
        {
            string cString = Connectstring;



            string selAllString = "select * from f_BomExtend('" + nowchild + "',1)  where itemno='" + nowparent + "'";
                
                 

            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

            if (nowData.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;




        }

        public static bool CheckNeedInform()
        {
            string cString = Connectstring;



            string selAllString = "select * from ly_inform_userlist where usercode = '" + NowUserID + "'";


            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

            if (nowData.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;




        }

        public static bool CheckHaveInform()
        {
            string cString = Connectstring;



            string selAllString = "select * from ly_inform_record  where  isnull(acceptflag,0) =0 and informpersoncode = '" + NowUserID + "'";


            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

            if (nowData.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;




        }
        //////CheckNoaccceptInform

        public static bool CheckNoaccceptInform()
        {
            string cString = Connectstring;



            string selAllString = "select * from ly_inform_record  where  DATEDIFF ( minute , informtime , getdate() )>(select top 1 informwaitingtime from T_syspar) and isnull(acceptflag,0) =0 and informcontent = '" + SQLDatabase.nowUserName() + "'";


            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

            if (nowData.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;




        }


    }

    public class DatabaseFactory
    {
        public static SQLDatabase CreateDatabase()
        {

            return new SQLDatabase();
        
        }
    
    }
    
    public abstract class SqlServerRepositoryBase<T> : RepositoryBase<T>
        where T : EntityBase
    {
        #region AppendChildData Delegate

        /// <summary>
        /// The delegate signature required for callback methods
        /// </summary>
        /// <param name="entityAggregate"></param>
        /// <param name="childEntityKey"></param>
        public delegate void AppendChildData(T entityAggregate,
            object childEntityKeyValue);

        #endregion

        #region Private Members

        private SQLDatabase database;
        private IEntityFactory<T> entityFactory;
        private Dictionary<string, AppendChildData> childCallbacks;

        #endregion

        #region Constructors

        protected SqlServerRepositoryBase()
            : this(null)
        {
        }

        protected SqlServerRepositoryBase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.database = DatabaseFactory.CreateDatabase();
            this.entityFactory = EntityFactoryBuilder.BuildFactory<T>();
            this.childCallbacks = new Dictionary<string, AppendChildData>();
            this.BuildChildCallbacks();
        }

        #endregion

        #region Abstract Methods

        protected abstract void BuildChildCallbacks();
        public abstract override T FindBy(object key);
        protected abstract override void PersistNewItem(T item);
        protected abstract override void PersistUpdatedItem(T item);
        protected abstract override void PersistDeletedItem(T item);

        #endregion

        #region Properties

        protected SQLDatabase Database
        {
            get { return this.database; }
        }

        protected Dictionary<string, AppendChildData> ChildCallbacks
        {
            get { return this.childCallbacks; }
        }

        #endregion

        #region Protected Methods

        protected SqlDataReader ExecuteReader(string sql)
        {
            SqlCommand command = this.database.GetSqlStringCommand(sql);

            SqlDataReader sd =this.database.ExecuteReader(command);
            return sd;
        }

        protected virtual T BuildEntityFromSql(string sql)
        {
            T entity = default(T);
           using (IDataReader reader = this.ExecuteReader(sql))
          
            {
                if (reader.Read())
              
                {
                    entity = this.BuildEntityFromReader(reader);
                }
                reader.Close();
            }

           
            return entity;
        }

        protected virtual T BuildEntityFromReader(IDataReader reader)
        {
            T entity = this.entityFactory.BuildEntity(reader);
            if (this.childCallbacks != null && this.childCallbacks.Count > 0)
            {
                object childKeyValue = null;
                DataTable columnData = reader.GetSchemaTable();
                foreach (string childKeyName in this.childCallbacks.Keys)
                {
                    if (DataHelper.ReaderContainsColumnName(columnData,
                        childKeyName))
                    {
                        childKeyValue = reader[childKeyName];
                    }
                    else
                    {
                        childKeyValue = null;
                    }
                    this.childCallbacks[childKeyName](entity, childKeyValue);
                }
            }
            return entity;
        }

        protected virtual List<T> BuildEntitiesFromSql(string sql)
        {
            List<T> entities = new List<T>();
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    entities.Add(this.BuildEntityFromReader(reader));
                }
            }
            return entities;
        }

        #endregion
    }
}

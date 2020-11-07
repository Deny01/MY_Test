using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


using System.IO;
//using System.Security.Cryptography;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;




namespace HappyYF.VehicleBudgets
{
    //[Serializable]
    //public enum Vehicle_Model
    //{
    //    整车5到10,
    //    半挂5到10,
    //    整车10以上,
    //    半挂10以上,
    //    特一,
    //    特二,
    //}

    
    //[Serializable]
    //public enum Have_deposit
    //{
    //    有保证金,
    //    无保证金,
    //    保证金,
    //}

    //[Serializable]
    //public enum Loan_Fixed_year
    //{
    //    一年,
    //    二年,
    //    三年,
    //}

    //[Serializable]
    //public enum Client_Area
    //{
    //    市内,
    //    市郊,
    //    市外,
    //}


    [XmlRoot("VehicleBudget"), Serializable]
    public class VehicleBudget
    {

        private static XmlSerializer xmlDataDef = new XmlSerializer(typeof(VehicleBudget));
        ////业务编号
        //string cardnumber;

        //public VehicleBudget( string yewubianhao)
        //{
        //    this.cardnumber = yewubianhao;
        //}

        //public string Cardnumber
        //{
        //    get { return cardnumber; }
        //    set { cardnumber = value; }
        //}

        ////车号
        //string vehiclecode;

        //public string Vehiclecode
        //{
        //    get { return vehiclecode; }
        //    set { vehiclecode = value; }
        //}

        //车种
        [XmlElement("vehicle_model")]
        string  vehicle_model;

        public string  Vehicle_model
        {
            get { return vehicle_model; }
            set { vehicle_model = value; }
        }

        //居住地
        [XmlElement("address_area")]
        string  address_area;

        public string  Address_area
        {
            get { return address_area; }
            set { address_area = value; }
        }

        //挂靠地
        [XmlElement("belong_area")]
       string  belong_area;

       public string Belong_area
        {
            get { return belong_area; }
            set { belong_area = value; }
        }

        //贷款比例
       [XmlElement("loan_Proportion")]
        decimal loan_Proportion=0;

        public decimal Loan_Proportion
        {
            get { return loan_Proportion; }
            set { loan_Proportion = value; }
        }
        
        //头车价
        [XmlElement("head_price")]
        decimal head_price = 0;

        public decimal Head_price
        {
            get { return head_price; }
            set { head_price = value; }
        }

        //挂车价
        [XmlElement("tail_price")]
        decimal tail_price = 0;

        public decimal Tail_price
        {
            get { return tail_price; }
            set { tail_price = value; }
        }

      
        ////车价
        //public decimal Vehicle_Price
        //{
        //    get { return head_price + tail_price; }
        //    //set { vehicle_Price = value; }
        //}

        ////首付款
        //public decimal First_Payment
        //{
        //    get { return Vehicle_Price - Loan_Money; }
        //    //set { vehicle_Price = value; }
        //}

        ////贷款额
        //public decimal Loan_Money
        //{
        //    get {
        //          return int .Parse ((Vehicle_Price * loan_Proportion/1000).ToString ().Split ('.')[0]) * 1000;
        //         }
        //    //set { vehicle_Price = value; }
        //}


        //贷款年限
        [XmlElement("loan_fixed_year")]
        string    loan_fixed_year;

        public string Loan_fixed_year
        {
            get { return loan_fixed_year; }
            set { loan_fixed_year = value; }
        }

        //每月还款 和年限关联
        //decimal month_return;

        //public decimal Month_return
        //{
        //    get { return month_return; }
        //    set { month_return = value; }
        //}

        //担保费率
        [XmlElement("sponsion_ratio")]
        decimal sponsion_ratio;

        public decimal Sponsion_ratio
        {
            get { return sponsion_ratio; }
            set { sponsion_ratio = value; }
        }


        //GPS安装费 和车型关联
        //private decimal gps_fee;

        //public decimal Gps_fee
        //{
        //    get { return gps_fee; }
        //    set { gps_fee = value; }
        //}


        //有无保证金
        [XmlElement("have_deposit")]
        string have_deposit;

        public string Have_deposit
        {
            get { return have_deposit; }
            set { have_deposit = value; }
        }
        //抵押登记费home_call 和车型关联

        //调查资料费：Banlbook 和居住地相关联

        //公证费 和年限关联
        //公证费因子基数Justness

        //decimal justness_gene;

        //public decimal Justness_gene
        //{
        //    get { return justness_gene; }
        //    set { justness_gene = value; }
        //}
        //decimal justness_radix;

        //public decimal Justness_radix
        //{
        //    get { return justness_radix; }
        //    set { justness_radix = value; }
        //}


        //头车数
        [XmlElement("head_number")]
        int head_number;

        public int Head_number
        {
            get { return head_number; }
            set { head_number = value; }
        }

        
        //挂车数
        [XmlElement("tail_number")]
        int tail_number;

        public int Tail_number
        {
            get { return tail_number; }
            set { tail_number = value; }
        }

        //车船税因子 insur_chesun_radix
        [XmlElement("insurance_head")]
        decimal insurance_head;

        public decimal Insurance_head
        {
            get { return insurance_head; }
            set { insurance_head = value; }
        }
        [XmlElement("insurance_tail")]
        decimal insurance_tail;

        public decimal Insurance_tail
        {
            get { return insurance_tail; }
            set { insurance_tail = value; }
        }

        //上户费 和车型关联
        //private decimal register;

        //public decimal Register
        //{
        //    get { return register; }
        //    set { register = value; }
        //}

        //续保押金
        [XmlElement("foregift_item")]
        string foregift_item;

        public string Foregift_item
        {
            get { return foregift_item; }
            set { foregift_item = value; }
        }
        //private decimal foregift;

        //public decimal Foregift
        //{
        //    get { return foregift; }
        //    set { foregift = value; }
        //}

        //车辆购置税率 和车型相关联
        //private decimal purchase_tax_ratio;

        //public decimal Purchase_tax_ratio
        //{
        //    get { return purchase_tax_ratio; }
        //    set { purchase_tax_ratio = value; }
        //}

        //贷后管理费比例
        [XmlElement("loan_operational_ratio")]
        private decimal loan_operational_ratio;

        public decimal Loan_operational_ratio
        {
            get { return loan_operational_ratio; }
            set { loan_operational_ratio = value; }
        }

       // private decimal loan_operational_gene;和车型关联 84/7


        //垫款利息比例 和车型关联
        //private decimal sell_accrual_ratio;

        //public decimal Sell_accrual_ratio
        //{
        //    get { return sell_accrual_ratio; }
        //    set { sell_accrual_ratio = value; }
        //}

        //贷款利息
        [XmlElement("loan_accrual_item")]
        private string loan_accrual_item;

        public string Loan_accrual_item
        {
            get { return loan_accrual_item; }
            set { loan_accrual_item = value; }
        }

     
     

        /////////////////////
        //方法

        ////保证金
        //public decimal Vehicle_Deposit(string have_deposit)
        //{

        //    return 0;
        //}

        ////抵押登记费home_call

        //public decimal Vehicle_Home_call(string vehicle_model)
        //{

        //    return 0;
        //}

        ////调查资料费 Bankbook
        //public decimal Vehicle_Bankbook(string address_area)
        //{

        //    return 0;
        //}


      

        ///////////////////////////////////////////////////////
        //       保险部分                                    //
        ///////////////////////////////////////////////////////

        //车损

        //decimal insur_chesun_gene;

        //public decimal Insur_chesun_gene
        //{
        //    get { return insur_chesun_gene; }
        //    set { insur_chesun_gene = value; }
        //}
        //decimal insur_chesun_radix;

        //public decimal Insur_chesun_radix
        //{
        //    get { return insur_chesun_radix; }
        //    set { insur_chesun_radix = value; }
        //}

        //三责
        [XmlElement("insur_sanze_item")]
        string insur_sanze_item;

        public string Insur_sanze_item
        {
            get { return insur_sanze_item; }
            set { insur_sanze_item = value; }
        }

       

     
      

       
        //盗抢

        //decimal insur_daoqiang_gene;

        //public decimal Insur_daoqiang_gene
        //{
        //    get { return insur_daoqiang_gene; }
        //    set { insur_daoqiang_gene = value; }
        //}
        //decimal insur_daoqiang_radix;

        //public decimal Insur_daoqiang_radix
        //{
        //    get { return insur_daoqiang_radix; }
        //    set { insur_daoqiang_radix = value; }
        //}

        

        //自燃

        //decimal insur_ziran_gene;

        //public decimal Insur_ziran_gene
        //{
        //    get { return insur_ziran_gene; }
        //    set { insur_ziran_gene = value; }
        //}
        //decimal insur_ziran_radix;

        //public decimal Insur_ziran_radix
        //{
        //    get { return insur_ziran_radix; }
        //    set { insur_ziran_radix = value; }
        //}


        //乘座
        [XmlElement("insur_chengzuo_number")]
        int insur_chengzuo_number;

        public int Insur_chengzuo_number
        {
            get { return insur_chengzuo_number; }
            set { insur_chengzuo_number = value; }
        }

        //交强

        //decimal insur_jiaoqiang_radix;

        //public decimal Insur_jiaoqiang_radix
        //{
        //    get { return insur_jiaoqiang_radix; }
        //    set { insur_jiaoqiang_radix = value; }
        //}


        //货物
        [XmlElement("insur_huowu_item")]
        string insur_huowu_item;

        public string Insur_huowu_item
        {
            get { return insur_huowu_item; }
            set { insur_huowu_item = value; }
        }

     

        /////////////////////////
        //方法
        //////////////////////////

        //三责
        //public decimal Vehicle_Sanze_Head(string vehicle_model, string sanze_stle)
        //{


        //    return 0;
        //}
        //public decimal Vehicle_Sanze_Tail(string vehicle_model, string sanze_stle)
        //{


        //    return 0;
        //}

        //交强

        //public decimal Vehicle_Jiaoqiang_Head(string vehicle_model)
        //{


        //    return 0;
        //}
        //public decimal Vehicle_Jiaoqiang_Tail(string vehicle_model)
        //{


        //    return 0;
        //}

        public static VehicleBudget Load()
        {
            XmlTextReader xmlReader = null;
            VehicleBudget vb = null;
            //Directory.GetCurrentDirectory
            try
            {

                if (File.Exists(Directory.GetCurrentDirectory() + "\\LastBudget"))
                {
                    xmlReader = new XmlTextReader(Directory.GetCurrentDirectory() + "\\LastBudget");
                    vb = (VehicleBudget)xmlDataDef.Deserialize(xmlReader);
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


            return vb ;
        }

        public void Save()
        {
            XmlTextWriter xmlWriter = null;
            try
            {
                xmlWriter = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\LastBudget", System.Text.Encoding.UTF8);
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
     

       

    }

    //public class VehicleBudget_Expenses
    //{
      
    //   private  VehicleBudget nowVehicle;


    //   public VehicleBudget_Expenses(VehicleBudget inVehicle)
    //    {

    //        this.nowVehicle = inVehicle;
    //    }

    //    //公司担保费
    //   //private decimal sponsion;

    //   public decimal Sponsion
    //   {
    //       get { 
               
               
    //           return this .ConverValue ( nowVehicle.Loan_Money * nowVehicle.Sponsion_ratio);
           
    //       }
    //       //set { sponsion = value; }
    //   }

    //    //GPS安装费
    //   //private decimal gps_fee;

    //   public decimal Gps_fee
    //   {
    //       get { return nowVehicle.Gps_fee; }
    //       //set { gps_fee = value; }
    //   }

    //    //保证金
    //   //private decimal deposit;

    //   public decimal Deposit
    //   {
    //       get { 
    //           return this .ConverValue ( nowVehicle .Vehicle_Deposit(nowVehicle .Have_deposit )) ; 
           
    //          }
    //       //set { deposit = value; }
    //   }

    //    //抵押登记费
    //   private decimal home_call;

    //   public decimal Home_call
    //   {
    //       get {
    //           return this.ConverValue( nowVehicle .Vehicle_Home_call(nowVehicle .Vehicle_model));
               
    //          }
    //       //set { home_call = value; }
    //   }

    //    //调查资料费
    //   private decimal bankbook;

    //   public decimal Bankbook
    //   {
    //       get {
    //           return this.ConverValue(nowVehicle .Vehicle_Bankbook(nowVehicle .Address_area));
           
    //       }
    //       //set { bankbook = value; }
    //   }

    //   //上牌费
    //   private decimal shangpai;

    //   public decimal Shangpai
    //   {
    //       get
    //       {
    //           return this.ConverValue(nowVehicle.Vehicle_Bankbook(nowVehicle.Address_area));

    //       }
    //       //set { bankbook = value; }
    //   }

    //    //公正费
    //   private decimal justness;

    //   public decimal Justness
    //   {
    //       get {
    //           return this.ConverValue(nowVehicle .Loan_Money * nowVehicle .Justness_gene + nowVehicle .Justness_radix);
           
    //           }
    //       //set { justness = value; }
    //   }

    //    //车船使用税
    //   //private decimal insurance_head;

    //   public decimal Insurance_Head
    //   {
    //       get {
    //           decimal temp_value;
    //             temp_value = nowVehicle .Head_number * 84 /12 * (13 - nowVehicle .Chechuanyizi_head  );
    //             return this.ConverValue(temp_value);
           
    //            }
    //       //set { insurance = value; }
    //   }
    //   //private decimal insurance_tail;

    //   public decimal Insurance_tail
    //   {
    //       get
    //       {
    //           decimal temp_value;
    //           temp_value = nowVehicle.Head_number * 84 / 12 * (13 - nowVehicle.Chechuanyizi_tail );
    //           return this.ConverValue(temp_value);
    //       }
    //       //set { insurance_tail = value; }
    //   }
    //   //private decimal insurance;

    //   public decimal Insurance
    //   {
    //       get { return this .Insurance_Head + this .Insurance_tail ; }
    //       //set { insurance = value; }
    //   }

    //    //上户费
    //   //private decimal register;

    //   public decimal Register
    //   {
    //       get { return nowVehicle .Register ; }
    //       //set { register = value; }
    //   }

    //    //续保押金
    //   //private decimal foregift;

    //   public decimal Foregift
    //   {
    //       get { return nowVehicle .Foregift ; }
    //       //set { foregift = value; }
    //   }

    //    //车辆购置税
    //   //private decimal purchase_tax;

    //   public decimal Purchase_tax
    //   {
    //       get { return this .ConverValue (nowVehicle .Vehicle_Price * nowVehicle .Purchase_tax_ratio ); }
    //       //set { purchase_tax = value; }
    //   }

    //    //贷后管理费
    //   //private decimal loan_operational_money;

    //   public decimal Loan_operational_money
    //   {
    //       get { return 0; }
    //       //set { loan_operational_money = value; }
    //   }

    //    //垫款利息
    //   //private decimal sell_accrual;

    //   public decimal Sell_accrual
    //   {
    //       get { return this .ConverValue (nowVehicle .Loan_Money * nowVehicle .Sell_accrual_ratio); }
    //       //set { sell_accrual = value; }
    //   }

    //    //贷款利息
    //    //private decimal loan_accrual;

    //    //其它
    //   private decimal else_money;

    //    public decimal Else_money
    //    {
    //        get { return else_money; }
    //        set { else_money = value; }
    //    }

    //    ///////////////////////////////////////////////

    //    public decimal ConverValue(decimal temp_value)
    //    {
    //        decimal return_value;

    //        return_value = int.Parse(temp_value.ToString().Split('.')[0]);

    //        if (return_value == temp_value)
    //            return return_value;
    //        else
    //            return return_value + 1;
    //    }



    //}

    //public class VehicleBudget_Insurance
    //{
    //     private  VehicleBudget nowVehicle;


    //     public VehicleBudget_Insurance(VehicleBudget inVehicle)
    //    {

    //        this.nowVehicle = inVehicle;
    //    }

    //    //车损
    //     //decimal insur_chesun_head;

    //     public decimal Insur_chesun_head
    //     {
    //         get { return this .ConverValue (nowVehicle .Head_price * nowVehicle .Insur_chesun_gene/100 + nowVehicle .Insur_chesun_radix ); }
    //         //set { insur_chesun = value; }
    //     }

    //     //decimal insur_chesun_tail;

    //     public decimal Insur_chesun_tail
    //     {
    //         get { return this.ConverValue(nowVehicle.Tail_price  * nowVehicle.Insur_chesun_gene / 100 + nowVehicle.Insur_chesun_radix); }
    //         //set { insur_chesun_tail = value; }
    //     }
    //     //decimal insur_chesun;

    //     public decimal Insur_chesun
    //     {
    //         get { return this .Insur_chesun_head + this .Insur_chesun_tail; }
    //         //set { insur_chesun = value; }
    //     }
    //    //三责
    //     //decimal insur_sanze_head;

    //     public decimal Insur_sanze_head
    //     {
    //         get { return this .ConverValue (nowVehicle .Vehicle_Sanze_Head(nowVehicle .Vehicle_model,"")); }
    //         //set { insur_sanze = value; }
    //     }
    //     //decimal insur_sanze_tail;

    //     public decimal Insur_sanze_tail
    //     {
    //         get { return this.ConverValue(nowVehicle.Vehicle_Sanze_Tail(nowVehicle.Vehicle_model, "")); }
    //         //set { insur_sanze = value; }
    //     }
    //     //decimal insur_sanze;

    //     public decimal Insur_sanze
    //     {
    //         get { return this .Insur_sanze_head + this .Insur_sanze_tail; }
    //         //set { insur_sanze = value; }
    //     }
    //     //decimal insur_sanze_add;

    //     public decimal Insur_sanze_add
    //     {
    //         get { return this .ConverValue (0  * 0); }
    //         //set { insur_sanze_add = value; }
    //     }
    //    //盗抢
       
    //     //decimal insur_daoqiang_head;

    //     public decimal Insur_daoqiang_head
    //     {
    //         get { return this.ConverValue(nowVehicle.Head_price * nowVehicle.Insur_daoqiang_gene / 1000 + nowVehicle.Insur_daoqiang_radix); }
    //         //set { insur_chesun = value; }
    //     }

    //     //decimal insur_daoqiang_tail;

    //     public decimal Insur_daoqiang_tail
    //     {
    //         get { return this.ConverValue(nowVehicle.Tail_price * nowVehicle.Insur_daoqiang_gene / 1000/2 + nowVehicle.Insur_daoqiang_radix/2); }
    //         //set { insur_chesun_tail = value; }
    //     }
    //     //decimal insur_daoqiang;

    //     public decimal Insur_daoqiang
    //     {
    //         get { return this.Insur_daoqiang_head + this.Insur_daoqiang_tail; }
    //         //set { insur_chesun = value; }
    //     }
    //    //自燃
    //     //decimal insur_ziran_head;

    //     public decimal Insur_ziran_head
    //     {
    //         get { return this.ConverValue(nowVehicle.Head_price * nowVehicle.Insur_ziran_gene / 1000 + nowVehicle.Insur_ziran_radix); }
    //         //set { insur_chesun = value; }
    //     }

    //     //decimal insur_ziran_tail;

    //     public decimal Insur_ziran_tail
    //     {
    //         get { return this.ConverValue(nowVehicle.Tail_price * nowVehicle.Insur_ziran_gene / 1000  + nowVehicle.Insur_ziran_radix); }
    //         //set { insur_chesun_tail = value; }
    //     }
    //     //decimal insur_ziran;

    //     public decimal Insur_ziran
    //     {
    //         get { return this.Insur_ziran_head + this.Insur_ziran_tail; }
    //         //set { insur_chesun = value; }
    //     }
    //    //乘座
    //    //交强
    //     //decimal insur_jiaoqiang_head;

    //     public decimal Insur_jiaoqiang_head
    //     {
    //         get { return this.ConverValue(nowVehicle .Vehicle_Jiaoqiang_Head(nowVehicle .Vehicle_model)); }
    //         //set { insur_chesun = value; }
    //     }

    //     //decimal insur_jiaoqiang_tail;

    //     public decimal Insur_jiaoqiang_tail
    //     {
    //         get { return this.ConverValue(nowVehicle.Vehicle_Jiaoqiang_Tail(nowVehicle .Vehicle_model)); }
    //         //set { insur_chesun_tail = value; }
    //     }
    //     //decimal insur_jiaoqiang;

    //     public decimal Insur_jiaoqiang
    //     {
    //         get { return this.Insur_jiaoqiang_head + this.Insur_jiaoqiang_tail; }
    //         //set { insur_chesun = value; }
    //     }
    //    //货物

    //     //decimal insur_huowu_head;

    //     public decimal Insur_huowu_head
    //     {
    //         get { return this.ConverValue(0); }
    //         //set { insur_chesun = value; }
    //     }

    //     //decimal insur_huowu_tail;

    //     public decimal Insur_huowu_tail
    //     {
    //         get { return this.ConverValue(0); }
    //         //set { insur_chesun_tail = value; }
    //     }
    //     //decimal insur_huowu;

    //     public decimal Insur_huowu
    //     {
    //         get { return this.Insur_huowu_head + this.Insur_huowu_tail; }
    //         //set { insur_chesun = value; }
    //     }
    //    //不计免赔

    //     //decimal insur_mianpei_head;

    //     public decimal Insur_mianpei_head
    //     {
    //         get { return this .ConverValue ((this .Insur_chesun_head + this.Insur_sanze_head + this .Insur_sanze_add) * 15 /100); }
    //         //set { insur_chesun = value; }
    //     }

    //     //decimal insur_mianpei_tail;

    //     public decimal Insur_mianpei_tail
    //     {
    //         get { return this.ConverValue((this.Insur_chesun_tail+ this.Insur_sanze_tail ) * 15 / 100); }
    //         //set { insur_chesun_tail = value; }
    //     }
    //     //decimal insur_mianpei;

    //     public decimal Insur_mianpei
    //     {
    //         get { return this.Insur_mianpei_head + this.Insur_mianpei_tail; }
    //         //set { insur_chesun = value; }
    //     }

    //     ///////////////////////////////////////////////

    //     public decimal ConverValue(decimal temp_value)
    //     {
    //         decimal return_value;

    //         return_value = int.Parse(temp_value.ToString().Split('.')[0]);

    //         if (return_value == temp_value)
    //             return return_value;
    //         else
    //             return return_value + 1;
    //     }



    //}

}

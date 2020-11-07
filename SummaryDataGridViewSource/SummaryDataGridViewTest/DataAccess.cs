using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SummaryDataGridViewTest
{
    class DataAccess
    {
        private static DataSet ds;

        private DataAccess() { }

		public static DataSet SampleData
		{
			get 
			{
				if (ds == null)
					ds = CreateDataSet();
				return ds;
			}
		}

		private static DataSet CreateDataSet() 
		{
			DataSet ds = new DataSet();

			IDbConnection connection = new System.Data.OleDb.OleDbConnection();
			connection.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Database Password=;Data Source=""NWind.mdb"";Password=;Jet OLEDB:Engine Type=3;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";

			IDbDataAdapter adapterSender = new System.Data.OleDb.OleDbDataAdapter();
			IDbCommand oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			oleDbSelectCommand1.Connection = connection;
			adapterSender.SelectCommand = oleDbSelectCommand1;

            oleDbSelectCommand1.CommandText = "SELECT TOP 10 OrderID, CustomerID , Freight, OrderDate, ShipVia  FROM Orders";
            //oleDbSelectCommand1.CommandText = "SELECT *  FROM Orders";
			adapterSender.Fill(ds);
			ds.Tables[0].TableName = "Orders";
				
			return ds;
		}
    }
}

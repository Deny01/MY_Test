	using System;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.ComponentModel.Design.Serialization;
	using System.Diagnostics;
	using System.Collections;
	using System.Windows.Forms;
	using System.Reflection;
	using System.Text;
using KReport.Engine;

namespace KReport.UndoRedo
{
	/// Designer transactions offer a mechanism to improve performance by wrapping around
	/// a series of component changes. The changes are not actually committed until the
	/// entire batch is processed. They can be aborted if the transaction is canceled.
	internal class ReportDesignerTransaction : DesignerTransaction 
	{
        //private Report report;
        private DesignerReport report;

        public ReportDesignerTransaction(DesignerReport report, string description)
            : base(description) 
		{
			this.report = report;

			// The report keeps a string stack of the transaction descriptions.
			report.TransactionDescriptions.Push(description);

			// If this is first transaction to be opened, have the report raise
			// opening/opened events.
			//
			if (report.TransactionCount++ == 0) 
			{
				report.OnTransactionOpening(EventArgs.Empty);
				report.OnTransactionOpened(EventArgs.Empty);
			}
		}

		protected override void OnCancel() 
		{
			if (report != null) 
			{
				Debug.Assert(report.TransactionDescriptions != null, "End batch operation with no desription?!?");

				// If this is the last transaction to be closed, have the report raise
				// closing/closed events.
				//
				if (--report.TransactionCount == 0) 
				{
					DesignerTransactionCloseEventArgs dtc = new DesignerTransactionCloseEventArgs(false);
					report.OnTransactionClosing(dtc);
					report.OnTransactionClosed(dtc);
				}

				string s =  (string)report.TransactionDescriptions.Pop();
				report = null;
			}
		}

		protected override void OnCommit() 
		{
			if (report != null) 
			{
				Debug.Assert(report.TransactionDescriptions != null, "End batch operation with no desription?!?");

				// If this is the last transaction to be closed, have the report raise
				// closing/closed events.
				//
				if (--report.TransactionCount == 0) 
				{
					DesignerTransactionCloseEventArgs dtc = new DesignerTransactionCloseEventArgs(true);
					report.OnTransactionClosing(dtc);
					report.OnTransactionClosed(dtc);
				}

				string s =  (string)report.TransactionDescriptions.Pop();
				report = null;
			}
		}
	}
}

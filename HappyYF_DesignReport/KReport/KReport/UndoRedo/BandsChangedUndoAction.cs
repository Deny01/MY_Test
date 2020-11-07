

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml;
using System.ComponentModel.Design.Serialization;
using KReport.Engine;


namespace KReport.UndoRedo
{
	
	public class BandsChangedUndoAction : KReport.UndoRedo.IUndoableOperation
	{
        DesignerReport report;
		
		ArrayList oldComponentNames;
		ArrayList newComponentNames;

        //MemberDescriptor member;

        public BandsChangedUndoAction(DesignerReport report, ArrayList oldComponentNames)
		{
			this.report     = report;
			this.oldComponentNames = oldComponentNames;
		}
		
		public void SetNewSelection(ArrayList newComponentNames)
		{
			this.newComponentNames = newComponentNames;
		}
		
		public void Undo()
		{
            UndoHandler.SetBandsLocation(report, oldComponentNames);

        
		}
		
		public void Redo()
		{
            UndoHandler.SetBandsLocation(report, newComponentNames);

        
		}
	}
}


// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version value="$version"/>
// </file>

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
	
	public class SelectComponentsUndoAction : KReport.UndoRedo.IUndoableOperation
	{
        DesignerReport report;
		
		ArrayList oldComponentNames;
		ArrayList newComponentNames;

        public SelectComponentsUndoAction(DesignerReport report, ArrayList oldComponentNames)
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
			UndoHandler.SetSelectedComponentsPerName(report, oldComponentNames);
		}
		
		public void Redo()
		{
			UndoHandler.SetSelectedComponentsPerName(report, newComponentNames);
		}
	}
}

// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Kr¸ger" email="mike@icsharpcode.net"/>
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
	
	public class ComponentRemovedUndoAction : IUndoableOperation
	{
        DesignerReport report;
		
		Type   componentType;
		string componentName;
		Control oldComponent;
		int cx, cy;
		int width, heigh;
		string strFormat = null;

        public ComponentRemovedUndoAction(DesignerReport report, ComponentEventArgs cea)
		{
			this.report     = report;
			oldComponent  = (Control)cea.Component;
			componentType = cea.Component.GetType();

			cx = oldComponent.Location.X;
			cy = oldComponent.Location.Y;
			width = oldComponent.Width;
			heigh = oldComponent.Height;
			if(oldComponent is System.Windows.Forms.Label)
			{
				strFormat = report.GetPropertyValue(oldComponent.Name, "Format");
			}
		}
		
		public void Undo()
		{

           

             if (oldComponent is CustomControl)
                {
                    BandBase base2 = report.FindBandPosY(((CustomControl)oldComponent).Location.Y);
                    ((CustomControl)oldComponent).Band = base2;
                    ((CustomControl)oldComponent).¥π÷±Œª÷√ = ((CustomControl)oldComponent).Location.Y - ((CustomControl)oldComponent).Band.Top;
                }
                report.ParentControl.Controls.Add(oldComponent as Control);
                report.SetCurrentControl(oldComponent as Control);
                return;
		

		}
		
		public void Redo()
		{
			if(oldComponent != null)
			{
                ((CustomControl)oldComponent).Cs.Remove();
                report.DestroyComponent(oldComponent);
			}
		}
	}
}

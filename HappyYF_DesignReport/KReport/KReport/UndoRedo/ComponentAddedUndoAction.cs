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
	
	public class ComponentAddedUndoAction : IUndoableOperation
	{
        DesignerReport report = null;
		Type componentType;
		object oldComponent;
		Control parentComponent;
		string componentName;
		int cx, cy;
		int width, heigh;

        public ComponentAddedUndoAction(DesignerReport report, ComponentEventArgs cea)
		{
			this.report     = report;
			this.componentType = cea.Component.GetType();
			this.oldComponent = cea.Component;

			if(oldComponent is Control)
			{ 
				Control con = oldComponent as Control;
				this.componentName = con.Name;

				cx = con.Location.X;
				cy = con.Location.Y;
				width = con.Width;
				heigh = con.Height;
				parentComponent = null;
			}
			else
			{
				//parentComponent = report.PrimarySelection;
			}
		}
		
		public void Undo()
		{
			if(oldComponent != null)
			{
				if(oldComponent is Control)
				{
                    ((CustomControl)oldComponent).Cs.Remove();
                    report.DestroyComponent(oldComponent as Control);
				}
			}
		}
		
		public void Redo()
		{

           
            if(report != null)
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
                //if(oldComponent is C1.Win.C1TrueDBGrid.C1TrueDBGrid)
                //{
                //    C1.Win.C1TrueDBGrid.C1TrueDBGrid c1 = oldComponent as C1.Win.C1TrueDBGrid.C1TrueDBGrid;

                //    report.AddC1TrueDBGrid(c1);

                //    return;
                //}

				object comp = report.CreateComponent(componentType, cx, cy, width, heigh);
				if(comp == null)
				{
					return;
				}

                //if(comp is C1.Win.C1TrueDBGrid.C1TrueDBGrid)
                //{
                //    Junxian.XReport.XmlControl.SetControlPropertyValueAddChildrenNoName(oldComponent, comp);
                //}
                //else
                //{
                //    Junxian.XReport.XmlControl.SetControlPropertyValueNoName(oldComponent, comp);
                //}

				if(comp is Control)
				{
					report.ParentControl.Controls.Add(comp as Control);
				}

				oldComponent = comp;

				if(oldComponent is Control)
				{ 
					Control con = oldComponent as Control;

					cx = con.Location.X;
					cy = con.Location.Y;
					width = con.Width;
					heigh = con.Height;
				}
			}
		}
	}
}

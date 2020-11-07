// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krger" email="mike@icsharpcode.net"/>
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
	
	public class ComponentChangedUndoAction : IUndoableOperation
	{
        DesignerReport report;
		object oldcomponent;
		string componentName = null;
		MemberDescriptor member;
		object oldValue = null;
		object newValue = null;

		string oldImagePath = null;
		string newImagePath = null;

		static string staticfieldName;
		static string staticobjName;
		static string staticXmlPropName;

		string fieldName;
		string objName;
		string XmlPropName;
		string PropName;

		bool   isCollection;
		bool   isComponentCollection;

        public ComponentChangedUndoAction(DesignerReport report, ComponentChangedEventArgs ea)
		{
			this.report            = report;
			this.oldcomponent = ea.Component;
			
			if(this.oldcomponent is Control)
			{
				this.componentName = ((Control)oldcomponent).Name;
			}

			this.member          = ea.Member;
			this.PropName = ea.Member.Name;

			if(ea.Component is PictureBox && PropName == "Image")
			{
				PictureBox pictureBox = ea.Component as PictureBox;
				if(pictureBox != null)
				{
					oldImagePath = report.GetPropertyValue(pictureBox.Name, "Image");
				}
			}

			fieldName = staticfieldName;
			objName = staticobjName;
			XmlPropName = staticXmlPropName;

			isCollection = ea.NewValue is IList;
			
			if (isCollection)
			{
				IList oldCol = (IList)ea.OldValue;
				IList newCol = (IList)ea.NewValue;
				object[] newArray = new object[newCol.Count];
				isComponentCollection = false;
				if (newCol.Count > 0)
				{
					isComponentCollection = newCol[0] is IComponent;
				}
				
				if (oldCol != null)
				{
					object[] oldArray = new object[oldCol.Count];
					if (isComponentCollection)
					{
						int idx = 0;
						foreach (IComponent cmp in oldCol)
						{
							oldArray[idx++] = cmp.Site.Name;
						}
					}
					else
					{
						oldCol.CopyTo(oldArray, 0);
					}
					this.oldValue = oldArray;
				}

				if (isComponentCollection)
				{
					int idx = 0;
					foreach (IComponent cmp in newCol)
					{
						newArray[idx++] = cmp.Site.Name;
					}
				}
				else
				{
					newCol.CopyTo(newArray, 0);
				}
				
				this.newValue = newArray;
			}
			else
			{
				this.oldValue        = ea.OldValue;
				this.newValue        = ea.NewValue;
			}
		}
	
		public void Undo()
		{	
			//			if(oldValue == null) return;

			if(oldcomponent == null)
			{
				return;
			}

			object component = null;
			if(oldcomponent is Control && componentName != null)
			{
				component = report.GetControl(oldcomponent as Control, componentName);
               // report.DestroyComponent(oldcomponent as Control);
                //component = oldcomponent;
			}

			if(component == null)
			{
				component = oldcomponent;
			}
			
			report.OnComponentChanging(component, member);
			if(component is PictureBox && PropName == "Image" && oldImagePath != null)
			{
				PictureBox pictureBox = component as PictureBox;
				newImagePath = report.GetPropertyValue(pictureBox.Name, "Image");

				SetPictureBoxImage(component as PictureBox, oldImagePath);
				//report.Edit_Info("Name", pictureBox.Name, "Image", oldImagePath);
				return;
			}

			Type t = component.GetType();
			PropertyInfo pInfo = t.GetProperty(member.Name);
			try
			{
				pInfo.SetValue(component, oldValue, null);
                //control.Location = new Point(control.Location.X, base2.Top + control.垂直位置);
                //component.
                if (component is CustomControl)
                {
                    ((CustomControl)component).Cs.Remove();
                    BandBase base2 = report.FindBandPosY(((CustomControl)component).Location.Y);
                    ((CustomControl)component).Band = base2;
                    ((CustomControl)component).垂直位置 = ((CustomControl)component).Location.Y - ((CustomControl)component).Band.Top;
                }

                if (component is BandDesigner)
                {
                    report.CurrentBandControl = (BandDesigner)component;
                    report.CurrentBand = (BandBase)((BandDesigner)component).Tag;

                    report.ArrangeAreaDesigner();
                    report.SetRulers();
                    report.ArrangeBands();
                    report.BandControlPaint(this, null);
                }
                

				WriteXmlProperty(component, oldValue);
			}
			catch{}
			report.OnComponentChanged(component, member, newValue, oldValue);
		}
		
		public void Redo()
		{	
			//			if(newValue == null) return;

			if(oldcomponent == null)
			{
				return;
			}

			object component = null;
			if(oldcomponent is Control && componentName != null)
			{
				component = report.GetControl(oldcomponent as Control, componentName);
			}

			if(component == null)
			{
				component = oldcomponent;
			}

			report.OnComponentChanging(component, member);
			if(component is PictureBox && PropName == "Image" && newImagePath != null)
			{
				PictureBox pictureBox = component as PictureBox;

				SetPictureBoxImage(component as PictureBox, newImagePath);
				//report.Edit_Info("Name", pictureBox.Name, "Image", newImagePath);
				return;
			}

			Type t = component.GetType();
			try
			{
				t.InvokeMember(member.Name,
					BindingFlags.Public           |
					BindingFlags.NonPublic        |
					BindingFlags.Instance         |
					BindingFlags.FlattenHierarchy |
					BindingFlags.SetProperty,
					null, 
					component, 
					new object[] { newValue });
                if (component is CustomControl)
                {
                    ((CustomControl)component).Cs.Remove();
                    BandBase base2 = report.FindBandPosY(((CustomControl)component).Location.Y);
                    ((CustomControl)component).Band = base2;
                    ((CustomControl)component).垂直位置 = ((CustomControl)component).Location.Y - ((CustomControl)component).Band.Top;
                }

				WriteXmlProperty(component, newValue);
			}
			catch{}
			report.OnComponentChanged(component, member, oldValue, newValue);
		}

		private void WriteXmlProperty(object component, object objValue)
		{
			if(objValue == null) return;

			//string propValue = Junxian.XmlComponent.Convert.ConvertToString(objValue);
			string FieldName = fieldName;
			string objname = objName;
			string xmlPropName;


			if(XmlPropName == null)
			{
				xmlPropName = PropName;
			}
			else
			{
				xmlPropName = XmlPropName;
			}

			if(component is Control)
			{
				Control con = component as Control;

				if(FieldName == null)
				{
					FieldName = "Name";
				}

				if(objname == null)
				{
					objname = con.Name;
				}

				if(XmlPropName == null)
				{
					xmlPropName = PropName;
				}
			}

			//report.Edit_Info(FieldName, objname, xmlPropName, propValue);
		}

		private void SetPictureBoxImage(PictureBox pictureBox, string strValue)
		{
			string strPath = "";
			if(strValue.IndexOf("\\") != -1)
			{
				strPath = strValue;
			}
			else
			{
				strPath = Application.StartupPath + "\\Image\\" + strValue;
			}

			if(!File.Exists(strPath))
			{
				MessageBox.Show("文件" + strPath + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			Bitmap tempImage = new Bitmap(strPath);
			if(tempImage != null)
			{
				pictureBox.Image = tempImage;
			}
		}

		public static string XmlPropertyName
		{
			get
			{
				return staticXmlPropName;
			}
			set
			{
				staticXmlPropName = value;
			}
		}

		public static string FieldName
		{
			get
			{
				return staticfieldName;
			}
			set
			{
				staticfieldName = value;
			}
		}

		public static string ObjectName
		{
			get
			{
				return staticobjName;
			}
			set
			{
				staticobjName = value;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AfpComponents
{
	//[ProvideProperty("AfpDraw", typeof(Control))]
	[ProvideProperty("AfpLabel", typeof(Control))]
	//[ProvideProperty("AfpCaption", typeof(Control))]
	[ToolboxBitmap(typeof(Label))]
	public partial class AfpLabelProvider : Component, IExtenderProvider
	{
		#region contructorz and protectorz
		protected class AfpLabelInfo
		{
			// label that will be drawn next to control
			protected Label _lab = new Label();
			// hash code of control so we know which labels goes to wchich control
			protected int _controlHash;
			public Label lab
			{
				get { return _lab; }
				set { _lab = value; }
			}
			public int controlHash
			{
				get { return _controlHash; }
				set { _controlHash = value; }
			}
			// we will take hashcode of each control
			public AfpLabelInfo(int AcontrolName)
			{
				_controlHash = AcontrolName;
			}
		}

		protected Hashtable AfpLabelProps = new Hashtable();
		protected List<AfpLabelInfo> _labels = new List<AfpLabelInfo>();
		protected Control activeControl;
		public AfpLabelProvider()
		{
			InitializeComponent();

		} 
		#endregion
		#region Label mapper methods

		protected int getIndexFromLabels(Control cnt)
		{
			for (int i = 0; i < _labels.Count; i++)
			{
				if (_labels[i].controlHash == (cnt.GetHashCode()) )
					return i;
			}
			return -1;
		}
		protected void RemoveControlFromLabels(Control cnt)
		{
			int i = getIndexFromLabels(cnt);
			if (i != -1)
			{
				_labels[i].lab.Dispose();
				_labels.RemoveAt(i);
			}
		}
		protected void AddOrRenewLabel(Control cnt)
		{
			int dummy = cnt.GetHashCode();
			for (int i = 0; i < _labels.Count; i++)
			{
				if (_labels[i].controlHash == (dummy))
				{
					if (_labels[i].lab == null)
					{
						_labels[i].lab = new Label();
						return;
					}
				}
			}
			
			_labels.Add(new AfpLabelInfo(dummy)); // if there was no control we add it manually
		}

		#endregion

		#region Getters and setterrs

		public string GetAfpLabel(Control c)
		{
			string text = (string)AfpLabelProps[c];
			if (text == null)
			{
				text = String.Empty;
			}
			return text;
		}
		public void SetAfpLabel(Control c, string value)
		{
			activeControl = c;
			AfpLabelProps[c] = value;
			if (value == String.Empty)
			{
				c.Disposed -= new EventHandler(c_Disposed);
				c.ParentChanged -= new EventHandler(val_ParentChanged);
				c.LocationChanged -= new EventHandler(val_LocationChanged);	
			}
			else
			{
				c.ParentChanged += new EventHandler(val_ParentChanged);
				c.LocationChanged += new EventHandler(val_LocationChanged);	
				c.Disposed += new EventHandler(c_Disposed);

				val_ParentChanged(activeControl, null);
			}
		}
		#endregion
		void c_Disposed(object sender, EventArgs e)
		{
			activeControl = (Control)sender;
			if (_labels[getIndexFromLabels(activeControl)].lab != null)
			{	
				RemoveControlFromLabels(activeControl);
			}
		}

		void val_LocationChanged(object sender, EventArgs e)
		{
			int i = getIndexFromLabels(activeControl);
			activeControl = (Control)sender;
			if (i != -1)
				setControlsPosition(_labels[getIndexFromLabels(activeControl)].lab);
		}

		void val_ParentChanged(object sender, EventArgs e)
		{
			activeControl = (Control)sender;
			AddOrRenewLabel(activeControl);
			if (activeControl.Parent != null)
			{
				activeControl.Parent.Controls.Add(_labels[getIndexFromLabels(activeControl)].lab);
				setControlsPosition(_labels[getIndexFromLabels(activeControl)].lab);
			}
			
		}
		protected virtual void setControlsPosition(Label someLab)
		{
			if (someLab != null)
			{
				// setting text
				someLab.Text = GetAfpLabel(activeControl);
				// autosize is important cause it saves us a lot of code
				someLab.AutoSize = true;
				// little bit to the right
				someLab.Left = activeControl.Left - someLab.Width - 5;
				// and little bit below top 
				someLab.Top = activeControl.Top + 3;
			}
		}
		


		#region IExtenderProvider Members

		public bool CanExtend(object extendee)
		{
			if (extendee is Control)
				return true;
			else
				return false;
		}

		#endregion

		


	}
}

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
	public class UndoHandler
	{
        DesignerReport report;
		
		UndoStack undoStack = new UndoStack();

		bool IsSelectComponents = true;

		bool  inUndoRedo     = false;
		int   transactionLevel = 0;
		int   undoOperations   = 0;
		
		public bool EnableUndo
		{
			get
			{
				return undoStack.CanUndo;
			}
		}

		public bool EnableRedo
		{
			get
			{
				return undoStack.CanRedo;
			}
		}
		
		public void Reset()
		{
			undoStack.ClearAll();
		}
		
		void ComponentChanged(object sender, ComponentChangedEventArgs ea)
		{
			if(!IsSelectComponents)
			{
				return;
			}

			if(inUndoRedo)
			{
				return;
			}
			if(ea.Component == null)
			{
				return;
			}
            //if (((String)(ea.NewValue)) == "reportBands")
            //{
            //    ++undoOperations;
            //    undoStack.Push(new BandsChangedUndoAction(report, (ArrayList )ea.OldValue ));
            //}
            //else
            //{
                ++undoOperations;
                undoStack.Push(new ComponentChangedUndoAction(report, ea));
            //}
		}
        void BandsChanged(object sender, ComponentChangedEventArgs ea)
        {
            if (!IsSelectComponents)
            {
                return;
            }

            if (inUndoRedo)
            {
                return;
            }
            if (ea.Component == null)
            {
                return;
            }
            //if (((String)(ea.NewValue)) == "reportBands")
            //{
                ++undoOperations;
                undoStack.Push(new BandsChangedUndoAction(report, (ArrayList)ea.OldValue));
           // }
           
        }
		
		void ComponentAdded(object sender, ComponentEventArgs ea)
		{
			if(!IsSelectComponents)
			{
				return;
			}

			if (inUndoRedo)
			{
				return;
			}
			++undoOperations;
			ComponentAddedUndoAction caua = new ComponentAddedUndoAction(report, ea);
			undoStack.Push(caua);
		}
		
		void ComponentRemoved(object sender, ComponentEventArgs ea)
		{
			if(!IsSelectComponents)
			{
				return;
			}

			if (inUndoRedo)
			{
				return;
			}
			++undoOperations;
			undoStack.Push(new ComponentRemovedUndoAction(report, ea));
		}

		SelectComponentsUndoAction selectComponentsUndoAction = null;
		
		void TransactionOpened(object sender, EventArgs e)
		{
			IsSelectComponents = true;
			if(!report.TransactionScription.StartsWith("创建") && report.TransactionScription != "一方报表" && report.GetSelectedComponents().Count == 0)
			{
				IsSelectComponents = false;
				return;
			}

			if (transactionLevel == 0)
			{
				undoOperations = 0;
				selectComponentsUndoAction = new SelectComponentsUndoAction(report, GetSelectedComponentNames(report));
				undoStack.Push(selectComponentsUndoAction);
			}
			++transactionLevel;
		}
		
		void TransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if(!IsSelectComponents)
			{
				return;
			}

			--transactionLevel;
			if (transactionLevel == 0 && undoOperations > 0)
			{
				if (selectComponentsUndoAction != null)
				{
					selectComponentsUndoAction.SetNewSelection(GetSelectedComponentNames(report));
					selectComponentsUndoAction = null;
				}
				undoStack.UndoLast(undoOperations + 1);
			}
		}

        public static ArrayList GetSelectedComponentNames(DesignerReport report)
		{
			ArrayList names = new ArrayList();
			
			foreach(Control component in report.GetSelectedComponents())
			{
				names.Add(component);
			}
			return names;
		}

        public static void SetSelectedComponentsPerName(DesignerReport report, ArrayList names)
		{
			if(names == null)
			{
				return;
			}

			ArrayList components = new ArrayList();
			
			foreach(Control component in names)
			{
				components.Add(component);
			}
			
			report.SetSelectedComponents(components);
		}

        public static void SetBandsLocation(DesignerReport report, ArrayList names)
        {
            if (names == null)
            {
                return;
            }

            ArrayList components = new ArrayList();

            foreach (object  component in names)
            {
                components.Add(component);
            }

            report.SetBandsLocation(components);
        }


        public void Attach(DesignerReport report)
		{
			this.report = report;

			AddComponentChangeEvents();

			//			IComponentChangeService report = (IComponentChangeService)((Report)report).GetService(typeof(IComponentChangeService));
			//			report.ComponentChanged += new ComponentChangedEventHandler(ComponentChanged);
			//			report.ComponentAdded   += new ComponentEventHandler(ComponentAdded);
			//			report.ComponentRemoved += new ComponentEventHandler(ComponentRemoved);
			
			report.TransactionOpened += new EventHandler(TransactionOpened);
			report.TransactionClosed += new DesignerTransactionCloseEventHandler(TransactionClosed);
		}
		
		public void Detach()
		{
			RemoveComponentChangeEvents();

			//			IComponentChangeService report = (IComponentChangeService)((Report)report).GetService(typeof(IComponentChangeService));
			//			report.ComponentChanged -= new ComponentChangedEventHandler(ComponentChanged);
			//			report.ComponentAdded   -= new ComponentEventHandler(ComponentAdded);
			//			report.ComponentRemoved -= new ComponentEventHandler(ComponentRemoved);
			
			report.TransactionOpened -= new EventHandler(TransactionOpened);
			report.TransactionClosed -= new DesignerTransactionCloseEventHandler(TransactionClosed);
		}
		
		private void AddComponentChangeEvents()
		{
			report.ComponentChanged += new ComponentChangedEventHandler(ComponentChanged);
			report.ComponentAdded   += new ComponentEventHandler(ComponentAdded);
			report.ComponentRemoved += new ComponentEventHandler(ComponentRemoved);
            report.BandsChanged += new ComponentChangedEventHandler(BandsChanged);
          
		}

		private void RemoveComponentChangeEvents()
		{
			report.ComponentChanged -= new ComponentChangedEventHandler(ComponentChanged);
			report.ComponentAdded   -= new ComponentEventHandler(ComponentAdded);
			report.ComponentRemoved -= new ComponentEventHandler(ComponentRemoved);
            report.BandsChanged -= new ComponentChangedEventHandler(BandsChanged);
		}

		public void Undo()
		{
			RemoveComponentChangeEvents();

			inUndoRedo = true;
			try 
			{
				undoStack.Undo();
				UpdateSelectableObjects();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			finally
			{
				inUndoRedo = false;
			}

			AddComponentChangeEvents();
		}
		
		public void Redo()
		{
			RemoveComponentChangeEvents();

			inUndoRedo = true;
			try
			{
				undoStack.Redo();
				UpdateSelectableObjects();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			finally 
			{
				inUndoRedo = false;
			}

			AddComponentChangeEvents();
		}
		
		protected void UpdateSelectableObjects()
		{
			if (report != null)
			{
				report.SetSelectedComponents();
			}
		}
	}
}

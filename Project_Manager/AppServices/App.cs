using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Project_Manager.AppServices
{
   
    public class App 
    {
       

        
        #region Singleton

        private static App instance = new App();

        public static App Instance
        {
            get
            {
                return instance;
            }
        }

        private App()
        {
           

            extensions = new List<IExtension>();

            extensions.Add(new CompanySetExtension());
            extensions.Add(new IndividualSetExtension());
           
            
        }

        #endregion

        #region Fields
        
        private List<IExtension> extensions;
       
        private bool disposed = false;

        #endregion

        #region Properties

       

     

        public List<IExtension> Extensions
        {
            get
            {
                return extensions;
            }
        } 

        #endregion

        #region Methods

        public IExtension GetExtensionByType(Type type)
        {
            for (int i = 0; i < this.extensions.Count; i++)
            {
                if (this.extensions[i].GetType() == type)
                {
                    return this.extensions[i];
                }
            }

            return null;
        }

       

        public void InitExtensions()
        {
            for (int i = 0; i < Extensions.Count; i++)
            {
                if (Extensions[i] is IInitializable)
                {
                    ((IInitializable)Extensions[i]).Init();                   
                }
            }

        }
        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                for (int i = 0; i < Extensions.Count; i++)
                {
                    if (Extensions[i] is IDisposable)
                    {
                        try
                        {
                            ((IDisposable)Extensions[i]).Dispose();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                    }
                }
            }
        }

      

        #endregion
    }
}

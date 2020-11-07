using System;
using System.Collections.Generic;
using System.Text;


namespace Project_Manager.AppServices
{
    public interface IExtension
    {
        string Name { get; }

        IUIExtension UIExtension { get; }
    }
}

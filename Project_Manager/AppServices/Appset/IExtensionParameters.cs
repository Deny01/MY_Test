using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Project_Manager.AppServices
{
    public interface IExtensionParameters
    {
        event PropertyChangedEventHandler ParameterChanged;
    }
}

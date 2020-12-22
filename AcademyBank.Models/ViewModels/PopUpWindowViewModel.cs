using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models
{
    public class PopUpWindowViewModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MainAction { get; set; }
        public int Id { get; set; }

        public PopUpWindowViewModel(string controllerName,string actionName,string mainActionName,int id)
        {
            ControllerName = controllerName;
            ActionName = actionName;
            MainAction = mainActionName;
            Id = id;
        }
        public PopUpWindowViewModel()
        {
                
        }
    }
}

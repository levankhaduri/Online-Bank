using AcademyBank.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Web.Controllers
{
    public class BaseController : Controller
    {
        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "<script language='javascript'>swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "</script>";
            TempData["notification"] = msg;
        }
    }
}

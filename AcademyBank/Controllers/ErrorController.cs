using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyBank.DAL.filter;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using AcademyBank.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcademyBank.Controllers
{
    public class ErrorController : BaseController
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var ErrorVm = new ErrorViewModel();
            switch (statusCode)
            {
                case 404:
                    ErrorVm.ErrorCode = "404";
                    ErrorVm.Description = "Sorry, Page not found";
                    break;
                case 401:
                    ErrorVm.ErrorCode = "401";
                    ErrorVm.Description = "Unauthorized guests can not access this page";
                    break;
                case 500:
                    ErrorVm.ErrorCode = "500";
                    ErrorVm.Description = "sorry, this page is not working";
                    break;
                default:
                    return View("ExceptionPage");
            }
            return View("StatusError", ErrorVm);           
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View("ExceptionPage");
        }
    }
}
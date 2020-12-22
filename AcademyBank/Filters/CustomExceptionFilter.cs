using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using AcademyBank.Models;
using Microsoft.Extensions.Logging;

namespace AcademyBank.DAL.filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(
            IWebHostEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider,
            ILogger<CustomExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"{context.Exception.Message} | {context.RouteData.Values["controller"].ToString()} | {context.RouteData.Values["action"].ToString()}");
            Microsoft.AspNetCore.Mvc.ViewResult result;
            if (!_hostingEnvironment.IsDevelopment())
            {
                result = new Microsoft.AspNetCore.Mvc.ViewResult { ViewName = "ExceptionPage" };
                context.Result = result;
            }
            else
            {
                var ErrorViewModel = new ErrorViewModel();
                result = new Microsoft.AspNetCore.Mvc.ViewResult { ViewName = "CustomExceptionPage" };

                result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
                                                        context.ModelState);
                ErrorViewModel.ErrorCode = context.HttpContext.ToString();
                ErrorViewModel.Description = context.Exception.ToString();
                result.ViewData.Add("Exception", context.Exception);

                context.Result = result;
            }
        }
    }
}
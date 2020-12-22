using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.Web.Filters
{
    public class InformationLogFilter : ActionFilterAttribute, IActionFilter
    {
        private readonly ILogger<InformationLogFilter> _logger;
        private string _actionParamsValues;
        private string _actionSensitiveParamsValues;
        private Stopwatch _watch = new Stopwatch();

        public InformationLogFilter(ILogger<InformationLogFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _actionParamsValues = ParamsValues(context);
            _actionSensitiveParamsValues = ParamsSensitiveValues(context);

            var actionParams = context.Filters.OfType<SensitiveInfoLogFilter>().Any()
            ? _actionSensitiveParamsValues
            : _actionParamsValues;

             _logger.LogInformation($"'{context.RouteData.Values["action"].ToString()}' from controller: '{context.RouteData.Values["controller"].ToString()}' start Executing with parameters and its values: [{actionParams}]");
            _watch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _watch.Stop();
            string timeElapsed = ElapsedTimeFormat(_watch);

            var actionParams = context.Filters.OfType<SensitiveInfoLogFilter>().Any()
            ? _actionSensitiveParamsValues
            : _actionParamsValues;

             _logger.LogInformation($"'{context.RouteData.Values["action"].ToString()}' from controller: '{context.RouteData.Values["controller"].ToString()}' finish executing with parameters and its values: [{actionParams}] | Time Elapsed: {timeElapsed}");
        }

        public string ParamsValues(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments.Keys;
            StringBuilder paramsAndValues = new StringBuilder();
            foreach (var key in parameters)
            {
                paramsAndValues.AppendJoin(": ", key , context.ActionArguments[key]+ ", ");
            }
            return paramsAndValues.ToString();
        }

        public string ParamsSensitiveValues(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments.Keys;
            StringBuilder paramsAndValues = new StringBuilder();
            foreach (var key in parameters)
            {
                string value = "*************";
                if (key== "userEmail" || key == "email" || key == "input")
                {
                    value = context.ActionArguments[key].ToString();
                }
                paramsAndValues.AppendJoin(": ", key, value + ", ");
            }
            return paramsAndValues.ToString();
        }

        private string ElapsedTimeFormat(Stopwatch watch)
        {
            TimeSpan ExecTime = watch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ExecTime.Hours, ExecTime.Minutes, ExecTime.Seconds,
                ExecTime.Milliseconds / 10);

            return elapsedTime;
        }
    }
}

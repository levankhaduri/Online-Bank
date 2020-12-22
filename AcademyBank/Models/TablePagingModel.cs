using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Web.Models
{
	public class TablePagingModel
	{

		public string Action { get; set; }
		public string Controller { get; set; }
		public string UrlParamOrdervalue { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }

	}
}

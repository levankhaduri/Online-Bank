using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Web.Models
{
	public class TablePartialModel
	{
		public string UrlParamOrdervalue { get; set; }
		public string Order { get; set; }
		public string OrderDesc { get; set; }
		public string DisplayName { get; set; }
		public string Action { get; set; }
		public string Controller { get; set; }
		public int CurrentPage { get; set; }

		public string SelectedCardStatus { get; set; }
	}
}

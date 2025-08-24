using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Infrastructure.MeiliSearch
{
	public class MeiliSearchModel
	{
		public string Url { get; set; } = string.Empty;
		public string MasterKey { get; set; } = string.Empty;
		public string IndexName { get; set; } = string.Empty;
	}
}

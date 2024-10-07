using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class SearchQuery
	{
		[Key]
		public Guid Id { get; set; }
		public string Query { get; set; }
		public string Response { get; set; }
	}
}

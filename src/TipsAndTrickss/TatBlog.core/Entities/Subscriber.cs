using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Contracts;

namespace TatBlog.core.Entities
{
	public class Subscriber : IEntity
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public bool Status { get; set; }
		public bool Flag { get; set; }
		public DateTime SignUpDate { get; set; }
		public DateTime UnSignUpDate { get; set; }
		public string Reason { get; set; }
		public string Notes { get; set; }
	}
}

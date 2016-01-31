using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySQL_vs_SQLServer.EF_DAL
{
	[Table("Department")]
	public class Department
	{
		[Key]
		public int id { get; set; }
		public string name { get; set; }
	}
}

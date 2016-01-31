using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySQL_vs_SQLServer.EF_DAL
{
	[Table("Person")]
	public class Person
	{
		[Key]
		public int id { get; set; }
		public string first { get; set; }
		public string last { get; set; }
		public int department { get; set; }
	}
}

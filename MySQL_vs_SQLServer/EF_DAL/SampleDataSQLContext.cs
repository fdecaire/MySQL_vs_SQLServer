using System;
using System.Data.Entity;

namespace MySQL_vs_SQLServer.EF_DAL
{
	public class SampleDataSQLContext : DbContext, IDisposable
	{
		public SampleDataSQLContext()
			: base(@"Data Source=localhost;Initial Catalog=sampledata;Integrated Security=True;MultipleActiveResultSets=True") // replace with connecton string to local SQL server
		{

		}

		public DbSet<Department> Departments { get; set; }
		public DbSet<Person> Persons { get; set; }
	}
}

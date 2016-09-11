using System;
using System.Data.Entity;
using MySql.Data.Entity;

namespace MySQL_vs_SQLServer.EF_DAL
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class SampleDataMySQLContext : DbContext, IDisposable
	{
		public SampleDataMySQLContext() : base(@"server=localhost;database=sampledata;uid=root;password=mysqlpassword") // change mysqlpassword to the password of root in mySQL
		{

		}

		public DbSet<Department> Departments { get; set; }
		public DbSet<Person> Persons { get; set; }
	}
}

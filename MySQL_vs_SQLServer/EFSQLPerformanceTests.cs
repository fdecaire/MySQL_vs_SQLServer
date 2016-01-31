using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQL_vs_SQLServer.EF_DAL;
using System.IO;

namespace MySQL_vs_SQLServer
{
	public class EFSQLPerformanceTests
	{
		private int DepartmentKey;

		public EFSQLPerformanceTests()
		{
			File.Delete("c:\\sqlserverresults.txt");

			// clean up any data from previous runs
			using (var db = new SampleDataSQLContext())
			{
				// delete any records from previous run
				var personQuery = (from pers in db.Persons select pers).ToList();
				db.Persons.RemoveRange(personQuery);
				db.SaveChanges();

				var deptQuery = (from dept in db.Departments select dept).ToList();
				db.Departments.RemoveRange(deptQuery);
				db.SaveChanges();

				// insert one department
				Department myDepartment = new Department()
				{
					name = "Operations"
				};

				db.Departments.Add(myDepartment);
				db.SaveChanges();

				// select the primary key of the department table for the only record that exists
				DepartmentKey = (from d in db.Departments where d.name == "Operations" select d.id).FirstOrDefault();
			}
		}

		public void RunAllTests()
		{
			double smallest = -1;
			for (int i = 0; i < 5; i++)
			{
				double result = TestInsert();

				if (smallest == -1)
				{
					smallest = result;
				}
				else
				{
					if (result < smallest)
					{
						result = smallest;
					}
				}
			}
			WriteLine("INSERT:" + smallest.ToString());
			Console.WriteLine("INSERT:" + smallest.ToString());

			smallest = -1;
			for (int i = 0; i < 5; i++)
			{
				double result = TestUpdate();

				if (smallest == -1)
				{
					smallest = result;
				}
				else
				{
					if (result < smallest)
					{
						result = smallest;
					}
				}
			}
			WriteLine("UPDATE:" + smallest.ToString());
			Console.WriteLine("UPDATE:" + smallest.ToString());

			smallest = -1;
			for (int i = 0; i < 5; i++)
			{
				double result = TestSelect();

				if (smallest == -1)
				{
					smallest = result;
				}
				else
				{
					if (result < smallest)
					{
						result = smallest;
					}
				}
			}
			WriteLine("SELECT:" + smallest.ToString());
			Console.WriteLine("SELECT:" + smallest.ToString());

			smallest = -1;
			for (int i = 0; i < 5; i++)
			{
				double result = TestDelete();

				if (smallest == -1)
				{
					smallest = result;
				}
				else
				{
					if (result < smallest)
					{
						result = smallest;
					}
				}
			}
			WriteLine("DELETE:" + smallest.ToString());
			Console.WriteLine("DELETE:" + smallest.ToString());

			WriteLine("");
		}

		public double TestInsert()
		{
			using (var db = new SampleDataSQLContext())
			{
				// read first and last names
				List<string> firstnames = new List<string>();
				using (StreamReader sr = new StreamReader(@"..\..\Data\firstnames.txt"))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
						firstnames.Add(line);
				}

				List<string> lastnames = new List<string>();
				using (StreamReader sr = new StreamReader(@"..\..\Data\lastnames.txt"))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
						lastnames.Add(line);
				}

				db.Configuration.AutoDetectChangesEnabled = false;
				db.Configuration.ValidateOnSaveEnabled = false;

				//test inserting 1000 records
				DateTime startTime = DateTime.Now;
				for (int j = 0; j < 10; j++)
				{
					for (int i = 0; i < 1000; i++)
					{
						Person personRecord = new Person()
						{
							first = firstnames[i],
							last = lastnames[i],
							department = DepartmentKey
						};

						db.Persons.Add(personRecord);
					}
				}

				db.SaveChanges();
				TimeSpan elapsedTime = DateTime.Now - startTime;

				return elapsedTime.TotalSeconds;
			}
		}

		public double TestSelect()
		{
			using (var db = new SampleDataSQLContext())
			{
				db.Configuration.AutoDetectChangesEnabled = false;

				DateTime startTime = DateTime.Now;
				for (int i = 0; i < 1000; i++)
				{
					var query = (from p in db.Persons
								 join d in db.Departments on p.department equals d.id
								 select p).ToList();
				}
				TimeSpan elapsedTime = DateTime.Now - startTime;

				return elapsedTime.TotalSeconds;
			}
		}

		public double TestUpdate()
		{
			using (var db = new SampleDataSQLContext())
			{
				DateTime startTime = DateTime.Now;
				var query = (from p in db.Persons select p).ToList();
				foreach (var item in query)
				{
					item.last = item.last + "2";
				}
				db.SaveChanges();

				TimeSpan elapsedTime = DateTime.Now - startTime;

				return elapsedTime.TotalSeconds;
			}
		}

		public double TestDelete()
		{
			using (var db = new SampleDataSQLContext())
			{
				DateTime startTime = DateTime.Now;
				var personQuery = (from pers in db.Persons select pers).ToList();

				db.Persons.RemoveRange(personQuery);
				db.SaveChanges();
				TimeSpan elapsedTime = DateTime.Now - startTime;

				return elapsedTime.TotalSeconds;
			}
		}

		public void WriteLine(string text)
		{
			using (StreamWriter writer = new StreamWriter("c:\\sqlserverresults.txt",true))
			{
				writer.WriteLine(text);
			}
		}
	}
}

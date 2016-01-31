using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQL_vs_SQLServer
{
	class Program
	{
		static void Main(string[] args)
		{
			
			var EFSQLTests = new EFSQLPerformanceTests();
			EFSQLTests.RunAllTests();
			/*
			var EFMySQLTests = new EFMySQLPerformanceTests();
			EFMySQLTests.RunAllTests();
			*/
		}
	}
}

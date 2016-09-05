namespace MySQL_vs_SQLServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var efsqlTests = new EFSQLPerformanceTests();
			efsqlTests.RunAllTests();
			/*
			var EFMySQLTests = new EFMySQLPerformanceTests();
			EFMySQLTests.RunAllTests();
			*/
		}
	}
}

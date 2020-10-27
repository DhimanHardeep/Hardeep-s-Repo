namespace DataRepository
{
    public static class Connection
    {
        public static string GetSqlConnection()
        {

            string connection = @"server=.;Database=StudentDB;Integrated Security=True;";
           
            return connection;

        }

    }
}


using System.Data.SQLite;
using System.Data.SQLite.Extensions;


namespace SQLiteExample
{
    /// <summary>
    /// Simple example to illustrate how to use SQLiteBulkCopy
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using (var srcConn = new SQLiteConnection(@"Data Source=source.sqlite;journal_mode=WAL;page_size=4096;cache_size=10000;locking_mode=EXCLUSIVE;synchronous=NORMAL;Version=3;cache=shared;"))
            {
                srcConn.Open();
                var cmd = srcConn.CreateCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS SourceTable(A int, B text) ";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "BEGIN;INSERT INTO SourceTable(A,B) values(1,'Jane');INSERT INTO SourceTable(A,B) values(2,'John');COMMIT;";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT * from SourceTable";

                using (var reader = cmd.ExecuteReader())
                {
                    using (var destConn = new SQLiteConnection(@"Data Source=target.sqlite;journal_mode=WAL;page_size=4096;cache_size=10000;locking_mode=EXCLUSIVE;synchronous=NORMAL;Version=3;cache=shared;"))
                    {
                        destConn.Open();
                        var cmdDest = destConn.CreateCommand();
                        cmdDest.CommandTimeout = 0;
                        cmdDest.CommandText = "CREATE TABLE  IF NOT EXISTS DestTable(A int, B text)";
                        cmdDest.ExecuteNonQuery();
                        using (var copy = new SQLiteBulkCopy(destConn))
                        {
                            copy.DestinationTableName = "DestTable";
                            copy.WriteToServer(reader);
                        }
                    }

                }
            }
        }
    }
}

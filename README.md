# SQLiteBulkCopy
Implement SQLiteBulkCopy similar to SQLBulkCopy.
It is based on https://github.com/aspnet/Microsoft.Data.Sqlite/issues/289 with additional support for binary data

# Usage
    using (var reader = cmd.ExecuteReader())
    {
        using (var destConn = new SQLiteConnection(...))
        {
            destConn.Open();
            ...
            using (var copy = new SQLiteBulkCopy(destConn))
            {
                copy.DestinationTableName = "DestTable";
                copy.WriteToServer(reader);
            }
        }
    }

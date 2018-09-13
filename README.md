# SQLiteBulkCopy
Implement SQLiteBulkCopy similar to SQLBulkCopy

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

using System;

namespace AzureTableStoragePocoConverter
{
    public static class TableEntityConvertSettings
    {
        public static Type RowKeyAttribute { get; set; }

        public static Type PartitionKeyAttribute { get; set; }

        public static Type ETagAttribute { get; set; }

        public static Type TimestampAttribute { get; set; }

        public static Type IgnorePropertyAttribute { get; set; }
    }
}

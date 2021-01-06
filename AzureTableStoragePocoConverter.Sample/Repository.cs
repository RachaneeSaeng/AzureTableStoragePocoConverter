using AzureTableStoragePocoConverter.Attributes;
using AzureTableStoragePocoConverter.Sample.Pocos;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace AzureTableStoragePocoConverter.Sample
{
    class Repository
    {
        private CloudTable _cloudTable;

        public Repository(CloudTable cloudTable)
        {
            _cloudTable = cloudTable;
            SetupConverter();
        }

        public async Task<Customer> GetCustomer(string lastName, string firstName)
        {
            // Load the DynamicTableEntity object from Storage using the keys
            var operation = TableOperation.Retrieve(lastName, firstName);
            var result = await _cloudTable.ExecuteAsync(operation);

            // Convert into the POCO using TableEntityConvert.FromTableEntity<T>()
            var customer = TableEntityConvert.FromTableEntity<Customer>(result.Result);

            return customer;
        }

        public async Task AddOrUpdateCustomer(Customer customer)
        {
            // Convert POCO to ITableEntity object using TableEntityConvert.ToTableEntity()
            var tableEntity = TableEntityConvert.ToTableEntity(customer);

            // Save the new or updated entity in the Storage
            var operation = TableOperation.InsertOrReplace(tableEntity);
            await _cloudTable.ExecuteAsync(operation);
        }

        private void SetupConverter()
        {
            TableEntityConvertSettings.RowKeyAttribute = typeof(RowKeyAttribute);
            TableEntityConvertSettings.PartitionKeyAttribute = typeof(PartitionKeyAttribute);
            TableEntityConvertSettings.ETagAttribute = typeof(ETagAttribute);
            TableEntityConvertSettings.TimestampAttribute = typeof(TimestampAttribute);
            TableEntityConvertSettings.IgnorePropertyAttribute = typeof(IgnorePropertyAttribute);
        }
    }
}

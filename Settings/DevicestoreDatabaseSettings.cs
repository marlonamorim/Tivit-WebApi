namespace Tivit.WebApi.Settings
{
    public class DevicestoreDatabaseSettings : IDevicestoreDatabaseSettings
    {
        public string DeviceCollectionName { get; set; }
        public string DataOfDeviceCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDevicestoreDatabaseSettings
    {
        string DeviceCollectionName { get; set; }
        string DataOfDeviceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
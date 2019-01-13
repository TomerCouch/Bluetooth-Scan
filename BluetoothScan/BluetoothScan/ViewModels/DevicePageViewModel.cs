using BluetoothScan.Models;
using BluetoothScan.ViewModels;
using Prism.Navigation;

namespace BluetoothScan.ViewModels
{
    public class DevicePageViewModel : ViewModelBase
    {
        private BTDeviceInfo _device;
        public BTDeviceInfo Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }
        
        public DevicePageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            Title = "Device Details Page";
        }

        // It grabs the device data from the previous page, the one that we navigated from
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            var deviceKey = "bluetoothDevice";
            if(parameters.ContainsKey(deviceKey))
            {
                Device = parameters.GetValue<BTDeviceInfo>(deviceKey);
            }
        }
    }
}

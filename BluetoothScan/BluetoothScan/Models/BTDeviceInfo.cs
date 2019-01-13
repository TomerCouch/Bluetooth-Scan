using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothScan.Models
{
    // A DTO for bluetooth devices
   public class BTDeviceInfo : BindableBase
    {
    protected string _deviceName;
    public string DeviceName{
        get => _deviceName;
        set => _deviceName = value;
    }
    protected string _macAddress;

        public BTDeviceInfo(string deviceName, string macAddress)
        {
            _deviceName = deviceName;
            _macAddress = macAddress;
        }

        public string MacAddress{
        get => _macAddress;
        set => _macAddress = value;
    }
    }
}

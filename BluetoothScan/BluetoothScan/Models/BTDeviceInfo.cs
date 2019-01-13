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
        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _deviceName = "UNKNOWN";
                }
                else
                {
                    _deviceName = value;
                }
            }

        }
        protected string _macAddress;
        public string MacAddress
        {
            get
            {
                return _macAddress;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _macAddress = "-";
                }
                else
                {
                    _macAddress = value;
                }
            }
        }

        public BTDeviceInfo(string deviceName, string macAddress)
        {
            DeviceName = deviceName;
            MacAddress = macAddress;
        }

    }
}

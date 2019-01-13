using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BluetoothScan.Models
{
    // It exposes Bluetooth devices from Droid outward - allows Xamarin sharing data from Android to Forms
    public interface IBluetoothDeviceManager
    {
        // It returns all found devices -> Paired bluetooth devices or unpaired, Classic or LTE
        ObservableCollection<BTDeviceInfo> ScanDevices();
    }
}

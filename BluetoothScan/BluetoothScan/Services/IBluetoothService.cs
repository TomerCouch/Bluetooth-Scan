using BluetoothScan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothScan.Services
{
    public interface IBluetoothService
    {
        Task<IList<BTDeviceInfo>> GetDevices();
    }
}

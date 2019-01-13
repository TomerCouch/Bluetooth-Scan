using BluetoothScan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothScan.Services
{
    class BluetoothService : IBluetoothService
    {

        // It Returns HARDCODED Data for now.
        public Task<IList<BTDeviceInfo>> GetDevices()
        {
            return Task.FromResult<IList<BTDeviceInfo>>(new List<BTDeviceInfo> {
                new BTDeviceInfo("A", "21"),
                new BTDeviceInfo("B", "314")
            });
        }
    }
}

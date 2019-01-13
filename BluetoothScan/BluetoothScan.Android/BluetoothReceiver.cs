using Android.App;
using Android.Bluetooth;
using Android.Content;
using BluetoothScan.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BluetoothScan.Droid
{
    // It manages bluetooth capabilities in the app: scanning, meta data on scanningand notifying the appropriate activity
    public class BluetoothReceiver : BroadcastReceiver
    {
        public static BluetoothAdapter BluetoothAdapter => BluetoothAdapter.DefaultAdapter;
        
        public BluetoothReceiver()
        {
            // Bluetooth is not supported on this device
            if (BluetoothAdapter == null)
            {
                throw new Exception("No Bluetooth adapter found.");
            }

            // Bluetooth Feature is OFF, it creates a request for the user to enable it
            if (!BluetoothAdapter.IsEnabled)
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                ((Activity)Forms.Context).StartActivityForResult(enableBtIntent, 1);
            }
            if (!BluetoothAdapter.IsEnabled)
            {
                throw new Exception("Bluetooth feature is OFF, unable to continue");
            }

        }

        // When new device is found, it updates the device list.
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;

            if (action == BluetoothDevice.ActionFound)
            {
                BluetoothDevice newDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                if (newDevice.BondState != Bond.Bonded)
                {
                    MainActivity.GetInstance().UpdateAdapter(new BTDeviceInfo(newDevice.Name, newDevice.Address));
                }
            }
        }
    }
}
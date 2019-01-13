using Android;
using Android.App;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using BluetoothScan.Droid;
using BluetoothScan.Models;
using Prism;
using Prism.Ioc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace BluetoothScan.Droid
{
    [Activity(Label = "BluetoothScan", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme.SplashScreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IBluetoothDeviceManager
    {
        private static MainActivity _instance; // App activity
        private BluetoothReceiver _receiver; // Handles Bluetooth Adapter activities
        public static BluetoothAdapter bluetoothAdapter => BluetoothAdapter.DefaultAdapter;
        private BLEScanCallback bleAdapterCallback; // Functions that handle a response from the bluetooth LE adapter i.e. BLE adapter callbacks

        public static ObservableCollection<BTDeviceInfo> devices; // updating collection of devices
        public static string adapterStatus = "Ready";

        // It exposes Update functions. 
        // TODO: other approaches may allow better encapsulation
        public static MainActivity GetInstance()
        {
            return _instance;
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // It creates a 3-second-delay for displaying the splash screen
            Thread.Sleep(3000);
            SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            Init();
        }

        // It checks appropiate premissions, gets paired BT devices, scans and enables listeners for new BT devices.
        private void Init()
        {
            _instance = this;

            BluetoothAccessValidator.RequestAccessIfNeeded(this);

            _receiver = new BluetoothReceiver();
            bleAdapterCallback = new BLEScanCallback();
            devices = getBondedDevices();

            SubscribeToBluetooth();

            startScanning();
        }

        // It stops Bluetooth scanning and broadcast listeners
        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnsubscribeFromBluetooth();
        }

        protected override void OnPause()
        {
            base.OnPause();

            cancelScanning();
        }

        protected override void OnResume()
        {
            base.OnResume();

            SubscribeToBluetooth();
        }
  
        // It converts bluetooth devices into a DTO device collection
        private ObservableCollection<BTDeviceInfo> getBondedDevices()
        {
           ObservableCollection<BTDeviceInfo> btDevices = new ObservableCollection<BTDeviceInfo>();

            foreach(BluetoothDevice device in bluetoothAdapter.BondedDevices)
            {
                btDevices.Add(new BTDeviceInfo(device.Name, device.Address));
            }

            return btDevices;
        }

        // It scans for both classic and BLE devices
        private void startScanning()
        {
            bluetoothAdapter.StartDiscovery();

            bluetoothAdapter.BluetoothLeScanner
            .StartScan(bleAdapterCallback);
        }

        // It stop both scans for both classic and BLE devices
        private void cancelScanning()
        {

            bluetoothAdapter.CancelDiscovery();

            bluetoothAdapter.BluetoothLeScanner.
                StopScan(bleAdapterCallback);
        }

        // It registers bluetooth receiver - for scanning and discovery of classic bluetooth devices
        private void SubscribeToBluetooth()
        {
                if (_receiver == null) return;

                RegisterReceiver(_receiver, new IntentFilter(BluetoothDevice.ActionFound));

        }
        
        // It unregisters listeners and resets the classic bluetooth receiver
        private void UnsubscribeFromBluetooth()
        {
            cancelScanning();

            if (_receiver != null)
            {
                UnregisterReceiver(_receiver);
            }
            _receiver = null;
            devices = null;
        }

        // It adds distinctively devices
        public void UpdateAdapter(BTDeviceInfo dataItem)
        {
            if (devices != null && dataItem != null && !devices.Any(device=> dataItem.DeviceName!= null && dataItem.DeviceName.Equals(device.DeviceName)))
            {
                devices.Add(dataItem);
            }
        }
        
        // It exposes device list for DI.Allows the use for Cross-Platforms
        public ObservableCollection<BTDeviceInfo> GetDevices()
        {
            return devices;
        }

        // It handles the callback for parsing the BLE scan result into BT DTOs
        // And updates the device collection
        public class BLEScanCallback : ScanCallback
        {
            public override void OnBatchScanResults(IList<ScanResult> results)
            {
                foreach (ScanResult result in results) {
                    addDevice(result);
                }
            }

            public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
            {
                addDevice(result);
            }

            public void addDevice(ScanResult result)
            {
                if(result == null || result.Device == null)
                {
                    return;
                }

                MainActivity.GetInstance().UpdateAdapter(
                      new BTDeviceInfo(result.Device.Name, result.Device.Address));
            }
        }

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}


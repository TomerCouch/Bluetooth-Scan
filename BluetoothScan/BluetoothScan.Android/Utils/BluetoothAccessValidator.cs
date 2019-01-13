using Android;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using BluetoothScan.Models;

namespace BluetoothScan.Droid
{
    public static class BluetoothAccessValidator
    {
        // It checks all needed permissions are granted
        // And asks for permissions if a permission access denied 
        public static void RequestAccessIfNeeded(Activity activity)
        {
            const int locationPermissionsRequestCode = 1000;

            var locationPermissions = new[]
            {
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation
            };

            // Has permission to access coarse location?
            var coarseLocationPermissionGranted =
                ContextCompat.CheckSelfPermission(activity, Manifest.Permission.AccessCoarseLocation);

            // Has permission to access fine location?
            var fineLocationPermissionGranted =
                ContextCompat.CheckSelfPermission(activity, Manifest.Permission.AccessFineLocation);

            // Request permission from the user if permissions are not granted
            if (coarseLocationPermissionGranted == Permission.Denied ||
                fineLocationPermissionGranted == Permission.Denied)
            {
                ActivityCompat.RequestPermissions(activity, locationPermissions, locationPermissionsRequestCode);
            }
        }
    }
}
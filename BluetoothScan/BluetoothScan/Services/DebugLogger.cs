﻿using Prism.Logging;
using System.Diagnostics;

namespace BluetoothScan.Services
{
    public class DebugLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Debug.WriteLine($"{category} - {priority}: {message}");
        }
    }
}
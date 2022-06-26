using System;
using System.Reflection;
using SNAP.API.Logging;
using Spectre.Console;
using StardewModdingAPI;

namespace SNAP.Logging
{
    public class SnapMonitor : ISnapMonitor
    {
        protected sealed class DisposableConsoleToggler : IDisposable
        {
            private readonly IMonitor Monitor;
            private readonly PropertyInfo WriteToConsole;
            private readonly object? OldWriteToConsole;

            public DisposableConsoleToggler(IMonitor monitor) {
                Monitor = monitor;
                WriteToConsole = monitor.GetType().GetProperty("WriteToConsole", BindingFlags.NonPublic | BindingFlags.Instance)!;
                OldWriteToConsole = WriteToConsole.GetValue(Monitor);
                WriteToConsole.SetValue(Monitor, false);
            }

            public void Dispose() {
                WriteToConsole.SetValue(Monitor, OldWriteToConsole);
            }
        }

        protected readonly IMonitor UnderlyingMonitor;

        public SnapMonitor(IMonitor underlyingMonitor) {
            UnderlyingMonitor = underlyingMonitor;
        }

        protected void DoWithoutConsole(Action action) {
            using DisposableConsoleToggler _ = new(UnderlyingMonitor);
            action();
        }

        #region IMonitor Impl

        public bool IsVerbose => UnderlyingMonitor.IsVerbose;

        public virtual void Log(string message, LogLevel level = LogLevel.Trace) {
            UnderlyingMonitor.Log(message, level);
        }

        public virtual void LogOnce(string message, LogLevel level = LogLevel.Trace) {
            UnderlyingMonitor.LogOnce(message, level);
        }

        public virtual void VerboseLog(string message) {
            UnderlyingMonitor.VerboseLog(message);
        }

        #endregion

        #region IConsoleMonitor Impl

        public virtual void ConsoleWrite(string msg, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, bool resetColor = false) {
            if (foregroundColor is not null) Console.ForegroundColor = foregroundColor.Value;
            if (backgroundColor is not null) Console.BackgroundColor = backgroundColor.Value;

            Console.Write(msg);

            if (resetColor) Console.ResetColor();
        }

        public virtual void ConsoleWriteLine(string msg, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, bool resetColor = false) {
            if (foregroundColor is not null) Console.ForegroundColor = foregroundColor.Value;
            if (backgroundColor is not null) Console.BackgroundColor = backgroundColor.Value;

            Console.WriteLine(msg);

            if (resetColor) Console.ResetColor();
        }

        public void MarkupWrite(string msg) {
            AnsiConsole.Markup(msg);
        }

        public void MarkupWriteLine(string msg) {
            AnsiConsole.MarkupLine(msg);
        }

        #endregion

        #region ILogFile

        public virtual void FileWriteLine(string message, LogLevel level = LogLevel.Trace) {
            DoWithoutConsole(() => UnderlyingMonitor.Log(message, level));
        }

        public virtual void FileWriteLineOnce(string message, LogLevel level = LogLevel.Trace) {
            DoWithoutConsole(() => UnderlyingMonitor.LogOnce(message, level));
        }

        public virtual void FileWriteLineVerbose(string message) {
            DoWithoutConsole(() => UnderlyingMonitor.VerboseLog(message));
        }

        #endregion
    }
}
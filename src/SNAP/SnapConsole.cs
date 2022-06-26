using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SNAP.API.Logging;
using StardewModdingAPI;

namespace SNAP
{
    /// <summary>
    ///     Console utilities.
    /// </summary>
    /// <remarks>
    ///     Relevant information: https://github.com/termstandard/colors
    /// </remarks>
    public static class SnapConsole
    {
        public readonly record struct PaddedString(string Text, int Length)
        {
            public override string ToString() {
                int padding = Length - Text.Length;
                
                int left = padding <= 0 ? 0 : padding / 2;
                int right = padding <= 0 ? 0 : padding - left;

                return new StringBuilder().Append(' ', left).Append(Text).Append(' ', right).ToString();
            }
        }

        public const char IgnoreChar = '\u200B';

        public static void WriteStartMessage(ISemanticVersion version, ISnapMonitor monitor) {
            const int length = 30;
            const string neonRed = "[#ff4554]";
            const string neonBlue = "[#00c3e3]";
            const string gray = "[#989898]";
            const string slash = "[/]";
            
            Assembly asm = typeof(SnapMod).Assembly;
            using Stream stream = asm.GetManifestResourceStream("SNAP.snap.txt")!;
            using Stream streamC = asm.GetManifestResourceStream("SNAP.snapc.txt")!;
            string snapUncolored = new StreamReader(stream).ReadToEnd();
            string snapColored = new StreamReader(streamC).ReadToEnd()
                                                          .Replace("b", neonBlue)
                                                          .Replace("r", neonRed)
                                                          .Replace("g", gray)
                                                          .Replace(".", slash);

            string subtitleUncolored = new PaddedString($"snap! v{version.ToString()} by Tomat", length).ToString();
            string subtitleColored = $" [white]{subtitleUncolored}[/]";

            List<string> uncolored = snapUncolored.Split("\r\n").ToList();
            List<string> colored = snapColored.Split("\r\n").ToList();
            uncolored.Add(subtitleUncolored);
            colored.Add(subtitleColored);
            
            foreach (string line in uncolored) monitor.FileWriteLine(line, LogLevel.Info);
            foreach (string line in colored) monitor.MarkupWrite(line);
        }
    }
}

/*
                           ___
   _________  ____  ____  /  /
  / ___/ __ \/ __ \/ __ \/  /
 (__  ) / / / /_/ / /_/ /__/
/____/_/ /_/\__/_/ ____/__/
                / /
               /_/
*/
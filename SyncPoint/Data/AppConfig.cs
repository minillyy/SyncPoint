using System;
using System.Collections.Generic;
using System.IO;

namespace SyncPoint.Data
{
    /// <summary>
    /// Reads key=value pairs from a config file.
    /// Keeps sensitive data out of source code.
    /// </summary>
    internal static class AppConfig
    {
        private static Dictionary<string, string>
            _values = null;

        private static readonly string ConfigPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "instructor.config");

        // ── Load config file once ────────────────────────
        private static void EnsureLoaded()
        {
            if (_values != null) return;

            _values =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(ConfigPath))
            {
                throw new FileNotFoundException(
                    "instructor.config not found.\n" +
                    "Please create it in the " +
                    "application folder.",
                    ConfigPath);
            }

            foreach (string line in
                File.ReadAllLines(ConfigPath))
            {
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) ||
                    line.StartsWith("#"))
                    continue;

                int idx = line.IndexOf('=');
                if (idx < 0) continue;

                string key =
                    line.Substring(0, idx).Trim();
                string value =
                    line.Substring(idx + 1).Trim();

                _values[key] = value;
            }
        }

        // ── Get a value by key ───────────────────────────
        public static string Get(string key)
        {
            EnsureLoaded();
            return _values.TryGetValue(
                key, out string val) ? val : "";
        }
    }
}
﻿using GitUIPluginInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitExtensions.SolutionRunner
{
    internal class PluginSettings : IEnumerable<ISetting>
    {
        public const string DefaultExecutableArguments = "{SolutionPath}";

        /// <summary>
        /// Gets a property holding path to executable to start.
        /// </summary>
        public static StringSetting ExecutablePathProperty { get; } = new StringSetting("Executable Path", "Executable path to start by clicking on solution file", null);

        /// <summary>
        /// Gets a property holding arguments for executa ble to start.
        /// </summary>
        public static StringSetting ExecutableArgumentsProperty { get; } = new StringSetting("Executable Arguments", $"Optional arguments for executable ({DefaultExecutableArguments} will be replaced with selected solution file)", DefaultExecutableArguments);

        /// <summary>
        /// Gets a property holding arguments for executa ble to start.
        /// </summary>
        public static BoolSetting IsTopLevelSearchedOnlyProperty { get; } = new BoolSetting("Top Level Search", "Search only top level directory structure", false);
        
        private readonly ISettingsSource source;

        /// <summary>
        /// Gets current value of <see cref="ExecutablePathProperty"/>.
        /// </summary>
        public string ExecutablePath => source.GetValue(ExecutablePathProperty.Name, ExecutablePathProperty.DefaultValue, t => t);

        /// <summary>
        /// Gets current value of <see cref="ExecutableArgumentsProperty"/>.
        /// </summary>
        public string ExecutableArguments => source.GetValue(ExecutableArgumentsProperty.Name, ExecutableArgumentsProperty.DefaultValue, t => t);

        /// <summary>
        /// Gets current value of <see cref="IsTopLevelSearchedOnlyProperty"/>.
        /// </summary>
        public bool IsTopLevelSearchedOnly => source.GetValue(IsTopLevelSearchedOnlyProperty.Name, IsTopLevelSearchedOnlyProperty.DefaultValue, t => Boolean.Parse(t));
        
        public PluginSettings(ISettingsSource source)
        {
            this.source = source;
        }

        #region IEnumerable<ISetting>

        private static readonly List<ISetting> properties;

        public static bool HasProperties => properties.Count > 0;

        static PluginSettings()
        {
            properties = new List<ISetting>(3)
            {
                ExecutablePathProperty,
                ExecutableArgumentsProperty,
                IsTopLevelSearchedOnlyProperty,
            };
        }

        public IEnumerator<ISetting> GetEnumerator()
            => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}

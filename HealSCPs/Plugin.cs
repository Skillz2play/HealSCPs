using PluginAPI.Core;
using PlayerRoles.Ragdolls;
using PluginAPI.Core.Attributes;
using System;

namespace HealSCPs
{
    public class Plugin
    {
        public const string PluginName = "HealSCPs";
        public const string PluginVersion = "1.0.0";
        public const string PluginDesc = "A plugin to make \'better\' balancing changes to SL.";
		public const string Author = "Skillz2play (Ported to NWAPI by btelnyy)";
        public static Plugin Instance => instance;
        private static Plugin instance;
        [PluginConfig(PluginName + ".yml")]
        public Config Config = new Config();

        [PluginEntryPoint(PluginName, PluginVersion, PluginDesc, Author)]
        public void LoadPlugin()
        {
            if (!Config.IsEnabled)
            {
                Log.Debug("Plugin is disabled!");
                return;
            }
            instance = this;
            Log.Info("Registering events...");
            PluginAPI.Events.EventManager.RegisterAllEvents(this);
        }

        [PluginUnload()]
        public void Unload()
        {
            Config = null;
        }
    }
}
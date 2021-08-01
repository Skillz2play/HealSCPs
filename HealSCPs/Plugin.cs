using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealSCPs
{
    public class Plugin : Plugin<Config>
    {
        
        private static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(() => new Plugin());
        internal static readonly List<int> BlacklistedIds = new List<int>();

        public static Plugin Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public override string Name => "HealSCPs";
        public override string Author => "Skillz2play";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(2, 11, 0);

        private Plugin()
        {
        }

        public override void OnEnabled()
        {

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
             
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            
        }

        public void UnregisterEvents()
        {
            
        }
    }
}
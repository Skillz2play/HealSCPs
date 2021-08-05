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

        private static readonly Plugin Singleton = new Plugin();
        public static Plugin Instance => Singleton;

        public override string Name => "HealSCPs";
        public override string Author => "Skillz2play";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(2, 11, 0);

        private Plugin()
        {
        }
    }
}
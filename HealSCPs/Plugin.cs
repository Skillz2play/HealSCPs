using Exiled.API.Features;
using System;

namespace HealSCPs
{
    public class Plugin : Plugin<Config>
    {
		public override void OnEnabled()
		{
				Plugin.singleton = this;
				base.OnEnabled();
		}
		public override void OnDisabled()
		{
				Plugin.singleton = null;
				base.OnDisabled();
		}
		public static Plugin Instance => singleton;

		public override string Name => "HealSCPs";
		public override string Author => "Skillz2play";
		public override Version Version => new Version(2, 0, 0);
		public override Version RequiredExiledVersion => new Version(7, 0, 0);

		private static Plugin singleton;
	}
}
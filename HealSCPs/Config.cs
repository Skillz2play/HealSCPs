using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealSCPs
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("How much health the SCP gets from a Medkit.")]
        public float MedkitHealthRecieve { get; set; } = 75;

        [Description("How much health the SCP gets from SCP 500.")]
        public float SCP500HealthRecieve { get; set; } = 100;

        [Description("How much health the SCP gets from Adrenaline.")]
        public float AdrenalineHealthRecieve { get; set; } = 35;

        [Description("How much health the SCP gets from Painkillers")]
        public float PainkillersHealthRecieve { get; set; } = 10;

        [Description("How much health the SCP gets from SCP 207")]
        public float SCP207HealthRecieve { get; set; } = 25;

        [Description("Does the SCP recieve 207 effects?")]
        public bool Recieve207Effects { get; set; } = true;

        [Description("Does the SCP recieve 500 effects?")]
        public bool Recieve500Effects { get; set; } = true;

        [Description("Does the SCP recieve Painkiller effects?")]
        public bool ReceivePainkillerEffects { get; set; } = true;

        [Description("Does the SCP recieve Adrenaline effects?")]
        public bool RecieveAdrenalineEffects { get; set; } = true;
    }
}
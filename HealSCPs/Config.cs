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

        [Description("How much health the SCP gets from Painkillers.")]
        public float PainkillersHealthRecieve { get; set; } = 10;

        [Description("How much health the SCP gets from SCP 207.")]
        public float SCP207HealthRecieve { get; set; } = 25;

        [Description("How far do the SCPs have to be for the health to be applied?")]
        public float Distance { get; set; } = 5f;
    }
}
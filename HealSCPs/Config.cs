using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

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

        [Description("What SCPs are allowed to be healed? NOT USED CURRENTLY")]
        public List<RoleType> AllowedScps { get; private set; } = new List<RoleType>() { RoleType.Scp049, RoleType.Scp0492, RoleType.Scp079, RoleType.Scp096, RoleType.Scp106, RoleType.Scp173, RoleType.Scp93953, RoleType.Scp93989 };
    }
}
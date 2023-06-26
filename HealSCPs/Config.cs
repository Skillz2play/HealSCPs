using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HealSCPs
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("How far do the SCPs have to be for the health to be applied?")]
        public float Distance { get; set; } = 5f;

        [Description("What SCPs are allowed to be healed?")]
        public List<RoleTypeId> AllowedScps { get; private set; } = new List<RoleTypeId>() { RoleTypeId.Scp049, RoleTypeId.Scp0492, RoleTypeId.Scp079, RoleTypeId.Scp096, RoleTypeId.Scp106, RoleTypeId.Scp173, RoleTypeId.Scp939 };

        public Dictionary<ItemType, HealItemProperties> AllowedHeals { get; set; } = new Dictionary<ItemType, HealItemProperties>()
        {
            [ItemType.Medkit] = new()
            {
                ApplyOriginalEffects = false,
                InstantHealAmount = 65,
                EffectInfo = new List<EffectsInfo>(),
            },
            [ItemType.SCP500] = new()
            {
                ApplyOriginalEffects = true,
                InstantHealAmount = 0,
                EffectInfo = new List<EffectsInfo>(),
            },
            [ItemType.SCP207] = new()
            {
                ApplyOriginalEffects = false,
                InstantHealAmount = 50,
                EffectInfo = new List<EffectsInfo>(),
            },
            [ItemType.Adrenaline] = new()
            {
                ApplyOriginalEffects = false,
                InstantHealAmount = 50,
                EffectInfo = new List<EffectsInfo>
                {
                    new EffectsInfo()
                    {
                        EffectAmount = 1,
                        EffectType = EffectType.Invigorated,
                        ShouldAddIfPresent = true,
                        Time = 5,
                    },
                },
            },
            [ItemType.Painkillers] = new()
            {
                ApplyOriginalEffects = true, // yes this is intended
                InstantHealAmount = 50, // yes this is intended
                EffectInfo = new List<EffectsInfo>(),
            },
        };

        bool IConfig.Debug { get; set; }

        public sealed class HealItemProperties
        {
            public float InstantHealAmount { get; set; } = 0f;

            public List<EffectsInfo> EffectInfo { get; set; } = new List<EffectsInfo>
            {
            new()
            {
                EffectType = default,
                ShouldAddIfPresent = false,
                Time = 0,
                EffectAmount = 0,
            },
            };

            public bool ApplyOriginalEffects { get; set; } = false;
        }

        public sealed class EffectsInfo
        {
            public EffectType EffectType { get; set; }

            public bool ShouldAddIfPresent { get; set; }

            public float Time { get; set; }

            public byte EffectAmount { get; set; }
        }
    }
}
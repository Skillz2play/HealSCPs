using Exiled.API.Features;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Thirdperson;
using System;

namespace HealSCPs.API
{
    /// <summary>
    /// A disposable struct for disabling hitboxes temporarily.
    /// </summary>
    public readonly struct HitboxDisabler : IDisposable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="HitboxDisabler"/> struct.
        /// </summary>
        public HitboxDisabler(Player player) : this(player.ReferenceHub)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="HitboxDisabler"/> struct.
        /// </summary>
        public HitboxDisabler(ReferenceHub hub) : this(hub.roleManager.CurrentRole)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="HitboxDisabler"/> struct.
        /// </summary>
        public HitboxDisabler(PlayerRoleBase role)
        {
            if (role is not IFpcRole fpcRole)
            {
                Model = null;
                return;
            }

            Model = fpcRole.FpcModule.CharacterModelInstance;
            ToggleHitboxes(false);
        }

        /// <summary>
        /// The character model associated with this hitbox disabler.
        /// </summary>
        public readonly CharacterModel Model;

        /// <summary>
        /// Enables or disables the hitboxes.
        /// </summary>
        public void ToggleHitboxes(bool enabled)
        {
            if (Model is not CharacterModel model)
                return;

            model.Hitboxes.ForEach(enabled ? ToggleHitboxOn : ToggleHitboxOff);
        }

        /// <summary>
        /// Enables the hitboxes.
        /// </summary>
        public void Dispose()
        {
            ToggleHitboxes(true);
        }

        private static void ToggleHitboxOff(HitboxIdentity hitbox)
        {
            hitbox.SetColliders(false);
        }

        private static void ToggleHitboxOn(HitboxIdentity hitbox)
        {
            hitbox.SetColliders(true);
        }
    }
}
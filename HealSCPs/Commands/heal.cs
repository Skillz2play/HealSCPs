using CommandSystem;
using System;
using Exiled.API.Features;
using UnityEngine;
using PlayerRoles;
using InventorySystem;
using InventorySystem.Items;
using static HealSCPs.Config;
using CustomPlayerEffects;
using InventorySystem.Items.Usables;
using MEC;
using HealSCPs.API;

namespace HealSCPs
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class heal : ICommand
    {
        public string Command { get; } = "heal";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Heal any scp in front of you";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((CommandSender)sender);

            string text = "n SCP";

            if (Plugin.Instance.Config.HealNonSCPs)
            {
                text = " player";
            }

            if (!player.IsHuman)
            {
                response = "You can't run this command!";
                return false;
            }
            
            if (player.CurrentItem is null
                || !InventoryItemLoader.AvailableItems.TryGetValue(player.CurrentItem.Base.ItemTypeId, out ItemBase item)
                || !Plugin.Instance.Config.AllowedHeals.TryGetValue(player.CurrentItem.Base.ItemTypeId, out HealItemProperties healProperties))
            {
                response = "You are not allowed to heal with that item!";
                return false;
            }
            Player hitPlayer;
            using (new HitboxDisabler(player))
            {
                Transform cam = player.CameraTransform;
                if (!Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Plugin.Instance.Config.Distance, LayerMask.GetMask("Default", "Player", "Hitbox")))
                {
                    response = $"You must be looking at a{text} in order to heal them.";
                    return false;
                }

                hitPlayer = Player.Get(hit.transform.root.gameObject);
            }
            if (hitPlayer == null || (!Plugin.Instance.Config.HealNonSCPs && hitPlayer.Role.Team != Team.SCPs))
            {
                response = $"You must be looking at a{text} in order to heal them.";
                return false;
            }
            if (!Plugin.Instance.Config.AllowedScps.Contains(hitPlayer.Role) && !Plugin.Instance.Config.HealNonSCPs)            
            {
                response = "This SCP is not allowed to be healed!";
                return false;
            }

            if (Plugin.Instance.Config.AllowedScps.Contains(hitPlayer.Role))
            {
                Heal(player, hitPlayer, item, healProperties);
            }
            else
            {
                Timing.CallDelayed(2, () => Heal(player, hitPlayer, item, healProperties));
            }

            response = $"You have healed {hitPlayer.Nickname}!";
            return true;
        }

        public void Heal(Player player, Player hitPlayer, ItemBase item, HealItemProperties healProperties)
        {
            hitPlayer.Heal(healProperties.InstantHealAmount);
            hitPlayer.ShowHint($"You have been healed by {player.Nickname}!");
            ushort OldSerial = player.CurrentItem.Base.ItemSerial;
            player.RemoveHeldItem();
            if (healProperties.ApplyOriginalEffects)
            {
                item = UnityEngine.Object.Instantiate(item.gameObject).GetComponent<ItemBase>() as Consumable;
                item.ItemSerial = OldSerial;

                if (item is Consumable consumable)
                {
                    item.Owner = hitPlayer.ReferenceHub;
                    consumable.ServerOnUsingCompleted();
                }

                UnityEngine.Object.Destroy(item.gameObject);
            }

            PlayerEffectsController controller = hitPlayer.ReferenceHub.playerEffectsController;

            foreach (EffectsInfo effect in healProperties.EffectInfo)
            {
                if (hitPlayer.TryGetEffect(effect.EffectType, out StatusEffectBase statusEffect))
                {
                    byte newValue = (byte)Mathf.Min(255, statusEffect.Intensity + effect.EffectAmount);

                    controller.ChangeState(statusEffect.GetType().Name, newValue, effect.Time, effect.ShouldAddIfPresent);
                }
            }
        }
    }
}
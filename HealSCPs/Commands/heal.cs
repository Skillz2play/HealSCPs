using CommandSystem;
using System;
using Exiled.API.Features;
using UnityEngine;
using Exiled.API.Extensions;
using PlayerRoles;
using InventorySystem;
using InventorySystem.Items;
using static HealSCPs.Config;
using CustomPlayerEffects;
using InventorySystem.Items.Usables;

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
            Transform cam = player.CameraTransform;
            if (!Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Plugin.Instance.Config.Distance, LayerMask.GetMask("Default", "Player", "Hitbox")))
            {
                response = "You must be looking at an SCP in order to heal them.";
                return false;
            }
            Player hitPlayer = Player.Get(hit.transform.GetComponentInParent<ReferenceHub>());
            if (hitPlayer == null || hitPlayer.Role.Team != Team.SCPs)
            {
                response = "You must be looking at an SCP in order to heal them.";
                return false;
            }
            if (!Plugin.Instance.Config.AllowedScps.Contains(hitPlayer.Role))            
            {
                response = "This SCP is not allowed to be healed!";
                return false;
            }
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
            player.RemoveHeldItem();
            response = $"You have healed {hitPlayer.Nickname}!";
            return true;
        }
    }
}
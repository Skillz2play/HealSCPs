using CommandSystem;
using System;
using Exiled.API.Features;
using UnityEngine;
using Exiled.API.Extensions;

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
            if (!player.CurrentItem.Type.IsMedical())
            {
                response = "You can't heal SCPs with that item!";
                return false;
            }
            Transform cam = player.CameraTransform;
            if (!Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Plugin.Instance.Config.Distance, LayerMask.GetMask("Default", "Player", "Hitbox")))
            {
                response = "You must be looking at an SCP in order to heal them.";
                return false;
            }
            Player hitPlayer = Player.Get(hit.transform.GetComponentInParent<ReferenceHub>());
            if (hitPlayer == null || hitPlayer.Role.Team != Team.SCP)
            {
                response = "You must be looking at an SCP in order to heal them.";
                return false;
            }
            if (!Plugin.Instance.Config.AllowedScps.Contains(hitPlayer.Role))            
            {
                response = "This SCP is not allowed to be healed!";
                return false;
            }

            float amount = player.CurrentItem.Type switch
            {
                ItemType.Adrenaline => Plugin.Instance.Config.AdrenalineHealthRecieve,
                ItemType.Medkit => Plugin.Instance.Config.MedkitHealthRecieve,
                ItemType.Painkillers => Plugin.Instance.Config.PainkillersHealthRecieve,
                ItemType.SCP207 => Plugin.Instance.Config.SCP207HealthRecieve,
                ItemType.SCP500 => Plugin.Instance.Config.SCP500HealthRecieve,
                _ => 0
            };
            hitPlayer.ShowHint($"You have been healed by {player.Nickname} for {amount}HP");
            hitPlayer.Heal(amount);
            player.RemoveHeldItem();
            response = $"Healed Player {hitPlayer.Nickname}";
            return true;
        }
    }
}
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

            float amount = 0f;
            switch (player.CurrentItem.Type)
            {
                case ItemType.Adrenaline:
                    amount = Plugin.Instance.Config.AdrenalineHealthRecieve;
                    break;
                case ItemType.Medkit:
                    amount = Plugin.Instance.Config.MedkitHealthRecieve;
                    break;
                case ItemType.Painkillers:
                    amount = Plugin.Instance.Config.PainkillersHealthRecieve;
                    break;
                case ItemType.SCP207:
                    amount = Plugin.Instance.Config.SCP207HealthRecieve;
                    break;
                case ItemType.SCP500:
                    amount = Plugin.Instance.Config.SCP500HealthRecieve;
                    break;
            }
            hitPlayer.Heal(amount);
            player.RemoveHeldItem();
            response = $"Healed Player {hitPlayer.Nickname}";
            return true;
        }
    }
}
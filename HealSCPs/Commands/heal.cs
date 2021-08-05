using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteAdmin;
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
            Player player = Player.Get((sender as PlayerCommandSender)?.ReferenceHub);
            
            if (player.IsHuman)
            {
                // we are humans
                if (player.CurrentItem.id.IsMedical())
                {

                    // we are holding a medical item
                    var cam = player.CameraTransform;
                            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Plugin.Instance.Config.Distance, LayerMask.GetMask("Default","Player","Hitbox")))
                            {
                                Player hitPlayer = Player.Get(hit.transform.GetComponentInParent<ReferenceHub>());
                                if (hitPlayer != null && hitPlayer.Team == Team.SCP)
                                {
                                    float amount = 0f;
                                    switch (player.CurrentItem.id)
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
                                        hitPlayer.ReferenceHub.playerStats.HealHPAmount(amount);
                                        player.RemoveItem();
                                    response = $"Healed Player {hitPlayer.Nickname}";
                            return true;
                        }
                        else
                        {
                            response = "You must be looking at an SCP in order to heal them.";
                            return true;
                        }
                    }
                    else
                    {
                        response = "You must be looking at an SCP in order to heal them.";
                        return true;
                    }
                }
                else
                {
                    response = "You can't heal SCPs with that item!";
                    return true;
                }
            }
            else
            {
                response = "You can't run this command!";
                return true;
            }
        }
    }
}

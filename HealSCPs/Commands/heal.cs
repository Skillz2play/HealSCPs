using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteAdmin;
using Exiled.API.Features;
using UnityEngine;

namespace HealSCPs
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class heal : ICommand
    {
        
        // Add the health receive so it actually heals the scps instead of just printing a message
        // Should also probably clean up the code a bit so it looks nicer
        // It's a wee bit messy

        // Compress all the if statements into 2 if else statements, one for class detection and one for item detection

        public string Command { get; } = "heal";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Heal any scp in front of you";

        public RoleType SCP;
        public RoleType Human;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((sender as PlayerCommandSender)?.ReferenceHub);
            if (player == null || Plugin.BlacklistedIds.Contains(player.Id) || player.SessionVariables.ContainsKey("IsGhostSpectator"))
            {
                response = string.Empty;
                return false;
            }

            switch (player.Team)
            {
                case Team.CDP:
                case Team.CHI:
                case Team.MTF:
                case Team.RSC:
                    
                    // we are humans
                    switch (player.CurrentItem.id)
                    {
                        case ItemType.Adrenaline:
                        case ItemType.Painkillers:
                        case ItemType.Medkit:
                        case ItemType.SCP500:
                        case ItemType.SCP207:
                            // we are holding a medical item
                            var cam = player.CameraTransform;
                            bool didHit = Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Plugin.Instance.Config.Distance);
                            if (didHit)
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
                                    hitPlayer.Health = Mathf.Clamp(hitPlayer.Health + amount, 0f, hitPlayer.MaxHealth);
                                    player.RemoveItem();
                                    response = $"Healed Player {hitPlayer.Nickname}";
                                    return true;
                                }
                            }
                            response = "You must be looking at an SCP in order to heal them.";
                            return true;
                        default:
                            response = "You can't heal SCPs with that item.";
                            return true;
                    }

                default:
                    response = "You can't run this command";
                    return true;


            }
        }
    }
}

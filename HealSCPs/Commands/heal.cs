using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteAdmin;
using Exiled.API.Features;

namespace HealSCPs
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class heal : ICommand
    {
        // Add player dectection to make sure the player is healing the scp and not the air
        // Also add the health receive so it actually heals the scps instead of just printing a message
        // Not sure if that needs an event handler or not
        // We'll see
        // Detect if a player is an scp through raycast or whatever or if they are in a radius near the SCP, execute the command
        
        public string Command { get; } = "heal";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Heal any scp in front of you";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((sender as PlayerCommandSender)?.ReferenceHub);
            if (player == null || Plugin.BlacklistedIds.Contains(player.Id) || player.SessionVariables.ContainsKey("IsGhostSpectator"))
            {
                response = string.Empty;
                return false;
            }


            Inventory.SyncItemInfo item = player.CurrentItem;
            if (item == default)
            {
                response = "You can't heal SCPs with that item.";
                return false;
            }

            if (item.id == ItemType.Medkit)
            {
                response = "You healed the SCP with a Medkit";
                return true;
            }
            
            if (item.id == ItemType.Painkillers)
            {
                response = "You healed the SCP with a Painkillers";
                return true;
            }

            if (item.id == ItemType.SCP500)
            {
                response = "You healed the SCP with SCP 500";
                return true;
            }

            if (item.id == ItemType.Adrenaline)
            {
                response = "You healed the SCP with Adrenaline";
                return true;
            }

            if (item.id == ItemType.SCP207)
            {
                response = "You healed the SCP with SCP 207";
                return true;
            }


            response = "";
            return true;
        }
    }
}

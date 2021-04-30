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
        
        // Add the health receive so it actually heals the scps instead of just printing a message
        // Should also probably clean up the code a bit so it looks nicer
        // It's a wee bit messy

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

            if (player.Role != RoleType.Scp049)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp0492)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp079)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp096)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp106)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp173)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp93953)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role != RoleType.Scp93989)
            {
                response = "You cant run this command";
                return false;
            }

            if (player.Role == RoleType.ChaosInsurgency)
            {
                response = "You can run this command";
                return true;
            }

            if (player.Role == RoleType.ClassD)
            {
                response = "You can run this command";
                return true;
            }

            if (player.Role == RoleType.Scientist)
            {
                response = "You can run this command";
                return true;
            }

            if (player.Role == RoleType.FacilityGuard)
            {
                response = "You can run this command";
                return true;
            }
            
            if (player.Role == RoleType.NtfCadet)
            {
                response = "You can run this command";
                return true;
            }

            if (player.Role == RoleType.NtfCommander)
            {
                response = "You can run this command";
                return true;
            }

            if (player.Role == RoleType.NtfLieutenant)
            {
                response = "You can run this command";
                return true;
            }

            if (player.Role == RoleType.NtfScientist)
            {
                response = "You can run this command";
                return true;
            }

            // This aint pretty but it gets the job done

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

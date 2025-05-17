using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;

namespace RecipeExtract.Common.Commands
{
    /// <summary>
    /// Command class for exporting recipe data through chat commands
    /// </summary>
    public class ExportRecipeChatCommand : ModCommand
    {
        // This is the command input in the chat box, without the slash
        public override string Command => "RecipeExtract";

        // The type of the command, can be Chat, Server, World (for single-player and host, Chat is usually appropriate)
        public override CommandType Type => CommandType.Chat;

        // (Optional) The description of the command, will be shown when player inputs /help export_recipe
        public override string Description => "Exports all current game recipes to a JSON file in UTF-8 format.";

        // (Optional) The usage of the command, will be shown when player inputs /help export_recipe
        public override string Usage => "/RecipeExtract";

        // The method called when the command is executed
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            // Check if there are any unexpected parameters
            if (args.Length > 0)
            {
                caller.Reply("Error: This command does not accept any parameters.", Color.Orange);
                caller.Reply($"Usage: {Usage}", Color.Orange);
                return;
            }

            // Get your mod instance
            if (ModContent.GetInstance<RecipeExtract>() is RecipeExtract modInstance)
            {
                try
                {
                    // Call the export method in the mod instance, and pass caller so the method can reply messages
                    modInstance.ExportData();
                }
                catch (Exception e)
                {
                    // Catch unexpected errors and notify the player and log them
                    string errorMsg = "An unexpected error occurred while executing the export command.";
                    caller.Reply(errorMsg, Color.Red);
                    modInstance.Logger.Error($"{errorMsg} {e}");
                }
            }
            else
            {
                caller.Reply("Error: Failed to get the mod instance. The mod may not be loaded correctly.", Color.Red);
            }
        }
    }
} 
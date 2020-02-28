using Terraria.ModLoader;

namespace Revolutions.Utils
{
    public class Testcmd1 : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "talk";

        public override string Description
            => "talk -time -text";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            int a = 0;
            int.TryParse(args[1], out a);
            new Talk(0, args[0], a, caller.Player);
        }
    }
}
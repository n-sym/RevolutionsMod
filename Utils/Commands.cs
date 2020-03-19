using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;
using Terraria.Localization;
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
            try
            {
                int.TryParse(args[1], out a);
            }
            catch (Exception e)
            {
                Helper.Print(e.Message);
            }
            new Talk(0, args[0], a, caller.Player);
        }
    }
    public class Testcmd2 : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "logo";

        public override string Description
            => "show logo";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            RevolutionsPlayer.logoTimer = 90;
            new Talk(0, Language.GetTextValue("Mods.Revolutions.Talk.taishuaile"), 180, caller.Player);
        }
    }

    public class Testcmd3 : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "addsf";

        public override string Description
            => "give you star flare with the quantities you want./addsf -quantity";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            RevolutionsPlayer rp = caller.Player.GetModPlayer<RevolutionsPlayer>();
            int a = 0;
            try
            {
                int.TryParse(args[0], out a);
            }
            catch (Exception e)
            {
                Helper.Print(e.Message);
            }
            rp.starFlare[0] += a;
            rp.maxStarFlare += rp.maxStarFlare - rp.starFlare[0] + 2 * a;
        }
    }
    public class Testcmd4 : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "dist";

        public override string Description
            => "distortion";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!Revolutions.Settings.dist || args.Length == 0) Revolutions.Settings.dist = !Revolutions.Settings.dist;
            if (args.Length != 0 && Revolutions.Settings.dist)
            {
                if (args[0] == "-1") args[0] = "100";
                try
                {
                    Filters.Scene["Filter"].GetShader().SwapProgram("Filter" + args[0]);
                }
                catch (Exception e)
                {
                    Helper.Print(e.Message);
                }
            }
        }
    }
    public class Testcmd5 : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "sfc";

        public override string Description
            => "sfc";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            RevolutionsPlayer z = caller.Player.GetModPlayer<RevolutionsPlayer>();
            z.starFlareStatus = true;
            if (args.Length == 1)
            {
                int a = 0;
                int.TryParse(args[0], out a);
                z.starFlareColorType = a;
            }
            if (args.Length == 3)
            {
                int r = 0;
                int.TryParse(args[0], out r);
                int g = 0;
                int.TryParse(args[0], out g);
                int b = 0;
                int.TryParse(args[0], out b);
                RevolutionsPlayer.customStarFlareColor = new Color(r, g, b);
            }
        }
    }
}
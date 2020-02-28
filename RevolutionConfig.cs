using Terraria.ModLoader.Config;

namespace Revolutions
{
    public class RevolutionsConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

    }
    public class RevolutionsConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

    }
}


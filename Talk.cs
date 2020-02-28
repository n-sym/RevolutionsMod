using Terraria;

namespace Revolutions
{
    public class Talk
    {
        public Talk(int who, string type, int time, Player player)
        {
            if (who == 0)
            {
                player.GetModPlayer<RevolutionsPlayer>().nowSaying = type;
                player.GetModPlayer<RevolutionsPlayer>().talkActive = time;
            }

        }
    }
    public class Mutter
    {
        public const string p01 = "Talk.NewHistory01";
        public const string p02 = "Talk.NewHistory02";
        public const string p03 = "Talk.NewHistory03";
        public const string a01 = "Talk.GoForTheEyes";
        public const string a02 = "Talk.CritalAttacks";
        public const string b01 = "Talk.LowHlthAtkSkelBoss";
        public const string b02 = "Talk.DftSkelBoss";
        public const string b03 = "Talk.AtkCthuEyes";
        public const string b04 = "Talk.DftCthuEyes";
        public const string i01 = "Talk.CrimBiome01";
        public const string i02 = "Talk.CrimBiome02";
        public const string i03 = "Talk.CrimBiome03";


        /*public string Get
        {
            get { return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height); }
        }*/
    }
}

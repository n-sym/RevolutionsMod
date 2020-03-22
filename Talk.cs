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
            if (who > 0)
            {
                RevolutionsPlayer.npctalk.Add(new StringTimerInt(type, who, time));
            }
        }
    }
}

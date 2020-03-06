using Terraria;

namespace Revolutions
{
    public class PlayerWithNum
    {
        public Player player;
        public float num;
    }
    public class StringTimerInt
    {
        public string text = "";
        public int number = 0;
        public int timer = 0;
        public StringTimerInt(string talk, int num, int time)
        {
            text = talk;
            number = num;
            timer = time;
        }
    }

}

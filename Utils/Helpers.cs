using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.IO;
using System.Xml;
using Terraria;
using Terraria.Localization;

namespace Revolutions.Utils
{
    public class Helper
    {
        /// <summary>
        /// 熵池用于生成未知数。它特别的地方是可以重复利用。
        /// </summary>
        public static int[] EntroptPool = new int[10001];
        public static string spname;
        /// <summary>
		/// 把t转化为字符串显示于屏幕左下角
		/// </summary>
        public static void Print(object t)
        {
            if (Main.netMode != 2)
            {
                Main.NewText(t.ToString(), 255, 255, 255);
            }
            else
            {
                NetworkText te = NetworkText.FromKey(t.ToString());
                NetMessage.BroadcastChatMessage(te, Color.White);
            }
        }
        /// <summary>
		/// 把t以指定颜色显示于屏幕左下角
		/// </summary>
        public static void PrintColor(string t, byte r, byte g, byte b)
        {
            if (Main.netMode != 2)
            {
                Main.NewText(t, r, g, b);
            }
            else
            {
                NetworkText te = NetworkText.FromKey(t);
                NetMessage.BroadcastChatMessage(te, new Color(r, g, b));
            }
        }
        /// <summary>
		/// 线性插值
		/// </summary>
        public static int LinearInterpolate(int a, int b, int c)
        {
            return a * (1 - c) + b * c;
        }
        /// <summary>
		/// 线性插值。相当于maxi等分线段。这个方法将返回线段中第i段的位置
		/// </summary>
        public static Vector2 GetCloser(float x, float y, float targetx, float targety, float i, float maxi)
        {
            x *= maxi - i;
            x /= maxi;
            y *= maxi - i;
            y /= maxi;
            targetx *= i;
            targetx /= maxi;
            targety *= i;
            targety /= maxi;
            return new Vector2(x + targetx, y + targety);
        }
        /// <summary>
		/// 线性插值。相当于maxi等分线段。这个方法将返回线段中第i段的位置
		/// </summary>
        public static Vector2 GetCloser(Vector2 current, Vector2 target, float i, float maxi)
        {
            current *= maxi - i;
            current /= maxi;
            target *= i;
            target /= maxi;
            return current + target;
        }
        /// <summary>
		/// 对颜色的线性插值，将混合指定的两个颜色
		/// </summary>
        public static Color GetCloserColor(Color current, Color target, float i, float maxi)
        {
            float r = current.R;
            float g = current.G;
            float b = current.B;
            float tr = target.R;
            float tg = target.G;
            float tb = target.B;
            r *= maxi - i;
            r /= maxi;
            g *= maxi - i;
            g /= maxi;
            b *= maxi - i;
            b /= maxi;
            tr *= i;
            tr /= maxi;
            tg *= i;
            tg /= maxi;
            tb *= i;
            tb /= maxi;
            return new Color((int)(r + tr), (int)(g + tg), (int)(b + tb));
        }
        /// <summary>
		/// 线性插值
		/// </summary>
        public static double GetCloserSingle(double current, double target, double i, double maxi)
        {
            current *= (maxi - i) / maxi;
            target *= i / maxi;
            return current + target;
        }
        /// <summary>
		/// 保存一个xml文件
		/// </summary>
        public static void WriteXML(string Values, string Name)
        {
            string XMLPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + Name + ".xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc = new XmlDocument();
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            XmlNode root = xmlDoc.CreateElement("Parent");
            xmlDoc.AppendChild(root);
            node = xmlDoc.CreateNode(XmlNodeType.Element, "Child", null);
            node.InnerText = Values;
            root.AppendChild(node);
            xmlDoc.Save(XMLPath);
        }
        public static string ReadXML(string Name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string XMLPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + Name + ".xml";
            if (File.Exists(XMLPath))
            {
                xmlDoc.Load(XMLPath);
            }
            XmlNode Values = xmlDoc.SelectSingleNode("Parent").SelectSingleNode("Child");
            return Values.InnerText;
        }
        /// <summary>
		/// 用于彩蛋
		/// </summary>
        public static string Name2Specialname(string name)
        {
            switch (name)
            {
                case "Sym":
                    break;
                case "Sym_":
                    name = "Sym";
                    break;
                case "理奈":
                    name = "Rina";
                    break;
                case "理奈_":
                    name = "Rina";
                    break;
                case "真宣理奈":
                    name = "Rina";
                    break;
                case "Cyril":
                    break;
                case "-Cyril-":
                    name = "Cryil";
                    break;
                case "努力的向阳花":
                    name = "Sunflower";
                    break;
                case "向阳花":
                    name = "Sunflower";
                    break;
                case "浪花朵朵":
                    name = "WaveFlower";
                    break;
                case "不考了100":
                    name = "Bukaole100";
                    break;
                case "":
                    name = "";
                    break;
                default:
                    name = "none";
                    break;

            }
            return name;
        }
        /// <summary>
		/// 用于彩蛋
		/// </summary>
        public static Color Specialname2Color(string name)
        {
            switch (name)
            {
                case "Sym":
                    return new Color(195, 235, 255, 255);
                case "Rina":
                    return new Color(64, 170, 255, 255);
                case "WaveFlower":
                    return new Color(0, 0, 0, 255);
                case "Sunflower":
                    return new Color(0, 183, 238, 255);
                case "Bukaole1000":
                    return new Color(0, 0, 0);
                case "Cyril":
                    return new Color(255, 165, 22, 255);
                case "":
                    return new Color(0, 0, 0);
                default:
                    return Color.White;
            }
        }
        /// <summary>
		/// 初始化
		/// </summary>
        public static void Initialize(Player player)
        {

        }
        /// <summary>
		/// 线性的彩虹色
		/// </summary>
        public static Color GetRainbowColorLinear(int i, int imax)
        {
            float a = imax / 7;
            if (i <= a)
            {
                return GetCloserColor(Color.Red, Color.Orange, i, a);
            }
            if (i <= 2 * a && i > a)
            {
                return GetCloserColor(Color.Orange, Color.Yellow, i - a, a);
            }
            if (i <= 3 * a && i > 2 * a)
            {
                return GetCloserColor(Color.Yellow, Color.Green, i - 2 * a, a);
            }
            if (i <= 4 * a && i > 3 * a)
            {
                return GetCloserColor(Color.Green, Color.Cyan, i - 3 * a, a);
            }
            if (i <= 5 * a && i > 4 * a)
            {
                return GetCloserColor(Color.Cyan, Color.Blue, i - 4 * a, a);
            }
            if (i <= 6 * a && i > 5 * a)
            {
                return GetCloserColor(Color.Blue, Color.Purple, i - 5 * a, a);
            }
            if (i > 6 * a)
            {
                return GetCloserColor(Color.Purple, Color.Red, i - 6 * a, a);
            }
            return Color.White;
        }
        public static Color ToGreyColor(Color color)
        {
            int a = color.R + color.G + color.B;
            a /= 3;
            return new Color(a, a, a);
        }
        public static Color SFCtypeToColor(int a)
        {
            switch (a)
            {
                case -1:
                    return RevolutionsPlayer.customStarFlareColor;
                case 0:
                    return new Color(246, 247, 150);
                case 1:
                    return new Color(240, 127, 225);
                case 2:
                    return new Color(127, 240, 232);
                case 3:
                    return new Color(181, 127, 240);
                default:
                    return Color.White;
            }
        }
        public static Vector2 RelativePositionTrans(Vector2 current, Vector2 target)
        {
            return new Vector2(current.X + target.X, current.Y + target.Y);
        }
        /// <summary>
		/// 将指定向量转化为单位向量，只用写一行
		/// </summary>
        public static Vector2 ToUnitVector(Vector2 vector)
        {
            return vector / vector.Length();
        }
        /// <summary>
		/// 将指定向量转化为单位向量，只用写一行
		/// </summary>
        public static Vector2 ToUnitVector(float x, float y)
        {
            return new Vector2(x, y) / new Vector2(x, y).Length();
        }
        /// <summary>
		/// 返回c加上数组a中0到b项的值
		/// </summary>
        public static float HyperPlus(float[] a, int b, float c)
        {
            for (int d = 0; d <= b; d++)
            {
                c += a[d];
            }
            return c;
        }
        /// <summary>
		/// 返回c加上数组a中0到b项的值
		/// </summary>
        public static Vector2 HyperPlus(Vector2[] a, int b, Vector2 c)
        {
            for (int d = 0; d <= b; d++)
            {
                c += a[d];
            }
            return c;
        }
        /// <summary>
		/// 生命偷取
		/// </summary>
        public static void LifeSteal(Player player, int damage, float ratio)
        {
            damage = (int)(damage * ratio);
            if (damage == 0) damage = 1;
            if (damage + player.statLife >= player.statLifeMax2 && player.statLifeMax2 >= player.statLife)
            {
                damage = player.statLifeMax2 - player.statLife;
            }
            if (damage != 0)
            {
                player.statLife += damage;
                player.HealEffect(damage);
            }
        }
        public static Point ToTilesPos(Vector2 position)
        {
            return new Point((int)(position.X / 16), (int)(position.Y / 16));
        }
        public static float GetStringLength(DynamicSpriteFont font, string text, float scale)
        {
            return font.MeasureString(text).X * scale;
        }
        public static float GetStringHeight(DynamicSpriteFont font, string text, float scale)
        {
            return font.MeasureString(text).Y * scale;
        }
        public static Vector2 GetStringSize(DynamicSpriteFont font, string text, float scale)
        {
            return font.MeasureString(text) * scale;
        }
        public static bool CanShowExtraUI()
        {
            return !Main.mapFullscreen && !Main.ingameOptionsWindow && !Main.inFancyUI && !Main.playerInventory;
        }
        public static void GetSetting()
        {
            try
            {
                string filePath = Main.SavePath + @"\revosets";
                StreamReader streamReader = new StreamReader(filePath);
                bool[] array = new bool[11];
                for (int i = 0; i < 11; i++)
                {
                    array[i] = bool.Parse(streamReader.ReadLine());
                }
                if (array[0]) Revolutions.Settings.rangeIndex = 0;
                if (array[1]) Revolutions.Settings.rangeIndex = 1;
                if (array[2]) Revolutions.Settings.rangeIndex = 2;
                Revolutions.Settings.dist = array[3];
                Revolutions.Settings.blur = array[4];
                Revolutions.Settings.autodoor = array[5];
                Revolutions.Settings.mutter = array[6];
                Revolutions.Settings.autoreuse = array[7];
                Revolutions.Settings.hthbar = array[8];
                Revolutions.Settings.spcolor = array[9];
                Revolutions.Settings.extraAI = array[10];
            }
            catch
            {

            }
        }
        public static void SaveSettings()
        {
            bool[] array = new bool[11];
            array[0] = false;
            array[1] = false;
            array[2] = false;
            array[Revolutions.Settings.rangeIndex] = true;
            array[3] = Revolutions.Settings.dist;
            array[4] = Revolutions.Settings.blur;
            array[5] = Revolutions.Settings.autodoor;
            array[6] = Revolutions.Settings.mutter;
            array[7] = Revolutions.Settings.autoreuse;
            array[8] = Revolutions.Settings.hthbar;
            array[9] = Revolutions.Settings.spcolor;
            array[10] = Revolutions.Settings.extraAI;
            string filePath = Main.SavePath + @"\revosets";
            StreamWriter streamWriter = new StreamWriter(filePath, false);
            for (int i = 0; i < 11; i++)
            {
                streamWriter.WriteLine(array[i].ToString());
            }
            streamWriter.Close();
            streamWriter.Dispose();
        }
        public static bool InTheRange(Vector2 current, Vector2 range, Vector2 target)
        {
            if (range.X < 0) return false;
            if (range.Y < 0) return false;
            if (current.X > target.X) return false;
            if (current.X + range.X < target.X) return false;
            if (current.Y > target.Y) return false;
            if (current.Y + range.Y < target.Y) return false;
            return true;
        }
    }
}
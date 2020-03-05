using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.UI;
using Revolutions.Utils;
//using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions
{
    public class Revolutions : Mod
    {
        public static Mod mod;
        public static ModHotKey TimeTravelingPotion;
        public static bool TTPHP;
        public Revolutions()
        {
        }
        public override void Load()
        {
            mod = this;
            TimeTravelingPotion = RegisterHotKey("Time Traveling Potion", "Q");
            //Helper.EntroptPool = new int[65537];
            Helper.EntroptPool = new int[10001];
            for (int i = 0; i < 10000; i++)
            {
                Random rd = new Random(i * i * i - i * i);
                Helper.EntroptPool[i] = rd.Next(-101, 101);
            }
            Helper.WriteXML("12345", "12345");
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect")); // The path to the compiled shader file.
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();
                Ref<Effect> screenRef2 = new Ref<Effect>(GetEffect("Effects/Blur"));
                Filters.Scene["Blur"] = new Filter(new ScreenShaderData(screenRef2, "Blur"), EffectPriority.VeryHigh);
                Filters.Scene["Blur"].Load();
            }

        }
        public override void Unload()
        {
            Helper.EntroptPool = new int[0];
            TimeTravelingPotion = null;
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            if (Main.gamePaused && !Filters.Scene["Blur"].IsActive()) Filters.Scene.Activate("Blur", Vector2.Zero);
            if (Filters.Scene["Blur"].IsActive() && !Main.gamePaused) Filters.Scene["Blur"].Deactivate();

            if (RevolutionsPlayer.logoTimer > 0) RevolutionsPlayer.logoTimer--;
            FirstUI firstUI = new FirstUI();
            firstUI.Draw(spriteBatch);
            SecondUI secondUI = new SecondUI();
            secondUI.Draw(spriteBatch);
        }
        public override void HotKeyPressed(string name)
        {

        }

    }
}


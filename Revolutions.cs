using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.NPCs;
using Revolutions.UI;
using Revolutions.Utils;
//using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Revolutions
{
    public class Revolutions : Mod
    {
        public static Mod mod;
        public static ModHotKey TimeTravelingPotion;
        public static bool TTPHP;
        public UserInterface AlphaUI;
        public UserInterface BetaUI;
        static SecondUI secondUI = new SecondUI();

        public static class Settings
        {
            public static int rangeIndex = 0;
            public static bool dist = false;
            public static bool blur = true;
            public static bool autodoor = true;
            public static bool mutter = true;
            public static bool autoreuse = false;
            public static bool hthbar = true;
            public static bool spcolor = true;
        }
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
                Ref<Effect> screenRef3 = new Ref<Effect>(GetEffect("Effects/Filter"));
                Filters.Scene["Filter"] = new Filter(new ScreenShaderData(screenRef3, "Filter0"), EffectPriority.VeryHigh);
                Filters.Scene["Filter"].Load();
                Ref<Effect> screenRef4 = new Ref<Effect>(GetEffect("Effects/ExtraEfts"));
                Filters.Scene["Extra"] = new Filter(new ScreenShaderData(screenRef4, "LikePE"), EffectPriority.VeryHigh);
                Filters.Scene["Extra"].Load();
                FirstUI firstUI = new FirstUI();
                firstUI.Activate();
                AlphaUI = new UserInterface();
                AlphaUI.SetState(firstUI);

            }
            Main.OnPostDraw += new Action<GameTime>(Welcome);
            Main.OnPostDraw += new Action<GameTime>(DrawCircle);
        }
        public override void Unload()
        {
            Main.OnPostDraw -= new Action<GameTime>(Welcome);
            Main.OnPostDraw -= new Action<GameTime>(DrawCircle);
            Helper.EntroptPool = new int[0];
            TimeTravelingPotion = null;
        }
        int a = 0;
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            if (Settings.blur && Main.gamePaused && !Filters.Scene["Blur"].IsActive()) Filters.Scene.Activate("Blur", Vector2.Zero);
            if (Filters.Scene["Blur"].IsActive() && !Main.gamePaused) Filters.Scene["Blur"].Deactivate();
            if (!Filters.Scene["Filter"].IsActive() && Settings.dist) Filters.Scene.Activate("Filter", Vector2.Zero).GetShader().UseColor(1f, 1f, 1f);
            if (Filters.Scene["Filter"].IsActive() && !Settings.dist) Filters.Scene["Filter"].Deactivate();
            if (RevolutionsPlayer.logoTimer >= 30 && !Filters.Scene["Extra"].IsActive()) Filters.Scene.Activate("Extra", Vector2.Zero);
            if (RevolutionsPlayer.logoTimer < 30 && Filters.Scene["Extra"].IsActive()) Filters.Scene["Extra"].Deactivate();
            if (Filters.Scene["Extra"].IsActive()) Filters.Scene["Extra"].GetShader().UseProgress(-((float)RevolutionsPlayer.logoTimer - 30) * ((float)RevolutionsPlayer.logoTimer - 30) / 7200 + 1f);
            if (RevolutionsPlayer.logoTimer > 0) RevolutionsPlayer.logoTimer--;
            if (Settings.hthbar)
            {
                secondUI = new SecondUI();
                secondUI.Draw(spriteBatch);
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            AlphaUI.Update(gameTime);
            ThirdUI thirdUI = new ThirdUI();
            thirdUI.Activate();
            if (Main.ingameOptionsWindow)
            {
                Main.InGameUI.SetState(thirdUI);
                //IngameOptions.Close();
                Main.inFancyUI = true;
            }
        }
        public override void MidUpdatePlayerNPC()
        {
            
        }
        public override void PreSaveAndQuit()
        {
            Main.InGameUI = new UserInterface();
            Main.inFancyUI = false;
            Helper.SaveSettings();
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Revolutions: AlphaUI",
                    delegate
                    {
                        AlphaUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public override void HotKeyPressed(string name)
        {

        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }
            /*if (Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().nowBoss != null && Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().nowBoss.type == NPCID.DukeFishron)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/battletower");
                priority = MusicPriority.BossLow;
            }*/

        }
        private static void Welcome(object obj)
        {
            if (RevolutionsPlayer.logoTimer >= 0 && Helper.CanShowExtraUI())
            {
                Texture2D Logo = Revolutions.mod.GetTexture("UI/Revolutions");
                Main.spriteBatch.Begin();
                float scale = 0.3f * (float)Math.Cos(0.0174533 * (90 - RevolutionsPlayer.logoTimer)) + 0.7f;
                Main.spriteBatch.Draw(Logo, new Vector2(0.5f * Main.screenWidth - 0.25f * scale * Main.UIScale * Logo.Width, 135f + 45f * (float)Math.Cos(0.0174533 * (RevolutionsPlayer.logoTimer))), new Rectangle(0, 0, (int)(Logo.Width), Logo.Height), Color.White * (float)Math.Cos(0.0174533 * (90 - RevolutionsPlayer.logoTimer)), 0f, Vector2.Zero, 0.5f * Main.UIScale * scale, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
            }
        }
        float timer = 0;


        private void DrawCircle(object obj)
        {
            if (RevolutionsPlayer.drawcircler > 0 && Helper.CanShowExtraUI() && Settings.rangeIndex != 2)
            {
                timer += 0.01f;
                float theta = 6.283f;
                theta /= RevolutionsPlayer.drawcircler * 1.6f * Main.GameZoomTarget;
                Vector2 drawOrigin = new Vector2(1, 1);
                SpriteBatch spriteBatch = Main.spriteBatch;
                spriteBatch.Begin();
                for (int i = 0; i < RevolutionsPlayer.drawcircler * 1.6f * Main.GameZoomTarget; i++)
                {
                    Color color = Color.White;
                    if (RevolutionsPlayer.drawcircletype == 0)
                    {
                        if (Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().spname == "none")
                        {
                            color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(i, (int)(RevolutionsPlayer.drawcircler * 1.6f * Main.GameZoomTarget)), Color.White, 5, 6);
                        }
                        else
                        {
                            color = Helper.Specialname2Color(Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().spname);
                        }
                    }
                    if (RevolutionsPlayer.drawcircletype == 1) color = Helper.GetCloserColor(new Color(126, 171, 243), color, 1, 2);
                    color *= (float)Math.Abs(Math.Sin(theta * i + timer));
                    Vector2 drawPos = Main.player[0].Center + new Vector2((float)Math.Cos(theta * i) * RevolutionsPlayer.drawcircler * Main.GameZoomTarget, (float)Math.Sin(theta * i) * RevolutionsPlayer.drawcircler * Main.GameZoomTarget) - Main.screenPosition;
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Projectiles.RareWeapon.MeteowerHelper>()],
                        drawPos, null, color, 0f, drawOrigin, 0.19f, SpriteEffects.None, 0f);
                }
                spriteBatch.End();
            }
        }
    }
}


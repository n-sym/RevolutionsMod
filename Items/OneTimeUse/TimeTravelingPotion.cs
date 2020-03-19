using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.OneTimeUse
{
    public class TimeTravelingPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Go back to 2 seconds before\n4 seconds cooldown\nDefault hotkey : Q");
        }
        public override void SetDefaults()
        {
            item.maxStack = 120;
            item.width = 40;
            item.useTurn = false;
            item.consumable = true;
            item.height = 20;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 12;
            item.autoReuse = false;
        }
        int timer = 0;
        public override bool CanUseItem(Player player)
        {
            if (timer != 0) return false;
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (timer == 0)
            {
                for (int i = 0; i < 71; i++)
                {
                    Dust d = Dust.NewDustDirect(player.position, player.height, player.width, MyDustId.BlueParticle, 0, 0, 100, Color.White, 0.8f);
                    d.noGravity = true;
                    d.velocity *= 2;
                }
                player.Teleport(player.GetModPlayer<RevolutionsPlayer>().pastPosition[120], 3);
                foreach(Projectile projectile in Main.projectile)
                {
                    if (Vector2.Distance(projectile.Center, player.GetModPlayer<RevolutionsPlayer>().pastCenter[120]) < 300f) projectile.Kill();
                }
                Main.PlaySound(SoundID.Item6, player.position);
                for (int i = 0; i < 71; i++)
                {
                    Dust d = Dust.NewDustDirect(player.position, player.height, player.width, MyDustId.BlueParticle, 0, 0, 100, Color.White, 0.8f);
                    d.noGravity = true;
                    d.velocity *= 2;
                }
                if (player.statLife > player.GetModPlayer<RevolutionsPlayer>().pastLife[120])
                {
                    CombatText.NewText(player.getRect(), CombatText.DamagedHostile, player.statLife - player.GetModPlayer<RevolutionsPlayer>().pastLife[120]);
                }
                if (player.statLife < player.GetModPlayer<RevolutionsPlayer>().pastLife[120])
                {
                    player.HealEffect(player.GetModPlayer<RevolutionsPlayer>().pastLife[120] - player.statLife);
                }
                if (player.statMana < player.GetModPlayer<RevolutionsPlayer>().pastMana[120])
                {
                    player.ManaEffect(player.GetModPlayer<RevolutionsPlayer>().pastMana[120] - player.statMana);
                }
                player.statLife = player.GetModPlayer<RevolutionsPlayer>().pastLife[120];
                player.statMana = player.GetModPlayer<RevolutionsPlayer>().pastMana[120];
                timer = 240;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void UpdateInventory(Player player)
        {
            if (timer > 0) timer--;
            if (Revolutions.TimeTravelingPotion.JustPressed && timer == 0)
            {
                UseItem(player);
                item.stack -= 1;
                Main.PlaySound(SoundID.Item6, player.position);
            }
        }

    }
}

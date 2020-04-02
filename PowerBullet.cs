using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Revolutions
{
    public abstract class PowerBullet
    {
        public int timeLeft;
        public float damage;
        public int critChance;
        public Entity target;
        public Entity owner;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 size;
        public Rectangle hitRec;
        public int takeImmunityFrame;
        public int[] extra;
        protected Vector2[] oldPosition;
        public bool active;
        public bool useSpeed;
        public bool canTakeDamage;
        protected bool fromPlayer;
        public Vector2 center
        {
            get => position + 0.5f * size;
            set => position = value - 0.5f * size;
        }
        public PowerBullet()
        {
            takeImmunityFrame = 10;
            oldPosition = new Vector2[21];
            extra = new int[5];
            active = true;
        }
        public void Active()
        {
            if (timeLeft > 0)
            {
                position += velocity;
                hitRec = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
                AI();
                if (owner is Player)
                {
                    foreach (NPC npc in Main.npc)
                    {
                        if (hitRec.Intersects(npc.Hitbox) && !npc.friendly && !npc.dontTakeDamage) HitNPC(npc);
                    }
                } 
                else if (owner is NPC)
                {
                    foreach (Player player in Main.player)
                    {
                        if (player == null) continue;
                        if (hitRec.Intersects(player.Hitbox)) HitPlayer(player);
                    }
                }
                timeLeft--;
                for (int j = 20; j > 0; j--)
                {
                    oldPosition[j] = oldPosition[j - 1];
                }
                oldPosition[0] = position;
            }
            else
            {
                active = false;
            }
        }
        public void HitPlayer(Player player)
        {
            if (PreHitPlayer(player))
            {
                if (owner is NPC && !player.immune)
                {
                    NPC o = (NPC)owner;
                    player.Hurt(PlayerDeathReason.ByNPC(o.type), (int)damage, 0);
                    player.immuneTime += takeImmunityFrame;
                }
            }
            PostHitPlayer(player);
        }
        public void HitNPC(NPC npc)
        {
            if (PreHitNPC(npc))
            {
                ((Player)owner).ApplyDamageToNPC(npc, (int)Math.Floor(damage), 0, 0, Main.rand.Next(100) < critChance);
                ((Player)owner).addDPS((int)Math.Floor(damage));
            }
            PostHitNPC(npc);
        }
        public virtual void AI(){ }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual bool PreHitPlayer(Player player) { return true; }
        public virtual void PostHitPlayer(Player player) { }
        public virtual bool PreHitNPC(NPC npc) { return true; }
        public virtual void PostHitNPC(NPC npc) { }

        public float Distance(Entity entity)
        {
            return entity.Distance(center);
        }
        public float Distance(PowerBullet powerBullet)
        {
            return Vector2.Distance(center, powerBullet.center);
        }
        public static PowerBullet NewBullet(Type bulletType, float damage, Vector2 pos, Vector2 spd, Entity owner)
        {
            PowerBullet powerBullet = (PowerBullet)Activator.CreateInstance(bulletType);
            powerBullet.damage = damage;
            powerBullet.position = pos;
            powerBullet.velocity = spd;
            powerBullet.owner = owner;
            Revolutions.powerBullets.Add(powerBullet);
            return powerBullet;
        }
    }
}


﻿using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bosspocalyps.Tiles.Warped;
using Bosspocalyps.Projectiles;

namespace Bosspocalyps
{
    public partial class BosspocalypsModPlayer : ModPlayer
    {
        public bool RedashHood, FrostiteEffect;
        public bool ZoneWarpedBiome;
        public int HealingAbyssKnivesCooldown;
        public LinkedList<int> SurroundingTEProjs = new LinkedList<int>();
        public int TTBTEPCTimer;
        public float TTBTEPCRotation;

        public override void ResetEffects()
        {
            ResetBuffs();
            ResetInsignias();
            RedashHood = false;
            FrostiteEffect = false;
            IceColdPotion = false;
        }

        public override void UpdateLifeRegen()
        {
            InsigniaLifeRegen();
        }

        public override void PreUpdate()
        {
            if(HealingAbyssKnivesCooldown > 0)
            {
                HealingAbyssKnivesCooldown--;
            }


            UpdateTEProjs();

            TTBTEPCRotation += 0.2f;
        }

        private void UpdateTEProjs()
        {
            LinkedListNode<int> f = SurroundingTEProjs.First;
            int d = 0;
            while (f != null)
            {
                Projectile proj = Main.projectile[f.Value];
                if (!proj.active || proj.type != ProjectileID.LightBeam || proj.owner != player.whoAmI)
                    SurroundingTEProjs.Remove(f);
                proj.ai[0] = d++;
                f = f.Next;
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            InsigniaDamageReduction(ref damage);
            return true;
        }

        public override float MeleeSpeedMultiplier(Item item)
        {
            float speedMultiplier = 1f;
            InsigniaMeleeSpeedMultiplier(ref speedMultiplier);
            return speedMultiplier;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (FrostiteEffect || (IceColdPotion && (item.ranged || item.melee)))
                target.AddBuff(BuffID.Frostburn, 120);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (FrostiteEffect || (IceColdPotion && (proj.ranged || proj.melee)))
                target.AddBuff(BuffID.Frostburn, 120);
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (RedashHood)
            {
                speedX *= 1.01f;
                speedY *= 1.01f;
            }
            return true;
        }

        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneWarpedBiome)
                return mod.GetTexture("Backgrounds/Warped_Biom");
            return null;
        }

        public override void UpdateBiomes()
        {
            ZoneWarpedBiome = BWorld.WarpedTiles > 50;
        }

        public override bool CustomBiomesMatch(Player other)
        {
            var modp = other.GetModPlayer<BosspocalypsModPlayer>();
            return ZoneWarpedBiome == modp.ZoneWarpedBiome;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            var modp = other.GetModPlayer<BosspocalypsModPlayer>();
            modp.ZoneWarpedBiome = ZoneWarpedBiome;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            writer.Write(new BitsByte(ZoneWarpedBiome));
            //writer.Write(Helpers.FlagsByte(ZoneWarpedBiome));
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            ((BitsByte)reader.ReadByte()).Retrieve(ref ZoneWarpedBiome);
            //Helpers.RetrieveFlagsByte(reader.ReadByte(), ref ZoneWarpedBiome);
        }
    }
}

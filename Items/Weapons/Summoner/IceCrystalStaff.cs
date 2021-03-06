﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bosspocalyps.Projectiles;
using Bosspocalyps.Items.Materials;
using Bosspocalyps.Buffs.Minions;

namespace Bosspocalyps.Items.Weapons.Summoner
{
    public class IceCrystalStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Crystal Staff");
        }

        public override void SetDefaults()
        {
            item.summon = true;
            item.damage = 14;
            item.mana = 10;
            item.width = 36;
            item.height = 38;
            item.useTime = 20;
            item.useAnimation = 20;
            item.buffType = ModContent.BuffType<IceCrystalBuff>();
            item.buffTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ModContent.ProjectileType<IceCrystalStaffProjectile>();
            item.shootSpeed = 1;
        }

        public override bool UseItem(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld, default, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient<FrostiteBar>(30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

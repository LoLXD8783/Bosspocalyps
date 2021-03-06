﻿using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Bosspocalyps.Items.Materials;

namespace Bosspocalyps.Items
{
    // TODO: Boss
    public class TheUnholyScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 22));
            DisplayName.SetDefault("The Unholy Scroll");
            Tooltip.SetDefault("It's pulsating with dark energy");
        }

        public override void SetDefaults()
        {
            item.width = 100;
            item.height = 100;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient<Scroll>();
            recipe.AddIngredient<UnholyBlood>(10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

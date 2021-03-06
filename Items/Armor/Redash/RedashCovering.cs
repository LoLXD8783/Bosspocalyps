﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Bosspocalyps.Items.Materials;

namespace Bosspocalyps.Items.Armor.Redash
{
    [AutoloadEquip(EquipType.Head)]
    public class RedashCovering : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Redash covering");
            Tooltip.SetDefault("Boosts max minion slots by 2");
        }

        public override void SetDefaults()
        {
            item.defense = 14;
            item.width = 24;
            item.height = 32;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<RedashChestpiece>() && legs.type == ModContent.ItemType<RedashLeggings>();

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient<RedashBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

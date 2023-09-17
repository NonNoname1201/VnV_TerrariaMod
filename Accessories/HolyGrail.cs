using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TLoh.Accessories;
public class HolyGrail : ModItem
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Holy Grail");
            Tooltip.SetDefault("Makes you feel a fear of god");
        }

        public override void SetDefaults() 
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 1f;
            player.accRunSpeed += 0.5f;
            player.endurance += 0.5f;
            player.statDefense += 10;
            player.lifeRegen += 5;
            player.luck += 5;
        }

        public override void AddRecipes() 
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SteampunkCup);
            recipe.AddIngredient(ItemID.CrossNecklace);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
}
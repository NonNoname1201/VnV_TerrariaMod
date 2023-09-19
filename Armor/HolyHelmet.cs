using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TLoh.Armor;

[AutoloadEquip(EquipType.Head)]
public class HolyHelmet : ModItem
{
    public override void SetStaticDefaults() 
    {
        DisplayName.SetDefault("Holy Helmet");
        Tooltip.SetDefault("Forged in god's image");
    }

    public override void SetDefaults() 
    {
        Item.width = 30;
        Item.height = 30;
        Item.value = Item.buyPrice(gold: 10);
        Item.rare = 8;
        Item.headSlot = 0;
    }

    public override void UpdateEquip(Player player)
    {
        player.endurance += 0.5f;
        player.statDefense += 10;
        player.lifeRegen += 5;
        player.luck += 5;
    }

    public override void AddRecipes() 
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SilverHelmet);
        recipe.AddIngredient(ItemID.LunarBar, 5);
        recipe.AddIngredient(ItemID.HallowedBar, 20);
        recipe.AddIngredient(ItemID.GoldBar, 20);
        recipe.AddIngredient(ItemID.SoulofLight, 10);
            
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}
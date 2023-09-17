using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TLoh.Items;

public class BaneOfTioska : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault(
            "Bane Of Tioska"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        Tooltip.SetDefault("You hear strange laughing scream as someone is calling help...\n\"Tioska-a-a!!!\"");
    }

    public override void SetDefaults()
    {
        var projectile = Mod.Find<ModProjectile>("BaneProjectile").Type;
        Item.damage = 200;
        Item.DamageType = DamageClass.Melee;
        Item.shoot = projectile;
        Item.shootSpeed = 100f;
        Item.crit = 20;
        Item.knockBack = 100;

        Item.width = 100;
        Item.height = 100;

        Item.useTime = 11;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;

        Item.autoReuse = true;
        Item.useTurn = true;

        Item.value = Item.buyPrice(0, 20, 0, 0);
        Item.rare = ItemRarityID.Red;
    }

    //make three projectiles shoot out in a cone
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        var projectile = Mod.Find<ModProjectile>("BaneProjectile").Type;
        float numberProjectiles = 5;
        var rotation = MathHelper.ToRadians(5 * numberProjectiles);
        position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
        for (var i = 0; i < numberProjectiles; i++)
        {
            var perturbedSpeed =
                new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation,
                    i / (numberProjectiles - 1))) * .2f;
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, projectile,
                damage, knockback, player.whoAmI);
        }

        return false;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddIngredient(ItemID.GuideVoodooDoll);
        recipe.AddIngredient(ItemID.LunarBar, 5);
        recipe.AddIngredient(ItemID.FragmentSolar, 10);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();

        var altRecipe = CreateRecipe();
        altRecipe.AddIngredient(ItemID.TerraBlade);
        altRecipe.AddIngredient(ItemID.GuideVoodooDoll);
        altRecipe.AddTile(TileID.Campfire);
        altRecipe.Register();
    }
}
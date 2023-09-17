using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TLoh.Projectiles;

public class BaneProjectile : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault(
            "Bane Projectile"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
    }

    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.width = 36;
        Projectile.height = 100;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 600;
        Projectile.light = 1.25f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 0;
    }

    public override void AI()
    {
        var dust = Dust.NewDust(Projectile.Center, 1, 1, 15, 0f, 0f, 0, new Color(128, 200, 200));
        Main.dust[dust].velocity *= 0.3f;
        Main.dust[dust].scale = Main.rand.Next(100, 135) * 0.013f;
        Main.dust[dust].noGravity = true;

        var dust2 = Dust.NewDust(Projectile.Center, 1, 1, 137);
        Main.dust[dust2].velocity *= 0.3f;
        Main.dust[dust2].scale = Main.rand.Next(80, 115) * 0.013f;
        Main.dust[dust2].noGravity = true;

        for (var i = 0; i < 20; i++)
        {
            var target = Main.npc[i];
            if (!target.friendly)
            {
                //Get the shoot trajectory from the projectile and target
                var shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                var shootToY = target.position.Y - Projectile.Center.Y;
                var distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                //If the distance between the live targeted npc and the projectile is less than 480 pixels
                if (distance < 480f && !target.friendly && target.active)
                {
                    //Divide the factor, 3f, which is the desired velocity
                    distance = 3f / distance;

                    //Multiply the distance by a multiplier if you wish the projectile to have go faster
                    shootToX *= distance * 5;
                    shootToY *= distance * 5;

                    //Set the velocities to the shoot values
                    Projectile.velocity.X = shootToX;
                    Projectile.velocity.Y = shootToY;
                }
            }
        }
    }
}
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TLoh.Projectiles;

public class BaneProjectileSecondary : ModProjectile
{
    int lifetime = 0;
    private float HomingCooldown
    {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault(
            "Bane Anger"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
    }

    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.width = 18;
        Projectile.height = 50;
        Projectile.aiStyle = 1;
        Projectile.friendly = false;
        Projectile.hostile = false;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 120;
        Projectile.light = 0.25f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 0;
    }
    

    public override void AI()
    {
        var dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.MagicMirror, 0f, 0f, 0, new Color(200, 200, 200));
        Main.dust[dust].velocity *= 0.3f;
        Main.dust[dust].scale = Main.rand.Next(80, 100) * 0.013f;
        Main.dust[dust].noGravity = true;



        if (lifetime > 30)
        {
            Projectile.friendly = true;
            var homingCooldown = HomingCooldown;
            HomingCooldown = homingCooldown + 1f;
            if (HomingCooldown > 10f)
            {
                HomingCooldown = 10f;
                var foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    var i = Main.npc[foundTarget];
                    if (Projectile.Distance(i.Center) > Math.Max(i.width, i.height))
                    {
                        var desiredVelocity = Projectile.DirectionTo(i.Center) * 60f;
                        Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 0.05f);
                        double num4 = (i.Center - Projectile.Center).ToRotation() - Projectile.velocity.ToRotation();
                        if (num4 > Math.PI) num4 -= 2 * Math.PI;
                        if (num4 < -Math.PI) num4 += 2 * Math.PI;
                        Projectile.velocity =
                            Projectile.velocity.RotatedBy(num4 * (Projectile.Distance(i.Center) > 100f ? 0.4f : 0.1f));
                    }
                }
            }
        }
        else
        {
            lifetime++;
        }
        

        if (Projectile.velocity.Length() < 2f) Projectile.velocity *= 1.03f;
        Projectile.rotation = Projectile.velocity.ToRotation() + 1.570796f; // (float) Math.PI / 2f;
        Projectile.localAI[0] += 0.1f;
        if (Projectile.localAI[0] > ProjectileID.Sets.TrailCacheLength[Projectile.type])
            Projectile.localAI[0] = ProjectileID.Sets.TrailCacheLength[Projectile.type];
        Projectile.localAI[1] += 0.25f;
    }

    private int HomeOnTarget()
    {
        var selectedTarget = -1;
        for (var i = 0; i < 200; i++)
        {
            var j = Main.npc[i];
            if (j.CanBeChasedBy(Projectile))
            {
                var distance = Projectile.Distance(j.Center);
                if (distance <= 300f && (selectedTarget == -1 ||
                                          Projectile.Distance(Main.npc[selectedTarget].Center) > distance))
                    selectedTarget = i;
            }
        }
        return selectedTarget;
    }
}
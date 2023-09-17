using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;
using TLoh.Buffs;
using TLoh.Items;
using TLoh.Projectiles;

namespace TLoh.TownNPCs;

[AutoloadHead]
public class Cleric : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cleric");
    }

    public override void SetDefaults()
    {
        NPC.townNPC = true;
        NPC.friendly = true;
        NPC.width = 20;
        NPC.height = 45;
        NPC.aiStyle = 7;
        NPC.defense = 100;
        NPC.lifeMax = 600;
        NPC.HitSound = SoundID.PlayerHit;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = -1f;
        Main.npcFrameCount[NPC.type] = 23;
        NPCID.Sets.ExtraFramesCount[NPC.type] = 0;
        NPCID.Sets.AttackFrameCount[NPC.type] = 1;
        NPCID.Sets.DangerDetectRange[NPC.type] = 500;
        NPCID.Sets.AttackType[NPC.type] = 1;
        NPCID.Sets.AttackTime[NPC.type] = 10;
        NPCID.Sets.AttackAverageChance[NPC.type] = 10;
        NPCID.Sets.HatOffsetY[NPC.type] = 17;
        AnimationType = 18;
        NPC.Happiness.SetBiomeAffection(new HallowBiome(), AffectionLevel.Love);
        NPC.Happiness.SetBiomeAffection(new ForestBiome(), AffectionLevel.Like);
        NPC.Happiness.SetBiomeAffection(new UndergroundBiome(), AffectionLevel.Dislike);
        NPC.Happiness.SetBiomeAffection(new CrimsonBiome(), AffectionLevel.Hate);
        NPC.Happiness.SetBiomeAffection(new CorruptionBiome(), AffectionLevel.Hate);
        NPC.Happiness.SetNPCAffection(18, AffectionLevel.Love);
        NPC.Happiness.SetNPCAffection(19, AffectionLevel.Like);
        NPC.Happiness.SetNPCAffection(20, AffectionLevel.Dislike);
        NPC.Happiness.SetNPCAffection(22, AffectionLevel.Hate);
    }

    public override bool CanTownNPCSpawn(int numTownNpCs, int money)
    {
        if (NPC.downedSlimeKing)
            for (var i = 0; i < 255; i++)
            {
                var player = Main.player[i];
                foreach (var item in player.inventory)
                    if (item.type == ItemID.HealingPotion)
                        return true;
            }

        return false;
    }

    public override List<string> SetNPCNameList()
    {
        return new List<string>
        {
            "Emeric",
            "Cassini",
            "Lucius",
            "Octavian",
            "Theron"
        };
    }

    public override void SetChatButtons(ref string button, ref string button2)
    {
        button = "Shop";
        button2 = "Spare sins";
    }

    public override void OnChatButtonClicked(bool firstButton, ref bool shop)
    {
        if (firstButton)
        {
            shop = true;
        }
        else
        {
            for (var index = 0; index < Player.MaxBuffs; ++index)
                if (Main.player[Main.myPlayer].buffType[index] == ModContent.BuffType<Blessing>())
                {
                    Main.npcChatText = "Thou art already blessed.";
                    return;
                }

            Main.npcChatText = "Thy sins are forgiven. May God have mercy on thy soul.";
            Main.player[Main.myPlayer].AddBuff(ModContent.BuffType<Blessing>(), 3600000);
        }
    }

    public override void SetupShop(Chest shop, ref int nextSlot)
    {
        //Holy
        shop.item[nextSlot].SetDefaults(ItemID.PurificationPowder, false);
        shop.item[nextSlot].value = 1;
        nextSlot++;

        shop.item[nextSlot].SetDefaults(ItemID.Pearlwood, false);
        shop.item[nextSlot].value = 1;
        nextSlot++;

        if (Main.hardMode)
        {
            shop.item[nextSlot].SetDefaults(ItemID.HolyArrow, false);
            shop.item[nextSlot].value = Item.buyPrice(0, 0, 1);

            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.HolyWater, false);
            shop.item[nextSlot].value = Item.buyPrice(0, 0, 1, 50);

            nextSlot++;
        }

        if (NPC.downedQueenBee)
        {
            shop.item[nextSlot].SetDefaults(ItemID.BottledHoney, false);
            shop.item[nextSlot].value = Item.buyPrice(0, 0, 9, 50);
            nextSlot++;
        }

        shop.item[nextSlot].SetDefaults(ItemID.LesserHealingPotion, false);
        shop.item[nextSlot].value = Item.buyPrice(0, 0, 2, 50);
        nextSlot++;

        shop.item[nextSlot].SetDefaults(ItemID.HealingPotion, false);
        shop.item[nextSlot].value = Item.buyPrice(0, 0, 9, 50);
        nextSlot++;


        if (NPC.downedMechBossAny)
        {
            shop.item[nextSlot].SetDefaults(ItemID.GreaterHealingPotion, false);
            shop.item[nextSlot].value = Item.buyPrice(0, 0, 29, 50);

            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.LifeforcePotion, false);
            shop.item[nextSlot].value = Item.buyPrice(0, 0, 9, 50);

            nextSlot++;
        }

        if (NPC.downedAncientCultist)
        {
            shop.item[nextSlot].SetDefaults(ItemID.GreaterHealingPotion, false);
            shop.item[nextSlot].value = Item.buyPrice(0, 1, 49, 50);

            nextSlot++;
        }

        if (NPC.downedMoonlord)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BaneOfTioska>(), false);
            shop.item[nextSlot].value = Item.buyPrice(1, 0, 0, 50);

            nextSlot++;
        }
    }

    public override string GetChat()
    {
        var dialogue = new List<string>();
        dialogue.Add("Beware, the end is nigh!" + (!NPC.downedMoonlord
            ? " Impending doom is approaching!"
            : " Oh, you've already beaten the squid guy?"));
        dialogue.Add(
            "I remember those old times when we stood in the middle of nowhere, shoutin': \"Deus Vult!\"\nWhat does it mean anyway?");
        dialogue.Add("How could mushrooms heal you? They are poisonous!");
        dialogue.Add("In the name of the Father, the Son and the Holy Spirit. Amen.");
        dialogue.Add("You wish you could dress like me? Ha! You're not worthy.");
        dialogue.Add("What is love? Baby don't hurt me... Sorry, I mean... Forget it.");
        dialogue.Add("I wonder, do you have a soul? I mean, you are a player, right?");
        dialogue.Add("You want infinite potion? I'll be broke in no time!");
        dialogue.Add("I know what you were doing with that hand.");
        dialogue.Add("I hope all these graves lying around don't belong to you.");

        if (Main.LocalPlayer.ZoneGraveyard) dialogue.Add("I hope all these graves lying around don't belong to you.");
        var princess = NPC.FindFirstNPC(663);
        if (princess != -1)
            dialogue.Add(
                "Can you please ask her highness " + Main.npc[princess].GivenName +
                " to stop calling me by name? It's creepy.");

        return Main.rand.Next(dialogue);
    }

    public override void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
        damage = 50;
        knockback = 2f;
    }

    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
        cooldown = 5;
        randExtraCooldown = 10;
    }

    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
        projType = ModContent.ProjectileType<BaneProjectile>();
        attackDelay = 1;
    }

    public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection,
        ref float randomOffset)
    {
        multiplier = 7f;
    }

    public override void OnKill()
    {
        Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.CrossNecklace);
        Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2(0, 10), ProjectileID.PurificationPowder,
            0, 0, Main.myPlayer);
    }
}
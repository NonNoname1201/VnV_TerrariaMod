using Terraria;
using Terraria.ModLoader;

namespace TLoh.Buffs;

public class Blessing : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("God's Blessing");
        Description.SetDefault("Lower potion sickness time, damage taken reduced by 10%");
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.endurance += 0.1f;
            player.potionDelayTime -= 900;
        }
    }
}
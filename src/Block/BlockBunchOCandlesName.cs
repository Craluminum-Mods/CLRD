using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace CLRD;

public class BlockBunchOCandlesName : BlockBunchOCandles
{
    public override string GetHeldItemName(ItemStack itemStack) => GetName(itemStack.Collectible);

    public override string GetPlacedBlockName(IWorldAccessor world, BlockPos pos) => GetName(this);

    private string GetName(CollectibleObject obj)
    {
        var color = obj.Variant["color"];
        var colorTranslated = Lang.Get("color-" + color);
        return obj.Code.FirstCodePart() switch
        {
            "bunchocandles" => Lang.GetMatching("block-bunchocandles-*") + " (" + colorTranslated + ")",
            _ => ""
        };
    }
}
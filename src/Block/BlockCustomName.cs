using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

namespace CLRD;

public class BlockName : Block
{
    public override string GetHeldItemName(ItemStack itemStack) => GetName(itemStack.Collectible);

    public override string GetPlacedBlockName(IWorldAccessor world, BlockPos pos) => GetName(this);

    private string GetName(CollectibleObject obj)
    {
        var color = obj.Variant["color"];
        var colorTranslated = Lang.Get("color-" + color);
        return obj.Code.FirstCodePart() switch
        {
            "leather" => Lang.Get("item-leather-" + obj.Variant["color"]),
            "cloth" => Lang.Get("item-cloth-" + obj.Variant["color"]),
            "plaster" => Lang.Get("block-plaster-plain") + " (" + colorTranslated + ")",
            _ => ""
        };
    }
}
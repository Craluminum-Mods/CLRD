using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace CLRD;

public class ItemName : Item
{
    public override string GetHeldItemName(ItemStack itemStack) => GetName(itemStack.Collectible);

    private string GetName(CollectibleObject obj)
    {
        var color = obj.Variant["color"];
        var colorTranslated = Lang.Get("color-" + color);
        return obj.Code.FirstCodePart() switch
        {
            "quicklime" => Lang.Get("item-quicklime") + " (" + colorTranslated + ")",
            "beeswax" => Lang.Get("item-beeswax") + " (" + colorTranslated + ")",
            "backpack" => Lang.Get("item-backpack") + " (" + colorTranslated + ")",
            "linensack" => Lang.Get("item-linensack") + " (" + colorTranslated + ")",
            "miningbag" => Lang.Get("item-miningbag") + " (" + colorTranslated + ")",
            _ => ""
        };
    }

    public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
    {
        base.GetHeldItemInfo(inSlot, dsc, world, withDebugInfo);
        AppendDescription(inSlot, dsc);
    }

    private static void AppendDescription(ItemSlot inSlot, StringBuilder dsc)
    {
        switch (inSlot.Itemstack.Collectible.Code.FirstCodePart())
        {
            case "backpack": dsc.Append(Lang.Get("itemdesc-backpack")); break;
            case "linensack": dsc.Append(Lang.Get("itemdesc-linensack")); break;
            case "miningbag": dsc.Append(Lang.Get("itemdesc-miningbag")); break;
        }
    }
}
using Vintagestory.API.Common;

[assembly: ModInfo("CLRD")]

namespace CLRD;

class Core : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);

        api.RegisterBlockClass("CLRD.BlockName", typeof(BlockName));
        api.RegisterBlockClass("CLRD.BlockBunchOCandlesName", typeof(BlockBunchOCandlesName));

        api.RegisterItemClass("CLRD.ItemName", typeof(ItemName));
        api.RegisterItemClass("CLRD.ItemCandleName", typeof(ItemCandleName));

        api.World.Logger.Event("started 'CLRD' mod");
    }
}
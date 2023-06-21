using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace CLRD;

public class ItemCandleName : ItemCandle
{
    public override string GetHeldItemName(ItemStack itemStack) => GetName(itemStack.Collectible);

    private string GetName(CollectibleObject obj)
    {
        var color = obj.Variant["color"];
        var colorTranslated = Lang.Get("color-" + color);
        return obj.Code.FirstCodePart() switch
        {
            "candle" => Lang.Get("item-candle") + " (" + colorTranslated + ")",
            _ => ""
        };
    }

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handHandling)
    {
        handHandling = EnumHandHandling.PreventDefault;

        if (blockSel == null || byEntity?.World == null || !byEntity.Controls.ShiftKey)
        {
            return;
        }
        IWorldAccessor world = byEntity.World;
        BlockPos offsetedPos = blockSel.Position.AddCopy(blockSel.Face);
        BlockPos belowPos = offsetedPos.DownCopy();
        Block targetedBlock = world.BlockAccessor.GetBlock(blockSel.Position);
        AssetLocation loc = new(Attributes["blockcodestartswith"].AsString());
        string codestartswith = loc.Domain + ":" + loc.Path;
        IPlayer player = byEntity.World.PlayerByUid((byEntity as EntityPlayer)?.PlayerUID);
        if (!byEntity.World.Claims.TryAccess(player, blockSel.Position, EnumBlockAccessFlags.BuildOrBreak))
        {
            slot.MarkDirty();
            return;
        }
        Block nextblock;
        if (targetedBlock.Code.ToString().StartsWith(codestartswith))
        {
            int.TryParse(targetedBlock.LastCodePart(), out int stage);
            if (stage == 9)
            {
                return;
            }
            nextblock = world.GetBlock(targetedBlock.CodeWithVariant("quantity", (stage + 1).ToString() ?? ""));
            world.BlockAccessor.SetBlock(nextblock.BlockId, blockSel.Position);
        }
        else
        {
            nextblock = byEntity.World.GetBlock(loc.WithPathAppendix("-1"));
            if (nextblock == null || !world.BlockAccessor.GetBlock(offsetedPos).IsReplacableBy(nextblock) || !world.BlockAccessor.GetBlock(belowPos).CanAttachBlockAt(world.BlockAccessor, nextblock, belowPos, BlockFacing.UP, new Cuboidi(1, 14, 1, 14, 15, 14)))
            {
                return;
            }
            world.BlockAccessor.SetBlock(nextblock.BlockId, offsetedPos);
        }
        if (player.WorldData.CurrentGameMode != EnumGameMode.Creative)
        {
            slot.TakeOut(1);
        }
        slot.MarkDirty();
        if (nextblock.Sounds != null)
        {
            world.PlaySoundAt(nextblock.Sounds.Place, blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z, player);
        }
    }
}
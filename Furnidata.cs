using Plus.Plugins;
using Plus.HabboHotel.Rooms.Chat.Commands;
using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Rooms;
using Plus.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Furnidata;

public class Furnidata : IPlugin
{
    
    public void Start()
    {
        var pluginInfo = new FurnidataDefinition();
        Logger(pluginInfo.Name + " by " + pluginInfo.Author + " has started.");
    }

    private void Logger(string message) {
        var pluginInfo = new FurnidataDefinition();
        var CYAN = "\u001b[34m";
        var WHITE = "\u001b[37m";
        Console.WriteLine(WHITE + "[" + CYAN + pluginInfo.Name + WHITE + "] " + message);    }

}


public class FurnidataModule : IChatCommand
{
    public string Key => "furnidata";

    public string PermissionRequired => "furnidata_plugin";

    public string Parameters => "";

    public string Description => "Shows furniture info when you type :furnidata infront of a furniture.";

    public async void Execute(GameClient session, Room room, string[] @params)
    {
        var tileInfront = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id).SquareInFront;
        var itemsInfront = room.GetRoomItemHandler().GetFurniObjects(tileInfront.X, tileInfront.Y);

        if (itemsInfront.Count == 0) {
            session.SendWhisper("No item is infront of you.");
            return;
         }

         if (itemsInfront.Count > 1) {
            session.SendWhisper("More than one item infront of you.");
            return;
         }

         var item = itemsInfront.First();
         var baseItem = item.GetBaseItem();

         session.SendNotification(
            "<b>public name</b>\n " + baseItem.PublicName + "\n" +
            "<b>furniture info</b>" + "\n" +
            "- id: " + baseItem.Id + "\n" +
            "- sprite id: " + baseItem.SpriteId + "\n" +
            "- classname: " + baseItem.ItemName + "\n" +
            "- width: " + baseItem.Width + "\n" +
            "- length: " + baseItem.Length + "\n" +
            "- stack_height: " + baseItem.Height + "\n" +
            "- can_stack: " + baseItem.Stackable + "\n" +
            "- can_sit: " + baseItem.IsSeat + "\n" +
            "- is_walkable: " + baseItem.Walkable + "\n" +
            "- allow_recycle: " + baseItem.AllowEcotronRecycle + "\n" +
            "- allow_trade: " + baseItem.AllowTrade + "\n" +
            "- allow_marketplace_sell: " + baseItem.AllowMarketplaceSell + "\n" +
            "- allow_gift: " + baseItem.AllowGift + "\n" +
            "- allow_inventory_stack: " + baseItem.AllowInventoryStack + "\n" +
            "- interaction_type: " + baseItem.InteractionType + "\n" +
            "- interaction_modes_count: " + baseItem.Modes + "\n" +
            "- behaviour_data: " + baseItem.BehaviourData + "\n" +
            "- vending_ids: " + System.String.Join(",", baseItem.VendingIds) + "\n" +
            "- height_adjustable: " + System.String.Join(",", baseItem.AdjustableHeights) + "\n" +
            "- effect_id: " + baseItem.EffectId + "\n" +
            "- is_rare: " + baseItem.IsRare + "\n" +
            "- extra_rot: " + baseItem.ExtraRot + "\n" +
            "<b>item/room</b>" + "\n" +
            "- item id: " + item.Id + "\n" +
            "- user id: " + session.GetHabbo().Id + "\n" +
            "- current rotation: " + item.Rotation + "\n" +
            "- x: " + item.GetX + "\n" +
            "- y: " + item.GetY + "\n" +
            "- z: " + item.GetZ
            );
        
    }
}

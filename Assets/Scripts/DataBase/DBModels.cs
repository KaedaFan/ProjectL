using Realms;

public class InventoryItemModel : RealmObject
{
    [PrimaryKey]
    public string ItemId { get; set; } 

    public int Count {  get; set; }
}
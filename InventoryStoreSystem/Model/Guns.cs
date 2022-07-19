using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryApi.Model
{
    [BsonIgnoreExtraElements]
    public class Guns
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string FirearmType { get; set; }
        public string Manufacturer { get; set; }
        public int Model { get; set; }
        public string Caliber { get; set; }
        public string CountryOfOrigin { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public string PurchasedStoreName { get; set; }
        public string SerialNo { get; set; }
        public string Codition { get; set; }
        public string StoredLocation { get; set; }
        public double PurchasePrice { get; set; }
        public string InsuranceValue { get; set; }
        public string Action { get; set; }
        public string OAL { get; set; }
        public string Weight { get; set; }
        public string TwistRate { get; set; }
        public string LengthOfPull { get; set; }
        public string BarrelLength { get; set; }
        public string BarrelFinish { get; set; }
        public string GunFinish { get; set; }
        public string AmmoTypeUsed { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public string GeneralNotes { get; set; }
        public string Sights { get; set; }
        public bool Scope { get; set; }
        public string TypeOfScope { get; set; }
        public bool Laser { get; set; }
        public string TypeOfLaser { get; set; }
        public string StockType { get; set; }
        public string TriggerType { get; set; }
        public string TriggerPull { get; set; }
        public string CostOfFirearm { get; set; }
        public string AnyOtherAccessories { get; set; }
        public bool PreBan { get; set; }
        public DateTime? NFAApprovalDate { get; set; }
        public string NFAPaperworkScanned { get; set; }
        public string NameOfTrustOrCorporation { get; set; }
        public bool LocalPoliceApproval { get; set; }
        public string LocalPoliceStationName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}


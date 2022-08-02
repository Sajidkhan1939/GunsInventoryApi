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
        
        public string LocalPoliceStationName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}


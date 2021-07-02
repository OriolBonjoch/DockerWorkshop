using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonCore.Models
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int PokepediaId { get; set; }

        public object Name { get; set; }

        public int Height { get; set; }

        public object Species { get; set; }

        public int Experience { get; set; }

        public bool Catched { get; set; }

        public bool Fainted { get; set; }
    }
}

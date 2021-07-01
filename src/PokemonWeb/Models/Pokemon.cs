namespace PokemonWeb.Models
{
    public class Pokemon
    {
        public string Id { get; set; }

        public int PokepediaId { get; set; }

        public string Name { get; set; }

        public string Species { get; set; }

        public int Height { get; set; }

        public int Experience { get; set; }

        public bool Catched { get; set; }

        public bool Fainted { get; set; }
    }
}
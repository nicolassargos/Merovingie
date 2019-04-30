using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;

namespace AoC.Api.Domain
{
    public class Farm : ActiveBuilding, IProductable
    {
        public int Time { get; set; }

        public SerializableDictionary<ResourcesType, int> Cost { get; set; }

        public int PopulationIncrement { get; set; }

        public Farm(string Name, Coordinates Position)
        : base(Name, 100, 100, false)
        {
            var farm = GetDefaultItemProperties();
            Cost = farm.Cost;
            PopulationIncrement = farm.PopulationIncrement;
            Resources = farm.Resources;
            Time = farm.Time;
        }

        public Farm(int Id, string Name, Coordinates Position) 
            : this(Name, Position)
        {
            this.Id = Id;
            Name = "Name" + Id.ToString();
        }

        public Farm() { }

        private Farm GetDefaultItemProperties()
        {
            var farm = new Farm();
            farm.Attack = false;
            farm.Cost = new SerializableDictionary<ResourcesType, int> { { ResourcesType.Gold, 200 }, { ResourcesType.Stone, 200 }, { ResourcesType.Wood, 500 } };
            farm.MaxLifePoints = 100;
            farm.LifePoints = farm.MaxLifePoints;
            farm.Name = "NoName";
            farm.PopulationIncrement = 4;
            farm.Resources = new SerializableDictionary<ResourcesType, int>();
            farm.Time = 3000;

            return farm;
        }
    }
}

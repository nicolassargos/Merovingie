using Common.Enums;
using Common.Helpers;
using AoC.Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AoC.Api.Domain
{
    public class ActiveBuilding: IBuilding
    {

        #region Properties
        [XmlElement]
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public int LifePoints { get; set; }
        public int MaxLifePoints { get; set; }
        public bool Attack { get; set; }
        public SerializableDictionary<ResourcesType, int> Stock { get; set; }
        #endregion


        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="lifepoints"></param>
        /// <param name="maxLifePoints"></param>
        /// <param name="attack"></param>
        /// <param name="resources"></param>
        protected ActiveBuilding(string name, int lifepoints, int maxLifePoints,
                                    bool attack)
        {
            Name = name;
            LifePoints = lifepoints;
            MaxLifePoints = maxLifePoints;
            Attack = attack;
            Stock = new SerializableDictionary<ResourcesType, int>();
        }

        public ActiveBuilding() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool DestroyBuilding()
        {
            return true;
        }

        public virtual bool CreateBuilding()
        {
            return true;
        }
        #endregion

    }
}

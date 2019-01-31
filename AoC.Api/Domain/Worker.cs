using AoC.Api.EventArgs;
using AoC.BLL;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Timers;
using System.Xml.Serialization;

namespace AoC.Api.Domain
{
    public class Worker : ICreator, IProductable, IUnit
    {
        [XmlIgnore]
        public ConcurrentQueue<IProductable> ProductionQueue
        {
            get; set;
        }
        public int Time { get => 3000; }

        public SerializableDictionary<ResourcesType, int> Cost
        {
            get;
        }
        public SerializableDictionary<ResourcesType, int> HoldedResources { get; set; }
        public PassiveBuilding FetchingBuilding { get; set; }
        public int LifePointsMax { get; set; }
        public int LifePoints { get; set; }
        public int AttackPower { get; set; }
        public Coordinates Position { get; set; }
        public bool Available { get; set; }
        public int Id { get; set; }
        public int PopulationSlots { get; set; }
        public bool IsWorking { get; set; }
        private static System.Timers.Timer _timer;

        public Worker()
        {
            ProductionQueue = new ConcurrentQueue<IProductable>();

            Cost = new SerializableDictionary<ResourcesType, int>() { { ResourcesType.Gold, 50 } };
            AttackPower = 1;
            LifePoints = 50;
            LifePointsMax = 50;
            PopulationSlots = 1;
            IsWorking = false;
            HoldedResources = new SerializableDictionary<ResourcesType, int>();
            FetchingBuilding = null;
            
            _timer = new System.Timers.Timer();

            Position = new Coordinates { x = 10, y = 10 };
        }

        public Worker(int Id)
            : this()
        {
            this.Id = Id;
        }

        /// <summary>
        /// Méthode appelée par le Manager pour re
        /// </summary>
        /// <param name="resource"></param>
        public void FetchResource(PassiveBuilding passiveBuilding)
        {
            //CancelAllActions();
            FetchingBuilding = passiveBuilding;
            IsWorking = true;


            // lancer une task de durée infinie 
            // qui rapporte la resource en question
            // Create a timer with a two second interval.
            _timer.Interval = 3000;
            // Hook up the Elapsed event for the timer.
            _timer.Elapsed += DoFetch;

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="qty"></param>
        private void DoFetch(Object sender, ElapsedEventArgs e)
        {
            // Cas où le worker ramasse la ressource
            if (HoldedResources.Count == 0)
            {
                var qtyCollected = FetchingBuilding.Remove(FetchingBuilding.CollectQty);
                HoldedResources.Add(qtyCollected.Key, qtyCollected.Value);
            }
            // Cas où le worker revient à la base
            else
            {
                OnResourceFetched(new ResourcesFetchedArgs { ResourcesFetched = HoldedResources });
                HoldedResources.Clear();
            }

        }

        protected void OnResourceFetched(ResourcesFetchedArgs e)
        {
            ResourceFetched?.Invoke(this, e);
        }

        public void CancelFetch()
        {
            _timer.Elapsed -= DoFetch;
            _timer.Dispose();
            _timer = new System.Timers.Timer();
            ResourceFetched = null;
        }

        public void CancelAllActions()
        {
            EmptyProductionQueue();
            CancelFetch();
            IsWorking = false;
        }

        private void EmptyProductionQueue()
        {
            // vider la production queue
            ProductionQueue.TryDequeue(out var productable);
        }

        // Evénement déclenché lorsque qu'une resource a été récoltée
        public event EventHandler<ResourcesFetchedArgs> ResourceFetched;
    }
}

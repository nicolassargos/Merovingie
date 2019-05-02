using AoC.Api.EventArgs;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Concurrent;
using System.Timers;
using System.Xml.Serialization;
using AoC.Api.Services;


namespace AoC.Api.Domain
{
    public class Worker : ICreator, IProductable, IUnit
    {

        #region Properties

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

        #endregion

        private static System.Timers.Timer _timer;

        private Generator _generator;


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
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

            _generator = new Generator(this);

            Position = new Coordinates { x = 10, y = 10 };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public Worker(int Id)
            : this()
        {
            this.Id = Id;
        }

        #endregion


        #region FetchRessoure

        /// <summary>
        /// Créée l'action de ramassage de ressource
        /// </summary>
        /// <param name="resource"></param>
        public void FetchResource(PassiveBuilding passiveBuilding)
        {
            FetchingBuilding = passiveBuilding;
            IsWorking = true;

            // lancer une task de durée infinie 
            // qui rapporte la resource en question
            // (3000ms simule le trajet)
            _timer.Interval = 3000;
            // Attache l'événement à lancer lorsque le ramassage est prêt
            _timer.Elapsed += DoFetch;

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }


        /// <summary>
        /// Lance les actions qui découlent du ramassage de ressource
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


        /// <summary>
        /// Evenement déclenché en cas de retour à un bâtiment de stockage
        /// avec des ressources
        /// </summary>
        /// <param name="e"></param>
        protected void OnResourceFetched(ResourcesFetchedArgs e)
        {
            ResourceFetched?.Invoke(this, e);
        }


        /// <summary>
        /// Annule l'action d'aller chercher une ressource
        /// </summary>
        public void CancelFetch()
        {
            _timer.Elapsed -= DoFetch;
            _timer.Dispose();
            _timer = new System.Timers.Timer();
            ResourceFetched = null;
        }

        #endregion


        #region ProductionQueue

        public void LaunchProduction (IProductable productable, Action<IProductable> callBack)
        {
            if (productable == null) throw new ArgumentNullException("LaunchProduction: productable is null");
            _generator.CreateEntity(productable, callBack);
        }

        /// <summary>
        /// Annule toutes las actions
        /// </summary>
        public void CancelAllActions()
        {
            EmptyProductionQueue();
            CancelFetch();
            IsWorking = false;
        }


        /// <summary>
        /// Vide la queue de production
        /// </summary>
        private void EmptyProductionQueue()
        {
            // vider la production queue
            ProductionQueue.TryDequeue(out var productable);
        }

        #endregion


        // Evénement déclenché lorsque qu'une resource a été récoltée
        public event EventHandler<ResourcesFetchedArgs> ResourceFetched;
    }
}

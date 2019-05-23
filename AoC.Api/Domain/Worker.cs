using AoC.Api.Domain.EventArgs;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Concurrent;
using System.Timers;
using System.Xml.Serialization;
using AoC.Api.Services;
using System.Collections.Generic;
using AutoMapper.Configuration.Annotations;
using AoC.DataLayer.Descriptors;
using AutoMapper;

namespace AoC.Api.Domain
{
    [AutoMap(typeof(WorkerDescriptor))]
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
        public int FetchingBuildingId { get; set; }
        [Ignore]
        public PassiveBuilding FetchingBuilding { get; set; }
        public int LifePointsMax { get; set; }
        public int LifePoints { get; set; }
        public int AttackPower { get; set; }
        public Coordinates Position { get; set; }
        public bool Available { get; set; }
        public int Id { get; set; }
        public int PopulationSlots { get; set; }
        public bool IsWorking { get; set; }
        public bool IsHoldingResource
        {
            get
            {
                if (HoldedResources == null || HoldedResources.Count == 0)
                {
                    HoldedResources = ResourceHelper.CreateEmptyResourcesDictionary();
                }

                return HoldedResources[ResourcesType.Gold] > 0 ||
                HoldedResources[ResourcesType.Stone] > 0 ||
                HoldedResources[ResourcesType.Wood] > 0;
            }
        }

        #endregion

        private static System.Timers.Timer _timer;

        private Generator _generator;

        // Evénement déclenché lorsque qu'une resource a été récoltée
        public event EventHandler<ResourcesFetchedArgs> ResourceCollected;


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Worker() : base()
        {
            AttackPower = 1;
            LifePoints = 50;
            LifePointsMax = 50;
            PopulationSlots = 1;
            IsWorking = false;
            FetchingBuildingId = 0;

            ProductionQueue = new ConcurrentQueue<IProductable>();

            // TODO: remplacer la valeur statique du cout de production par une valeur cherchée en configuration
            Cost = new SerializableDictionary<ResourcesType, int>() { { ResourcesType.Gold, 50 } };

            HoldedResources = ResourceHelper.CreateEmptyResourcesDictionary();

            _timer = new System.Timers.Timer();

            _generator = new Generator(this);

            Position = new Coordinates { x = 0, y = 0 };
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

        public Worker(int lifePoints, bool isWorking, SerializableDictionary<ResourcesType, int> holdedResources, Coordinates position)
            : this()
        {
            if (lifePoints == 0) throw new ArgumentException("WorkerConstructor: Life points cannot be 0");
            LifePoints = lifePoints;

            IsWorking = isWorking;

            if (holdedResources != null && holdedResources.Count > 0)
            {
                HoldedResources = holdedResources;
            }

            if (position.x > 0 && position.y > 0)
            {
                Position = position;
            }
        }

        public Worker(Coordinates position)
            : this()
        {
            if (position.x > 0 && position.y > 0)
            {
                Position = position;
            }
        }

        #endregion


        #region FetchRessoure

        /// <summary>
        /// Créée l'action de ramassage de ressource
        /// </summary>
        /// <param name="resource"></param>
        public void FetchResource(PassiveBuilding passiveBuilding)
        {
            FetchingBuildingId = passiveBuilding.Id;
            FetchingBuilding = passiveBuilding;
            IsWorking = true;

            // lancer une task de durée finie en boucle
            // (3000ms simule le temps d'extraction)
            _timer.Interval = passiveBuilding.FetchTimeEllapse;
            // Attache l'événement à lancer lorsque le ramassage est prêt
            _timer.Elapsed += CommitFetch;

            _timer.AutoReset = false;
            _timer.Enabled = true;
        }


        /// <summary>
        /// Lance les actions qui découlent du ramassage de ressource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="qty"></param>
        private void CommitFetch(Object sender, ElapsedEventArgs e)
        {
            // Retire une quantité de ressources au stock du building
            var resourceCollected = FetchingBuilding.Remove(new KeyValuePair<ResourcesType, int>(FetchingBuilding.Resource, FetchingBuilding.CollectQty));

            // Ajoute une quantité au stock du worker
            HoldedResources[resourceCollected.Key] =  resourceCollected.Value;

            // Emet l'événement d'ajout au stock
            OnResourceFetched(new ResourcesFetchedArgs { resources = HoldedResources, buildingId = FetchingBuilding.Id, unitId = this.Id });
        }


        /// <summary>
        /// Evenement déclenché en cas de retour à un bâtiment de stockage
        /// avec des ressources
        /// </summary>
        /// <param name="e"></param>
        protected void OnResourceFetched(ResourcesFetchedArgs e)
        {
            ResourceCollected?.Invoke(this, e);
        }

        /// <summary>
        /// Réinitialise à 0 les ressources portées par un worker
        /// </summary>
        public void ReleaseResources()
        {
            HoldedResources[ResourcesType.Gold] = 0;
            HoldedResources[ResourcesType.Stone] = 0;
            HoldedResources[ResourcesType.Wood] = 0;
        }


        /// <summary>
        /// Annule l'action d'aller chercher une ressource
        /// </summary>
        public void CancelFetch()
        {
            _timer.Elapsed -= CommitFetch;
            _timer.Dispose();
            _timer = new System.Timers.Timer();
            ResourceCollected = null;
        }

        #endregion


        #region ProductionQueue

        public void LaunchProduction (IProductable productable, Action<IProductable> callBack)
        {
            if (productable == null) throw new ArgumentNullException("LaunchProduction: productable is null");
            IsWorking = true;
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
    }
}

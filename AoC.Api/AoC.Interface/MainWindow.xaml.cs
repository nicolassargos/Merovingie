using AoC.Api.Domain;
using AoC.Api.EventArgs;
using AoC.Api.UseCases;
using AoC.Map;
using Common.Enums;
using Common.Exceptions;
using Common.Interfaces;
using Domain;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AoC.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string WORKER_CREATED = "A new Worker was just created!\n";
        const string FARM_CREATED = "A new farm has been created!\n";
        const string NO_WORKER_AVAILABLE = "No workers are available to do this\n";
        const string NOT_ENOUGH_RESOURCES = "Not enough resources!\n";
        const string NOT_ENOUGH_SLOTS = "Not enough place... Build some farms!\n";

        GameManager manager;
        int SelectedItemId;
        IGameDescriptor game;

        #region Events

        private void OnMaxPopulationChanged(object sender, MaxPopulationChangedArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                MaxPopulation.Content = e.CurrentMaxPopulation;
                LogBox.Text += FARM_CREATED;
            });
        }

        private void OnResourcesChanged(object sender, ResourcesChangedArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                StoneStock.Content = e.CurrentResources[ResourcesType.Stone];
                WoodStock.Content = e.CurrentResources[ResourcesType.Wood];
                GoldStock.Content = e.CurrentResources[ResourcesType.Gold];
            });
        }

        private void OnPopulationChanged(object sender, PopulationChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                TotalPopulationCount.Content = e.CurrentPopulation;
                LogBox.Text += WORKER_CREATED;
                AddWorkerToUI(e.Unit);
            });
        }

        private void OnBuildingCreated(object sender, BuildingCreatedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                LogBox.Text += $"Batiment {e.building.Name} créé";
                AddBuildingToUI(e.building);
            });
        }

        private void OnCarryResourceCollected(object sender, ResourcesChangedArgs e)
        {
            Dispatcher.Invoke(() => {
                LogBox.Text += e.CurrentResources[ResourcesType.Stone].ToString() + " units have been collected from Carry\n";
            });
        }

        #endregion

        #region Main

        public MainWindow()
        {
            // Génère la carte
            game = GameGenerator.GenerateDefaultMap();

            // Sauvegarde la nouvelle partie dans un fichier
            GameFileManager.SaveGame(game, "game1");

            // Lit le fichier
            game = GameFileManager.ReadGame("game1");

            manager = new GameManager(game);

            manager.PopulationChanged += OnPopulationChanged;
            //manager.ResourcesChanged += OnResourcesChanged;
            manager.OnMaxPopulationChanged += OnMaxPopulationChanged;
            manager.OnBuildingCreated += OnBuildingCreated;

            foreach (var carry in game.Carries)
            {
                carry.CarryStockChanged += OnCarryResourceCollected;
            }

            InitializeComponent();
            this.DataContext = this;
            InitializeUIFromGame(game);
        }

        #endregion

        #region ControllerCreation

        /// <summary>
        /// Initialise l'interface à partir d'un GameDescriptor
        /// </summary>
        /// <param name="game"></param>
        private void InitializeUIFromGame(IGameDescriptor game)
        {
            foreach (var townhall in game.TownHalls)
            {
                AddTownHallToUI(townhall);
            }

            foreach (var farm in game.Farms)
            {
                AddFarmToUI(farm);
            }

            foreach (var worker in game.Workers)
            {
                AddWorkerToUI(worker);
            }
        }

        /// <summary>
        /// Redirige l'ajout d'un bouton de building vers la méthode appropriée
        /// </summary>
        /// <param name="building"></param>
        private void AddBuildingToUI(IBuilding building)
        {
            if (building == null) return;

            switch (building.GetType().Name)
            {
                case "Farm":
                    AddFarmToUI(building as Farm);
                    break;
                case "TownHall":
                    AddTownHallToUI(building as TownHall);
                    break;
                default: break;
            }
        }

        /// <summary>
        /// Ajoute dynamiquement un bouton Worker à l'interface graphique
        /// </summary>
        /// <param name="unit"></param>
        private void AddWorkerToUI(IUnit unit)
        {
            // TODO: Log
            if (unit == null) return;

            var btnWorker = new Button { Content = ($"Worker {unit.Id}") };
            // Créé un bouton dynamiquement qui sélectionne le worker nouvellement créé
            btnWorker.Click += (s, e) =>
            {
                SelectedItemId = unit.Id;
                LogBox.Text += "Worker selected " + SelectedItemId + "\n";
                ProductionPanel.Visibility = Visibility.Visible;
                ProductionListLabel.Content = $"Worker {SelectedItemId}";
            };

            ActionPanel.Children.Add(btnWorker);
        }

        /// <summary>
        /// Ajoute dynamiquement un bouton Farm à l'interface graphique
        /// </summary>
        /// <param name="farm"></param>
        private void AddFarmToUI(Farm farm)
        {
            // TODO: log
            if (farm == null) return;

            var btnBuilding = new Button { Content = ($"Farm {farm.Id}") };
            // Créé un bouton dynamiquement qui sélectionne le worker nouvellement créé
            btnBuilding.Click += (s, e) =>
            {
                SelectedItemId = farm.Id;
                LogBox.Text += "Farm selected " + SelectedItemId + "\n";
                ProductionPanel.Visibility = Visibility.Visible;
                ProductionListLabel.Content = $"Farm {SelectedItemId}";
            };

            ActionPanel.Children.Add(btnBuilding);
        }

        /// <summary>
        /// Ajoute dynamiquement un bouton TownHall à l'interface graphique
        /// </summary>
        /// <param name="townHall"></param>
        private void AddTownHallToUI(TownHall townHall)
        {
            // TODO: log
            if (townHall == null) return;

            var btnBuilding = new Button { Content = ($"Town Hall {townHall.Id}") };
            // Créé un bouton dynamiquement qui sélectionne le worker nouvellement créé
            btnBuilding.Click += (s, e) =>
            {
                SelectedItemId = townHall.Id;
                LogBox.Text += "Town Hall selected " + SelectedItemId + "\n";
                CreateNewWorkerBtn.Visibility = Visibility.Visible;
                ProductionListLabel.Content = $"Town Hall {SelectedItemId}";
            };

            ActionPanel.Children.Add(btnBuilding);
        }

        #endregion


        private void Manager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Type myType = typeof(e.PropertyName);
            //this.TotalPopulationCount.Content = Type.GetField(e.PropertyName);
            ////    Type myTypeA = typeof(MyFieldClassA);
            ////FieldInfo myFieldInfo = myTypeA.GetField("Field");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                manager.CreateWorker(game.TownHalls.Find( t => t.Id == SelectedItemId));
            }
            catch (NotEnoughResourcesException rex)
            {
                LogBox.Text += NOT_ENOUGH_RESOURCES;
            }
            catch (NotEnoughUnitSlotsAvailableException uex)
            {
                LogBox.Text += NOT_ENOUGH_SLOTS;
            }
            catch (Exception)
            {
                LogBox.Text += "Town hall non trouvé!";
            }
            
        }

        private void CreateNewFarmBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                manager.CreateFarm(SelectedItemId);
            }
            catch(NoWorkerAvailableException ex)
            {
                LogBox.Text += NO_WORKER_AVAILABLE;
            }
            catch (NotEnoughResourcesException ex)
            {
                LogBox.Text += NOT_ENOUGH_RESOURCES;
            }
        }

        private void FetchWoodBtn_Click(object sender, RoutedEventArgs e)
        {
            LogBox.Text += new StringBuilder($"Worker {SelectedItemId} go fetch some wood...\n");
            manager.FetchResource(SelectedItemId, game.Trees[0]);
        }

        private void FetchStoneBtn_Click(object sender, RoutedEventArgs e)
        {
            LogBox.Text += new StringBuilder($"Worker {SelectedItemId} go fetch some stone...\n");
            manager.FetchResource(SelectedItemId, game.Carries[0]);
        }

        private void FetchGoldBtn_Click(object sender, RoutedEventArgs e)
        {
            LogBox.Text += new StringBuilder($"Worker {SelectedItemId} go fetch some gold...\n");
            manager.FetchResource(SelectedItemId, game.GoldMines[0]);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            manager.CancelTask(SelectedItemId);
        }
    }
}

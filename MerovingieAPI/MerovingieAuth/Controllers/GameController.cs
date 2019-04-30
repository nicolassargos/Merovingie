using System.Collections.Generic;
using AoC.Api.Domain;
using AoC.Api.Domain.UseCases;
using AoC.MerovingieFileManager;
using Common.Enums;
using Common.Helpers;
using Merovingie.Helpers;
using Merovingie.Models;
using Merovingie.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Merovingie.Controllers
{
    public class GameController : Controller
    {
        GameManager manager;
        IGameDescriptor game;
        UIMessages uiMessages;
        private readonly IConfiguration configuration;

        public GameController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        // GET: /<controller>/
        public IActionResult Game(string fileName)
        {
            // initialise la classe qui contient tous le messages de l'UI
            uiMessages = new UIMessages();

            // Lit le fichier
            game = GameFileManager.ReadGame(fileName);

            manager = new GameManager(game);

            return View(manager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult GameListing()
        {
            var filesDetected = GameFileManager.GetGameFiles();

            IList<GameFileDetailModel> gameFiles = new List<GameFileDetailModel>();
            foreach (var item in filesDetected)
            {
                gameFiles.Add(new GameFileDetailModel() { creationDate = item.CreationDate, name = item.Name, path = item.Path });
            }

            return View(gameFiles);
        }


        [Authorize]
        [HttpGet]
        [Route("Load/{gamePathToLoad}")]
        public IActionResult Load(string gamePathToLoad)
        {

            return Redirect($"~/{configuration.GetValue<string>("GameEngine")}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var newGameDescriptor = GameGenerator.GenerateDefaultMap();

            // TODO: créer un mapper

            // Mapping de GameDescriptor vers GameDescriptorModel
            GameDescriptorModel gamedescriptorModel = new GameDescriptorModel
            {
                Farms = newGameDescriptor.Farms.Count,
                Workers = newGameDescriptor.Workers.Count,
            };

            if (newGameDescriptor.Resources != null)
            {
                gamedescriptorModel.Resources = new SerializableDictionary<ResourcesType, int>();
                foreach (var resource in newGameDescriptor.Resources)
                {
                    gamedescriptorModel.Resources[resource.Key] = resource.Value;
                }
            }
            
            return View(gamedescriptorModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public RedirectToActionResult Create(GameDescriptorModel gameModel)
        {
            IGameDescriptor newGameDescriptor = GameGenerator.GenerateMapFromOptions(gameModel.Workers, gameModel.Farms, gameModel.Resources);

            GameFileManager.SaveGame(newGameDescriptor, "newGame");

            return RedirectToAction("Load");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Authorize]
        public RedirectToActionResult Delete(string fileName)
        {
            GameFileManager.DeleteGame(fileName);

            return RedirectToAction("Load");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Api.UseCases;
using AoC.Map;
using Common.Enums;
using Common.Helpers;
using Domain;
using Merovingie.Helpers;
using Merovingie.Models;
using Merovingie.Models.Game;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Merovingie.Controllers
{
    public class GameController : Controller
    {
        GameManager manager;
        IGameDescriptor game;
        UIMessages uiMessages;


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
        public IActionResult Load()
        {
            var filesDetected = GameFileManager.GetGames();

            IList<GameFileDetailModel> gameFiles = new List<GameFileDetailModel>();
            foreach (var item in filesDetected)
            {
                gameFiles.Add(new GameFileDetailModel() { CreationDate = item.CreationDate, Name = item.Name, Path = item.Path });
            }

            return View(gameFiles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        [HttpPost]
        public RedirectToActionResult Create(GameDescriptorModel gameModel)
        {
            IGameDescriptor newGameDescriptor = GameGenerator.GenerateMapFromOptions(gameModel.Workers, gameModel.Farms, gameModel.Resources);

            GameFileManager.SaveGame(newGameDescriptor, "newGame");

            return RedirectToAction("Load");
        }


        public RedirectToActionResult Delete(string fileName)
        {
            GameFileManager.DeleteGame(fileName);

            return RedirectToAction("Load");
        }

    }
}

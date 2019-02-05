using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Api.UseCases;
using AoC.Map;
using AutoMapper;
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
        public IActionResult Game()
        {
            // initialise la classe qui contient tous le messages de l'UI
            uiMessages = new UIMessages();
            // Génère la carte
            game = GameGenerator.GenerateMap();

            // Sauvegarde la nouvelle partie dans un fichier
            GameFileManager.SaveGame(game, "game1");

            // Lit le fichier
            game = GameFileManager.ReadGame("game1");

            manager = new GameManager(game);

            return View(manager);
        }

        public IActionResult Load()
        {
            var filesDetected = GameFileManager.GetGames();

            IList<GameFileDetailModel> gameFiles = new List<GameFileDetailModel>();
            foreach (var item in filesDetected)
            {
                gameFiles.Add(new GameFileDetailModel() { CreationDate = item.CreationDate, Name = item.Name, Path = item.Path });
            }

            //if (gameFiles.Count() == 0) return new EmptyResult();
            return View(gameFiles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newGameDescriptor = GameGenerator.GenerateMap();

            GameDescriptorModel gamedescriptorModel = Mapper.Map<IGameDescriptor, GameDescriptorModel>(newGameDescriptor);
            return View(gamedescriptorModel);
        }

    }
}

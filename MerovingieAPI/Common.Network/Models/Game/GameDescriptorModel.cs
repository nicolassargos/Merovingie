using System.ComponentModel.DataAnnotations;
using Common.Enums;
using Common.Helpers;
using Xunit;




namespace AoC.Common.Network.Models
{
    public class GameDescriptorModel
    {
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]{1,40}$",
         ErrorMessage = "Characters and numbers only are allowed.")]
        [Display(Name = "Game name")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Only numbers less than 100 are allowed.")]
        [Display(Name = "Farms")]
        public int Farms { get; set; }

        [Range(0, 100, ErrorMessage = "Only numbers less than 100 are allowed.")]
        [Display(Name = "Peasants")]
        public int Workers { get; set; }
        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
    }
}

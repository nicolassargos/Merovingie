using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ResourceHelper
    {
        /// <summary>
        /// Formatte et renvoie un dictionnaire contenant une ressource et une quantité
        /// associée (correspondant à une collecte)
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public static SerializableDictionary<ResourcesType, int> GetResourcesCollected(ResourcesType resource, int qty)
        {
            var formatedResource = new SerializableDictionary<ResourcesType, int>();
            formatedResource.Add(resource, qty);

            return formatedResource;
        }

        public static SerializableDictionary<ResourcesType, int> CreateEmptyResourcesDictionary()
        {
            return new SerializableDictionary<ResourcesType, int>
                    {
                        { ResourcesType.Gold, 0 },
                        { ResourcesType.Stone, 0 },
                        { ResourcesType.Wood, 0 }
                    };
        }
    }
}

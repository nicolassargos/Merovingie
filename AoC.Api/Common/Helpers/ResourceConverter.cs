using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ResourceConverter
    {
        /// <summary>
        /// Formatte et renvoie un dictionnaire contenant une ressource et un quantité
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
    }
}

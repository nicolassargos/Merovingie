using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace AoC.Common.Network.Models
{
    public class GameFileDetailModel
    {
        public string name { get; set; }
        public string path { get; set; }

        public string fullPath {
            get
            {
                if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(name)) return null;
                return Path.Combine(path, name);
            }
        }

        public DateTime creationDate { get; set; }
    }
}

using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merovingie.Models
{
    public class GameFileDetailModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

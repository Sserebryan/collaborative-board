using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollaborationBoard.Models
{
    public class MetaData
    {
        [JsonProperty(PropertyName = "drawState")]
        public DrawState DrawState { get; set; }

        [JsonProperty(PropertyName = "currX")]
        public int CurrX { get; set; }

        [JsonProperty(PropertyName = "currY")]
        public int CurrY { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries
{
    internal class IWildberriesResponse
    {
        [JsonIgnore] public int Id { get; set; }
    }
}

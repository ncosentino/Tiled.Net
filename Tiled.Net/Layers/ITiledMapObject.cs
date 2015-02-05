﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiled.Net.Layers
{
    public interface ITiledMapObject
    {
        #region Properties
        string Id { get; }

        int Gid { get; }

        string Name { get; }

        string Type { get; }

        float X { get; }

        float Y { get; }

        float? Width { get; }

        float? Height { get; }
     
        IEnumerable<string>  PropertyNames { get; }
        
        IEnumerable<KeyValuePair<string, string>> Properties { get; }
        #endregion

        #region Methods
        string GetPropertyValue(string propertyName);
        #endregion
    }
}

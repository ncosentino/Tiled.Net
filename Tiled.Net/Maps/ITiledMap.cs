﻿using System.Collections.Generic;
using Tiled.Net.Layers;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Maps
{
    public interface ITiledMap
    {
        #region Properties
        int Width { get; }
        
        int Height { get; }
        
        int TileWidth { get; }
        
        int TileHeight { get; }
        
        IReadOnlyCollection<ITileset> Tilesets { get; }

        IReadOnlyCollection<IMapLayer> Layers { get; }

        IReadOnlyCollection<IObjectLayer> ObjectLayers { get; }
        #endregion
    }
}

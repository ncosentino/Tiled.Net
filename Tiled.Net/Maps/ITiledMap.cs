using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
        IEnumerable<ITileset> Tilesets { get; }
        
        IEnumerable<IMapLayer> Layers { get; }
        
        IEnumerable<IObjectLayer> ObjectLayers { get; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiled.Net.Tilesets
{
    public interface ITileset
    {
        #region Properties
        int FirstGid { get; }

        string Name { get; }

        int TileWidth { get; }

        int TileHeight { get; }

        IEnumerable<ITilesetImage> Images { get; }
        
        IEnumerable<ITilesetTile> Tiles { get; }
        #endregion
    }
}

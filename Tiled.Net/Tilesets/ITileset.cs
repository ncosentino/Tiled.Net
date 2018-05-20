using System.Collections.Generic;
using Tiled.Net.Terrain;

namespace Tiled.Net.Tilesets
{
    public interface ITileset
    {
        #region Properties
        int FirstGid { get; }

        string Name { get; }

        int TileWidth { get; }

        int TileHeight { get; }

        // FIXME: is this supposed to be a single image for a tileset... yes?
        IReadOnlyCollection<ITilesetImage> Images { get; }

        IReadOnlyCollection<ITerrainType> TerrainTypes { get; }

        IReadOnlyDictionary<int, ITilesetTile> Tiles { get; }
        #endregion
    }
}

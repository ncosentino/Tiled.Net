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

        IReadOnlyCollection<ITilesetImage> Images { get; }

        IReadOnlyCollection<ITerrainType> TerrainTypes { get; }

        IReadOnlyCollection<ITilesetTile> Tiles { get; }
        #endregion
    }
}

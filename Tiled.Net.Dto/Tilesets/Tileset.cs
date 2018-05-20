﻿using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Terrain;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Dto.Tilesets
{
    public sealed class Tileset : ITileset
    {
        #region Constructors
        public Tileset(
            int firstGid,
            string name,
            int tileWidth,
            int tileHeight,
            IEnumerable<ITilesetImage> images,
            IEnumerable<ITerrainType> terrainTypes,
            IEnumerable<ITilesetTile> tiles)
        {
            FirstGid = firstGid;
            Name = name;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Images = images.ToArray();
            TerrainTypes = terrainTypes.ToArray();
            Tiles = tiles.ToDictionary(x => x.Id, x => x);
        }
        #endregion

        #region Properties
        public int FirstGid { get; }

        public string Name { get; }

        public int TileWidth { get; }

        public int TileHeight { get; }

        public IReadOnlyCollection<ITilesetImage> Images { get; }

        public IReadOnlyCollection<ITerrainType> TerrainTypes { get; }

        public IReadOnlyDictionary<int, ITilesetTile> Tiles { get; }
        #endregion
    }
}

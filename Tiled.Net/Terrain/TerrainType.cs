using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiled.Net.Terrain
{
    public class TerrainType : ITerrainType
    {
        #region Constructors
        public TerrainType(int id, string name, int tile)
        {
            Id = id;
            Name = name;
            Tile = tile;
        }
        #endregion

        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Tile { get; private set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiled.Net.Terrain
{
    public interface ITerrainType
    {
        #region Properties
        int Id { get; }

        string Name { get; }

        int Tile { get; }
        #endregion
    }
}

using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Layers;

namespace Tiled.Net.Dto.Layers
{
    public sealed class MapLayer : IMapLayer
    {
        #region Fields

        private readonly IReadOnlyDictionary<int, IDictionary<int, IMapLayerTile>> _tiles;
        #endregion

        #region Constructors
        public MapLayer(string name, int width, int height, IDictionary<int, IDictionary<int, IMapLayerTile>> tiles)
        {
            Name = name;
            Width = width;
            Height = height;
            _tiles = tiles.ToDictionary(x => x.Key, x => x.Value);
        }
        #endregion

        #region Properties
        public string Name { get; }

        public int Width { get; }

        public int Height { get; }

        #endregion

        #region Methods
        public IMapLayerTile GetTile(int x, int y)
        {
            return _tiles[y][x];
        }
        #endregion
    }
}

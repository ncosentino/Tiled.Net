using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Tiled.Net.Layers
{
    public class MapLayer : IMapLayer
    {
        #region Fields
        private readonly string _name;
        private readonly int _width;
        private readonly int _heighth;
        private readonly IDictionary<int, IDictionary<int, IMapLayerTile>> _tiles;
        #endregion

        #region Constructors
        public MapLayer(string name, int width, int height, IDictionary<int, IDictionary<int, IMapLayerTile>> tiles)
        {
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentOutOfRangeException>(width >= 0, "width");
            Contract.Requires<ArgumentOutOfRangeException>(height >= 0, "height");
            Contract.Requires<ArgumentNullException>(tiles != null, "tiles");

            _name = name;
            _width = width;
            _heighth = height;
            _tiles = tiles; // FIXME: should probably create a copy...
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _heighth; }
        }
        #endregion

        #region Methods
        public IMapLayerTile GetTile(int x, int y)
        {
            return _tiles[y][x];
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_name != null);
            Contract.Invariant(_heighth >= 0);
            Contract.Invariant(_width >= 0);
            Contract.Invariant(_tiles != null);
        }
        #endregion
    }
}

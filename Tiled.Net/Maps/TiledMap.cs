using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiled.Net.Layers;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Maps
{
    public class TiledMap : ITiledMap
    {
        #region Fields
        private readonly int _width;
        private readonly int _height;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly List<ITileset> _tilesets;
        private readonly List<IMapLayer> _layers;
        private readonly List<IObjectLayer> _objectLayers;
        #endregion

        #region Constructors
        public TiledMap(
            int width, 
            int height, 
            int tileWidth, 
            int tileHeight, 
            IEnumerable<ITileset> tilesets, 
            IEnumerable<IMapLayer> layers, 
            IEnumerable<IObjectLayer> objectLayers)
        {
            _width = width;
            _height = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _tilesets = new List<ITileset>(tilesets);
            _layers = new List<IMapLayer>(layers);
            _objectLayers = new List<IObjectLayer>(objectLayers);
        }
        #endregion

        #region Properties
        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int TileWidth
        {
            get { return _tileWidth; }
        }

        public int TileHeight
        {
            get { return _tileHeight; }
        }

        public IEnumerable<ITileset> Tilesets
        {
            get { return _tilesets; }
        }

        public IEnumerable<IMapLayer> Layers
        {
            get { return _layers; }
        }

        public IEnumerable<IObjectLayer> ObjectLayers
        {
            get { return _objectLayers; }
        }
        #endregion
    }
}

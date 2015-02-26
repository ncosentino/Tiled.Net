using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiled.Net.Terrain;

namespace Tiled.Net.Tilesets
{
    public class Tileset : ITileset
    {
        #region Fields
        private readonly int _firstGid;
        private readonly string _name;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly List<ITilesetImage> _images;
        private readonly List<ITerrainType> _terrainTypes;
        private readonly List<ITilesetTile> _tiles;
        #endregion

        #region Constructors
        public Tileset(int firstGid, string name, int tileWidth, int tileHeight, IEnumerable<ITilesetImage> images, IEnumerable<ITerrainType> terrainTypes, IEnumerable<ITilesetTile> tiles)
        {
            _firstGid = firstGid;
            _name = name;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _images = new List<ITilesetImage>(images);
            _terrainTypes = new List<ITerrainType>(terrainTypes);
            _tiles = new List<ITilesetTile>(tiles);
        }
        #endregion

        #region Properties
        public int FirstGid
        {
            get { return _firstGid; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int TileWidth
        {
            get { return _tileWidth; }
        }

        public int TileHeight
        {
            get { return _tileHeight; }
        }

        public IEnumerable<ITilesetImage> Images
        {
            get { return _images; }
        }

        public IEnumerable<ITerrainType> TerrainTypes
        {
            get { return _terrainTypes; }
        }

        public IEnumerable<ITilesetTile> Tiles
        {
            get { return _tiles; }
        }
        #endregion
    }
}

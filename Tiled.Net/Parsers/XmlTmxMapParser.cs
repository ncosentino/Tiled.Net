using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Tiled.Net.Layers;
using Tiled.Net.Maps;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Parsers
{
    public class XmlTmxMapParser
    {
        #region Methods
        public ITiledMap ReadXml(string xmlTmxContents)
        {
            Contract.Requires<ArgumentNullException>(xmlTmxContents != null, "xmlTmxContents");
            Contract.Requires<ArgumentException>(xmlTmxContents.Trim().Length != 0, "xmlTmxContents");
            Contract.Ensures(Contract.Result<ITiledMap>() != null);

            using (var reader = XmlReader.Create(new StringReader(xmlTmxContents)))
            {
                return ReadXml(reader);
            }
        }

        public ITiledMap ReadXml(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<ITiledMap>() != null);

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "map")
                {
                    continue;
                }

                var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
                var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);
                var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
                var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

                List<ITileset> tilesets;
                List<IMapLayer> layers;
                List<IObjectLayer> objectLayers;
                ReadMapContents(
                    reader.ReadSubtree(),
                    out tilesets,
                    out layers,
                    out objectLayers);

                return new TiledMap(
                    width,
                    height,
                    tileWidth,
                    tileHeight,
                    tilesets,
                    layers,
                    objectLayers);
            }

            throw new FormatException("Could not find the map element.");
        }

        private void ReadMapContents(XmlReader reader, out List<ITileset> tilesets, out List<IMapLayer> layers, out List<IObjectLayer> objectLayers)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");

            tilesets = new List<ITileset>();
            layers = new List<IMapLayer>();
            objectLayers = new List<IObjectLayer>();

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "tileset":
                        tilesets.Add(ReadTileset(reader));
                        break;
                    case "layer":
                        layers.Add(ReadLayer(reader));
                        break;
                    case "objectgroup":
                        objectLayers.Add(ReadObjectLayer(reader));
                        break;
                } 
            }
        }

        private IObjectLayer ReadObjectLayer(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<IObjectLayer>() != null);

            var layerName = reader.GetAttribute("name");
            
            var objects = ReadMapObjects(reader.ReadSubtree());

            return new ObjectLayer(
                layerName, 
                objects);
        }

        private IEnumerable<ITiledMapObject> ReadMapObjects(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<IEnumerable<ITiledMapObject>>() != null);

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "object")
                {
                    continue;
                }

                var id = reader.GetAttribute("id");
                var objectName = reader.GetAttribute("name");
                var type = reader.GetAttribute("type");
                var gid = int.Parse(reader.GetAttribute("gid"), CultureInfo.InvariantCulture);
                var x = float.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
                var y = float.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);

                var widthAttr = reader.GetAttribute("width");
                var width = widthAttr == null
                    ? (float?)null
                    : float.Parse(widthAttr, CultureInfo.InvariantCulture);

                var heightAttr = reader.GetAttribute("height");
                var height = heightAttr == null
                    ? (float?)null
                    : float.Parse(heightAttr, CultureInfo.InvariantCulture);

                var properties = ReadTilesetTileProperties(reader.ReadSubtree());

                yield return new TiledMapObject(
                    id,
                    objectName,
                    type,
                    gid,
                    x,
                    y,
                    width,
                    height,
                    properties);
            }
        }

        private IMapLayer ReadLayer(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<IMapLayer>() != null);

            var layerName = reader.GetAttribute("name");
            var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
            var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);

            var tiles = new Dictionary<int, IDictionary<int, IMapLayerTile>>();

            int row = 0;
            int column = -1;
            foreach (var tile in ReadLayerTiles(reader.ReadSubtree()))
            {
                if (row % width == 0)
                {
                    column++;
                    row = 0;

                    tiles[column] = new Dictionary<int, IMapLayerTile>();
                }

                tiles[column][row] = tile;
                row++;
            }

            return new MapLayer(
                layerName, 
                width, 
                height, 
                tiles);
        }

        private IEnumerable<IMapLayerTile> ReadLayerTiles(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<IEnumerable<IMapLayerTile>>() != null);

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "tile")
                {
                    continue;
                }

                var gid = int.Parse(reader.GetAttribute("gid"), CultureInfo.InvariantCulture);

                yield return new MapLayerTile(gid);
            }
        }

        private ITileset ReadTileset(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<ITileset>() != null);

            var firstGid = int.Parse(reader.GetAttribute("firstgid"), CultureInfo.InvariantCulture);
            var tilesetName = reader.GetAttribute("name");
            var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
            var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

            List<ITilesetImage> images;
            List<ITilesetTile> tiles;
            ReadTilesetContent(reader.ReadSubtree(), out images, out tiles);

            return new Tileset(
                firstGid,
                tilesetName,
                tileWidth,
                tileHeight,
                images,
                tiles);
        }

        private void ReadTilesetContent(XmlReader reader, out List<ITilesetImage> images, out List<ITilesetTile> tiles)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");

            images = new List<ITilesetImage>();
            tiles = new List<ITilesetTile>();

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "image":
                        images.Add(ReadTilesetImage(reader));
                        break;
                    case "tile":
                        tiles.Add(ReadTilesetTile(reader));
                        break;
                    default:
                        break;
                }
            }
        }

        private ITilesetImage ReadTilesetImage(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<ITilesetImage>() != null);

            var entrySource = reader.GetAttribute("source");
            var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
            var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);

            return new TilesetImage(
                entrySource,
                width,
                height);
        }

        private ITilesetTile ReadTilesetTile(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<ITilesetTile>() != null);

            var id = int.Parse(reader.GetAttribute("id"), CultureInfo.InvariantCulture);
            var properties = ReadTilesetTileProperties(reader.ReadSubtree());

            return new TilesetTile(
                id,
                properties);
        }

        private IEnumerable<KeyValuePair<string, string>> ReadTilesetTileProperties(XmlReader reader)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader");
            Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<string, string>>>() != null);

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "property")
                {
                    continue;
                }

                var propertyName = reader.GetAttribute("name");
                var propertyValue = reader.GetAttribute("value");
                
                yield return new KeyValuePair<string, string>(
                    propertyName, 
                    propertyValue);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

using Tiled.Net.Dto.Layers;
using Tiled.Net.Dto.Maps;
using Tiled.Net.Dto.Terrain;
using Tiled.Net.Dto.Tilesets;
using Tiled.Net.Layers;
using Tiled.Net.Maps;
using Tiled.Net.Parsers;
using Tiled.Net.Terrain;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Tmx.Xml
{
    public sealed class XmlTmxMapParser : IMapParser
    {
        #region Methods

        public ITiledMap ParseMap(Stream resourceStream)
        {
            using (var reader = new XmlTextReader(resourceStream))
            {
                return ReadXml(reader);
            }
        }

        private ITiledMap ReadXml(string xmlTmxContents)
        {
            using (var reader = XmlReader.Create(new StringReader(xmlTmxContents)))
            {
                return ReadXml(reader);
            }
        }

        private ITiledMap ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "map")
                {
                    continue;
                }

                var renderOrder = reader.GetAttribute("renderorder");
                var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
                var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);
                var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
                var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

                ReadMapContents(
                    reader.ReadSubtree(),
                    out var tilesets,
                    out var layers,
                    out var objectLayers,
                    out var properties);

                return new TiledMap(
                    width,
                    height,
                    tileWidth,
                    tileHeight,
                    renderOrder,
                    tilesets,
                    layers,
                    objectLayers,
                    properties);
            }

            throw new FormatException("Could not find the map element.");
        }

        private void ReadMapContents(
            XmlReader reader,
            out List<ITileset> tilesets,
            out List<IMapLayer> layers,
            out List<IObjectLayer> objectLayers,
            out IReadOnlyDictionary<string, object> properties)
        {
            tilesets = new List<ITileset>();
            layers = new List<IMapLayer>();
            objectLayers = new List<IObjectLayer>();
            properties = new Dictionary<string, object>();

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
                    case "properties":
                        properties = ReadProperties(reader.ReadSubtree())
                            .ToDictionary(x => x.Key, x => x.Value);
                        break;
                }
            }
        }

        private IObjectLayer ReadObjectLayer(XmlReader reader)
        {
            var layerName = reader.GetAttribute("name");
            
            var objects = ReadMapObjects(reader.ReadSubtree());

            return new ObjectLayer(
                layerName, 
                objects);
        }

        private IEnumerable<ITiledMapObject> ReadMapObjects(XmlReader reader)
        {
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

                var gidhAttr = reader.GetAttribute("gid");
                var gid = gidhAttr == null
                    ? (int?)null
                    : int.Parse(gidhAttr, CultureInfo.InvariantCulture);
                
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

                var properties = ReadProperties(reader.ReadSubtree());

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
            var firstGid = int.Parse(reader.GetAttribute("firstgid"), CultureInfo.InvariantCulture);
            var tilesetName = reader.GetAttribute("name");
            var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
            var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

            List<ITilesetImage> images;
            List<ITilesetTile> tiles;
            List<ITerrainType> terrainTypes;
            ReadTilesetContent(reader.ReadSubtree(), out images, out tiles, out terrainTypes);
            
            return new Tileset(
                firstGid,
                tilesetName,
                tileWidth,
                tileHeight,
                images,
                terrainTypes,
                tiles);
        }

        private void ReadTilesetContent(XmlReader reader, out List<ITilesetImage> images, out List<ITilesetTile> tiles, out List<ITerrainType> terrainTypes)
        {
            images = new List<ITilesetImage>();
            tiles = new List<ITilesetTile>();
            terrainTypes = new List<ITerrainType>();

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
                    case "terraintypes":
                        terrainTypes.AddRange(ReadTilesetTerrainTypes(reader.ReadSubtree(), terrainTypes.Count));
                        break;
                    default:
                        break;
                }
            }
        }

        private IEnumerable<ITerrainType> ReadTilesetTerrainTypes(XmlReader reader, int startId)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "terrain")
                {
                    continue;
                }

                yield return ReadTilesetTerrainType(reader, startId++);
            }
        }

        private ITerrainType ReadTilesetTerrainType(XmlReader reader, int id)
        {
            var name = reader.GetAttribute("name");
            var tile = int.Parse(reader.GetAttribute("tile"), CultureInfo.InvariantCulture);

            return new TerrainType(
                id,
                name,
                tile);
        }

        private ITilesetImage ReadTilesetImage(XmlReader reader)
        {
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
            var id = int.Parse(reader.GetAttribute("id"), CultureInfo.InvariantCulture);

            var terrainAttribute = reader.GetAttribute("terrain");
            var terrainCorners = string.IsNullOrEmpty(terrainAttribute)
                ? new[] { -1, -1, -1, -1 }
                : terrainAttribute.Split(',').Select(x => x == string.Empty ? -1 : int.Parse(x, CultureInfo.InvariantCulture)).ToArray();

            var properties = ReadProperties(reader.ReadSubtree());

            return new TilesetTile(
                id,
                properties,
                terrainCorners);
        }

        private IEnumerable<KeyValuePair<string, object>> ReadProperties(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    !"property".Equals(reader.Name, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var propertyName = reader.GetAttribute("name");
                var propertyValue = (object)reader.GetAttribute("value");
                if (propertyValue == null)
                {
                    var subTree = reader.ReadSubtree();
                    subTree.ReadToDescendant("property");
                    propertyValue = ReadProperties(subTree).ToDictionary(
                        x => x.Key,
                        x => x.Value);
                }
                
                yield return new KeyValuePair<string, object>(
                    propertyName, 
                    propertyValue);
            }
        }
        #endregion
    }
}

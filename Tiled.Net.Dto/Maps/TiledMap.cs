using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Layers;
using Tiled.Net.Maps;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Dto.Maps
{
    public sealed class TiledMap : ITiledMap
    {
        #region Constructors
        public TiledMap(
            int width, 
            int height, 
            int tileWidth, 
            int tileHeight,
            string renderOrder,
            IEnumerable<ITileset> tilesets, 
            IEnumerable<IMapLayer> layers, 
            IEnumerable<IObjectLayer> objectLayers,
            IEnumerable<KeyValuePair<string, object>> properties)
        {
            Width = width;
            Height = height;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            RenderOrder = renderOrder;
            Tilesets = tilesets.ToArray();
            Layers = layers.ToArray();
            ObjectLayers = objectLayers.ToArray();
            Properties = properties.ToDictionary(x => x.Key, x => x.Value);
        }
        #endregion

        #region Properties

        public string RenderOrder { get; }

        public int Width { get; }

        public int Height { get; }

        public int TileWidth { get; }

        public int TileHeight { get; }

        public IReadOnlyCollection<ITileset> Tilesets { get; }

        public IReadOnlyCollection<IMapLayer> Layers { get; }

        public IReadOnlyCollection<IObjectLayer> ObjectLayers { get; }

        public IReadOnlyDictionary<string, object> Properties { get; }
        #endregion
    }
}

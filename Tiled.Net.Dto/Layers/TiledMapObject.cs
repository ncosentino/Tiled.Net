using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Layers;

namespace Tiled.Net.Dto.Layers
{
    public class TiledMapObject : ITiledMapObject
    {
        #region Constructors
        public TiledMapObject(
            string id,
            string name,
            string type,
            int? gid,
            float x,
            float y,
            float? width,
            float? height,
            IEnumerable<KeyValuePair<string, object>> properties)
        {
            Id = id;
            Name = name;
            Type = type;
            Gid = gid;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Properties = properties.ToDictionary(p => p.Key, p => p.Value);
        }
        #endregion

        #region Properties
        public string Id { get; }

        public int? Gid { get; }

        public string Name { get; }

        public string Type { get; }

        public float X { get; }

        public float Y { get; }

        public float? Width { get; }

        public float? Height { get; }
        
        public IReadOnlyDictionary<string, object> Properties { get; }
        #endregion
        
        #region Methods
        public object GetPropertyValue(string propertyName)
        {
            return Properties.ContainsKey(propertyName)
                ? Properties[propertyName]
                : null;
        }
        #endregion
    }
}

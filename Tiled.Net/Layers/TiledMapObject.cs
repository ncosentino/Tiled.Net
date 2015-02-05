using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Tiled.Net.Layers
{
    public class TiledMapObject : ITiledMapObject
    {
        #region Fields
        private readonly string _id;
        private readonly int? _gid;
        private readonly string _name;
        private readonly string _type;
        private readonly float _x;
        private readonly float _y;
        private readonly float? _width;
        private readonly float? _height;
        private readonly Dictionary<string, string> _properties;
        #endregion

        #region Constructors
        public TiledMapObject(string id, string name, string type, int? gid, float x, float y, float? width, float? height, IEnumerable<KeyValuePair<string, string>> properties)
        {
            Contract.Requires<ArgumentNullException>(id != null, "id");
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentNullException>(type != null, "type");
            Contract.Requires<ArgumentOutOfRangeException>(gid == null || gid >= 0, "gid");
            Contract.Requires<ArgumentOutOfRangeException>(width == null || width >= 0, "width");
            Contract.Requires<ArgumentOutOfRangeException>(height == null || height >= 0, "height");
            Contract.Requires<ArgumentNullException>(properties != null, "properties");

            _id = id;
            _name = name;
            _type = type;
            _gid = gid;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _properties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var property in properties)
            {
                _properties[property.Key] = property.Value;
            }
        }
        #endregion

        #region Properties
        public string Id
        {
            get { return _id; }
        }

        public int? Gid
        {
            get { return _gid; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Type
        {
            get { return _type; }
        }

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float? Width
        {
            get { return _width; }
        }

        public float? Height
        {
            get { return _height; }
        }

        public IEnumerable<string> PropertyNames
        {
            get { return _properties.Keys; }
        }

        public IEnumerable<KeyValuePair<string, string>> Properties
        {
            get { return _properties; }
        }
        #endregion
        
        #region Methods
        public string GetPropertyValue(string propertyName)
        {
            return _properties.ContainsKey(propertyName)
                ? _properties[propertyName]
                : null;
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_id != null);
            Contract.Invariant(_name != null);
            Contract.Invariant(_type != null);
            Contract.Invariant(_gid == null || _gid >= 0);
            Contract.Invariant(_width == null || _width >= 0);
            Contract.Invariant(_height == null || _height >= 0);
            Contract.Invariant(_properties != null);
        }
        #endregion
    }
}

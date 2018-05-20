using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Dto.Tilesets
{
    public class TilesetTile : ITilesetTile
    {
        #region Fields
        private readonly IReadOnlyList<int> _cornerTerrainIds; 
        #endregion

        #region Constructors
        public TilesetTile(
            int id,
            IEnumerable<KeyValuePair<string, string>> properties,
            IEnumerable<int> cornerTerrainIds)
        {
            Id = id;
            _cornerTerrainIds = cornerTerrainIds.ToArray();

            Properties = properties.ToDictionary(x => x.Key, x => x.Value);
        }
        #endregion

        #region Properties
        public int Id { get; }
        
        public IReadOnlyDictionary<string, string> Properties { get; }
        #endregion

        #region Methods
        public string GetPropertyValue(string propertyName)
        {
            return Properties.ContainsKey(propertyName)
                ? Properties[propertyName]
                : null;
        }

        public int GetTerrainId(int cornerIndex) => _cornerTerrainIds[cornerIndex];
        #endregion
    }
}

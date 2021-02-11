using System.Collections.Generic;

namespace Tiled.Net.Tilesets
{
    public interface ITilesetTile
    {
        #region Properties

        int Id { get; }

        IReadOnlyDictionary<string, object> Properties { get; }

        #endregion

        #region Methods

        object GetPropertyValue(string propertyName);

        int GetTerrainId(int cornerIndex);

        #endregion
    }
}

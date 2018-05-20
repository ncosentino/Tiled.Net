using System.Collections.Generic;

namespace Tiled.Net.Tilesets
{
    public interface ITilesetTile
    {
        #region Properties

        int Id { get; }

        IReadOnlyDictionary<string, string> Properties { get; }

        #endregion

        #region Methods

        string GetPropertyValue(string propertyName);

        int GetTerrainId(int cornerIndex);

        #endregion
    }
}

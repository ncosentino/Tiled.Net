using System.Collections.Generic;

namespace Tiled.Net.Layers
{
    public interface ITiledMapObject
    {
        #region Properties
        string Id { get; }

        int? Gid { get; }

        string Name { get; }

        string Type { get; }

        float X { get; }

        float Y { get; }

        float? Width { get; }

        float? Height { get; }

        IReadOnlyDictionary<string, string> Properties { get; }
        #endregion

        #region Methods
        string GetPropertyValue(string propertyName);
        #endregion
    }
}

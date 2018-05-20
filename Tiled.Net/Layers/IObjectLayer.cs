using System.Collections.Generic;

namespace Tiled.Net.Layers
{
    public interface IObjectLayer
    {
        #region Properties
        string Name { get; }

        IReadOnlyCollection<ITiledMapObject> Objects { get; }
        #endregion
    }
}

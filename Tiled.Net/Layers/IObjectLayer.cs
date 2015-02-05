using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiled.Net.Layers
{
    public interface IObjectLayer
    {
        #region Properties
        string Name { get; }

        IEnumerable<ITiledMapObject> Objects { get; }
        #endregion
    }
}

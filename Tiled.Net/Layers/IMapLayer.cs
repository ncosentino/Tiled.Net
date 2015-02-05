using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiled.Net.Layers
{
    public interface IMapLayer
    {
        #region Properties
        string Name { get; }
        
        int Width { get; }
        
        int Height { get; }
        #endregion

        #region Methods
        IMapLayerTile GetTile(int x, int y);
        #endregion
    }
}

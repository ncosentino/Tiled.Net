using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Layers;

namespace Tiled.Net.Dto.Layers
{
    public class ObjectLayer : IObjectLayer
    {
        #region Constructors
        public ObjectLayer(string name, IEnumerable<ITiledMapObject> objects)
        {
            Name = name;
            Objects = objects.ToArray();
        }
        #endregion

        #region Properties
        public string Name { get; }

        public IReadOnlyCollection<ITiledMapObject> Objects { get; }
        #endregion
    }
}

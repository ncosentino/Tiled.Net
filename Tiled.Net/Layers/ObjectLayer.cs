using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Tiled.Net.Layers
{
    public class ObjectLayer : IObjectLayer
    {
        #region Fields
        private readonly string _name;
        private readonly List<ITiledMapObject> _objects;
        #endregion

        #region Constructors
        public ObjectLayer(string name, IEnumerable<ITiledMapObject> objects)
        {
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentNullException>(objects != null, "objects");

            _name = name;
            _objects = new List<ITiledMapObject>(objects);
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<ITiledMapObject> Objects
        {
            get { return _objects; }
        }
        #endregion

        #region Methods
        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_name != null);
            Contract.Invariant(_objects != null);
        }
        #endregion
    }
}

using Tiled.Net.Layers;

namespace Tiled.Net.Dto.Layers
{
    public class MapLayerTile : IMapLayerTile
    {
        #region Constructors
        public MapLayerTile(int gid)
        {
            Gid = gid;
        }
        #endregion

        #region Properties
        public int Gid { get; }

        #endregion
    }
}

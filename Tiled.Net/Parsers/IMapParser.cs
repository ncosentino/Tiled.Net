using System.IO;
using Tiled.Net.Maps;

namespace Tiled.Net.Parsers
{
    public interface IMapParser
    {
        ITiledMap ParseMap(Stream resourceStream);
    }
}
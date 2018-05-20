namespace Tiled.Net.Tilesets
{
    public interface ITilesetCache
    {
        ITileset ForGid(int gid);
    }
}
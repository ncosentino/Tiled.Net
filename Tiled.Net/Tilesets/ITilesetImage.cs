namespace Tiled.Net.Tilesets
{
    public interface ITilesetImage
    {
        #region Properties
        string SourcePath { get; }
        
        int Width { get; }
        
        int Height { get; }
        #endregion
    }
}

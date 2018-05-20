using Tiled.Net.Tilesets;

namespace Tiled.Net.Dto.Tilesets
{
    public class TilesetImage : ITilesetImage
    {
        #region Constructors
        public TilesetImage(string sourcePath, int width, int height)
        {
            SourcePath = sourcePath;
            Width = width;
            Height = height;
        }
        #endregion

        #region Properties
        public string SourcePath { get; }

        public int Width { get; }

        public int Height { get; }
        #endregion
    }
}

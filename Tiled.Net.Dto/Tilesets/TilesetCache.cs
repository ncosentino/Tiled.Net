using System;
using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Tilesets;

namespace Tiled.Net.Dto.Tilesets
{
    public sealed class TilesetCache : ITilesetCache
    {
        private readonly IReadOnlyCollection<ITileset> _tilesets;
        private readonly Dictionary<int, ITileset> _cache;

        public TilesetCache(IEnumerable<ITileset> tilesets)
        {
            _cache = new Dictionary<int, ITileset>();
            _tilesets = tilesets.ToArray();
        }

        public ITileset ForGid(int gid)
        {
            ITileset tileset;
            if (_cache.TryGetValue(gid, out tileset))
            {
                return tileset;
            }

            ITileset candidate = null;
            foreach (var entry in _tilesets)
            {
                // NOTE: not sure why, but the first 'gid' for a tileset is actually 1 more than the
                // actual first 'gid' in the tileset... it's dumb?
                if (entry.FirstGid - 1 <= gid)
                {
                    candidate = entry;
                }
                else
                {
                    break;
                }
            }

            if (candidate == null)
            {
                throw new InvalidOperationException($"No tileset with entry for gid '{gid}'.");
            }

            _cache[gid] = candidate;
            return candidate;
        }
    }
}
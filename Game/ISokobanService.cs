using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public interface ISokobanService
    {
        int LevelCount { get; set; }
        string GetMap(int levelNumber);
    }
}
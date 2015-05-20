using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using AnimatedTile;

namespace HubPicker.Animations
{
    public interface ITileAnimation
    {
        Storyboard GetStoryboard(Tile tile);
    }
}

using System;
using MonoTouch.UIKit;

namespace Currency
{
    public static class UIViewExtensions
    {
        public static void Width(this UIView view, float width)
        {
            var frame = view.Frame;
            frame.Width = width;
            view.Frame = frame;
        }
    }
}


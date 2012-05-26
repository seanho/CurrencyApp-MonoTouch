using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace Currency
{
    public static class UIViewControllerExtensions
    {
        public static RectangleF ContentFrame(this UIViewController controller)
        {
            var appFrame = UIScreen.MainScreen.ApplicationFrame;
            var navbarHeight = controller.NavigationController.NavigationBar.Frame.Size.Height;
            return new RectangleF(0, 0, appFrame.Size.Width, appFrame.Size.Height - navbarHeight);
        }
    }
}


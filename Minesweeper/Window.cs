using System;
using Gtk;

namespace Minesweeper
{
    public partial class Window : Gtk.Window
    {
        public Window() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }
    }
}

using System;
using Cairo;
using Gtk;

namespace Minesweeper
{
    public partial class RulesWindow : Gtk.Window
    {
        private Gdk.Pixbuf rulesImage;

        public RulesWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            ModifyBg(StateType.Normal, new Gdk.Color(40, 40, 40));
            DeleteEvent += delegate { this.Destroy(); };

            this.rulesImage = new Gdk.Pixbuf("rules.jpg");
            Image image1 = new Image(this.rulesImage);

            this.fixed6.Put(image1, 0, 0);

            Add(this.fixed6);
            this.ShowAll();
        }
    }
}

using Gtk;
using System;

namespace Minesweeper
{
    public partial class DefeatWindow : Window
    {
        private bool _OK_Clicked = false;

        public bool OK_Clicked
        {
            get => _OK_Clicked;
            set => _OK_Clicked = value;
        }

        public DefeatWindow() : base(WindowType.Toplevel)
        {
            this.Build();
            this.Hide();
        }

        public void ShowDefeatWindow(GameField gameField)
        {
            if (gameField.Size < 0)
                this.Show();
        }

        protected void OnClicked(object sender, EventArgs e)
        {
            this.OK_Clicked = true;
        }

        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
            this.OK_Clicked = true;
        }
    }
}

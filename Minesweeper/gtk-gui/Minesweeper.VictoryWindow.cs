
// This file has been generated by the GUI designer. Do not modify.
namespace Minesweeper
{
	public partial class VictoryWindow
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.Label label3;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox3;

		private global::Gtk.DrawingArea drawingarea19;

		private global::Gtk.Entry entryField;

		private global::Gtk.DrawingArea drawingarea17;

		private global::Gtk.HBox hbox1;

		private global::Gtk.DrawingArea drawingarea23;

		private global::Gtk.Button button3;

		private global::Gtk.DrawingArea drawingarea21;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Minesweeper.VictoryWindow
			this.WidthRequest = 250;
			this.HeightRequest = 250;
			this.Name = "Minesweeper.VictoryWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("Победа");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Container child Minesweeper.VictoryWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label();
			this.label3.WidthRequest = 150;
			this.label3.HeightRequest = 180;
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Вы выйграли!\n\nВведите ваше имя ниже и \nнажмите ОК.");
			this.label3.Justify = ((global::Gtk.Justification)(2));
			this.vbox1.Add(this.label3);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.label3]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.drawingarea19 = new global::Gtk.DrawingArea();
			this.drawingarea19.Name = "drawingarea19";
			this.hbox3.Add(this.drawingarea19);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.drawingarea19]));
			w2.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entryField = new global::Gtk.Entry();
			this.entryField.CanFocus = true;
			this.entryField.Name = "entryField";
			this.entryField.Text = global::Mono.Unix.Catalog.GetString("Введите имя...");
			this.entryField.IsEditable = true;
			this.entryField.InvisibleChar = '●';
			this.hbox3.Add(this.entryField);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.entryField]));
			w3.Position = 1;
			// Container child hbox3.Gtk.Box+BoxChild
			this.drawingarea17 = new global::Gtk.DrawingArea();
			this.drawingarea17.Name = "drawingarea17";
			this.hbox3.Add(this.drawingarea17);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.drawingarea17]));
			w4.Position = 2;
			this.vbox2.Add(this.hbox3);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.drawingarea23 = new global::Gtk.DrawingArea();
			this.drawingarea23.Name = "drawingarea23";
			this.hbox1.Add(this.drawingarea23);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.drawingarea23]));
			w6.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button3 = new global::Gtk.Button();
			this.button3.WidthRequest = 100;
			this.button3.HeightRequest = 30;
			this.button3.CanFocus = true;
			this.button3.Name = "button3";
			this.button3.UseUnderline = true;
			this.button3.Label = global::Mono.Unix.Catalog.GetString("ОК");
			this.hbox1.Add(this.button3);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button3]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.drawingarea21 = new global::Gtk.DrawingArea();
			this.drawingarea21.Name = "drawingarea21";
			this.hbox1.Add(this.drawingarea21);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.drawingarea21]));
			w8.Position = 2;
			this.vbox2.Add(this.hbox1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.vbox1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.vbox2]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			this.vbox1.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			this.button3.Clicked += new global::System.EventHandler(this.OnClicked);
		}
	}
}

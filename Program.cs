using Gtk;
using Gdk;
using System;
using PhoneWaveNamespace;

class SimpleWindow : Gtk.Window
{
    public SimpleWindow() : base("Simple GtkSharp Window")
    {
        PhoneWave phoneWave = new PhoneWave();

        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Application.Quit(); };

        Box vbox = new Box(Orientation.Vertical, 5);

        Pixbuf pixbuf = new Pixbuf("Assets/cat.jpg");
        Pixbuf scaledPixbuf = pixbuf.ScaleSimple(400, 200, InterpType.Bilinear);

        Image image = new Image(scaledPixbuf);
        vbox.PackStart(image, false, false, 0);

        Entry input = new Entry();
        vbox.PackStart(input, false, false, 0);

        Box hbox = new Box(Orientation.Horizontal, 5);

        Button buttonSubmit = new Button("Submit");
        buttonSubmit.Clicked += delegate
        {
            Console.WriteLine("Button clicked");
            Console.WriteLine("Input: " + input.Text);

            // Not where it should be, but it's just an example
            phoneWave.GenerateRandomImage("Assets/random.jpg");

            if (input.Text == "cat")
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Correct, it's a cat!");
                md.Run();
                md.Destroy();
            }
            else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Wrong!!!!!");
                md.Run();
                md.Destroy();
            }
        };

        Button buttonRegenerate = new Button("Regenerate");
        buttonRegenerate.Clicked += delegate
        {
            Console.WriteLine("I'm afraid nothing happens here.");
        };

        hbox.PackStart(buttonSubmit, true, true, 0);
        hbox.PackStart(buttonRegenerate, true, true, 0);

        vbox.PackStart(hbox, false, false, 0);

        Add(vbox);

        ShowAll();
    }

    static void Main()
    {
        Application.Init();
        new SimpleWindow();
        Application.Run();
    }
}

using Gtk;
using Gdk;
using System;
using PhoneWaveNamespace;

class SimpleWindow : Gtk.Window
{
    public SimpleWindow() : base("Simple GtkSharp Window")
    {
        PhoneWave phoneWave = new PhoneWave();

        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Application.Quit(); };

        phoneWave.GenerateRandomImage("Assets/random.jpg", 500, 500, 5);

        Fixed fixedContainer = new Fixed();

        Pixbuf pixbuf = new Pixbuf("Assets/captchaEx.png");
        Pixbuf scaledPixbuf = pixbuf.ScaleSimple(400, 200, InterpType.Bilinear);

        Pixbuf pixbufOverlay = new Pixbuf("Assets/random.jpg");
        Pixbuf scaledPixbufOverlay = pixbufOverlay.ScaleSimple(400, 200, InterpType.Bilinear);

        Image image = new Image(scaledPixbuf);

        Image overlay = new Image(scaledPixbufOverlay);

        fixedContainer.Put(image, 0, 0);
        fixedContainer.Put(overlay, 0, 0);

        Entry input = new Entry();
        input.WidthRequest = 400;
        fixedContainer.Put(input, 0, 220);

        Box hbox = new Box(Orientation.Horizontal, 5);

        Button buttonSubmit = new Button("Submit");
        buttonSubmit.WidthRequest = 190;
        buttonSubmit.Clicked += delegate
        {
            Console.WriteLine("Button clicked");
            Console.WriteLine("Input: " + input.Text);

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
        buttonRegenerate.WidthRequest = 190;
        buttonRegenerate.Clicked += delegate
        {
            phoneWave.GenerateRandomImage("Assets/random.jpg", 500, 500, 5);

            pixbufOverlay = new Pixbuf("Assets/random.jpg");
            scaledPixbufOverlay = pixbufOverlay.ScaleSimple(400, 200, InterpType.Bilinear);

            overlay.Pixbuf = scaledPixbufOverlay;
        };

        hbox.PackStart(buttonSubmit, false, false, 0);
        hbox.PackStart(buttonRegenerate, false, false, 0);

        hbox.WidthRequest = 400;

        fixedContainer.Put(hbox, 0, 260);

        Add(fixedContainer);

        ShowAll();
    }

    static void Main()
    {
        Application.Init();
        new SimpleWindow();
        Application.Run();
    }
}

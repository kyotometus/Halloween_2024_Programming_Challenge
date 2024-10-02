using Gtk;
using Gdk;
using System;
using PhoneWaveNamespace;

class SimpleWindow : Gtk.Window
{
    private string generatedCaptchaText;
    private PhoneWave phoneWave;
    private Image overlay;

    // GUI Constructor
    public SimpleWindow() : base("Mommy Kurisu Makise Captcha")
    {
        phoneWave = new PhoneWave();

        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        Resizable = false;
        DeleteEvent += delegate { Application.Quit(); };

        // This function takes care of generating the captcha image and overlaying it on the window
        generatedCaptchaText = phoneWave.GenerateRandomCaptchaWithOverlay("Assets/captcha.png", 500, 200);

        Fixed fixedContainer = new Fixed();

        Pixbuf pixbufCaptcha = new Pixbuf("Assets/captcha.png");
        Pixbuf scaledPixbufCaptcha = pixbufCaptcha.ScaleSimple(400, 200, InterpType.Bilinear);

        overlay = new Image(scaledPixbufCaptcha);

        fixedContainer.Put(overlay, 0, 0);

        Entry input = new Entry();
        input.WidthRequest = 400;
        fixedContainer.Put(input, 0, 220);

        Box hbox = new Box(Orientation.Horizontal, 5);

        // This is the ultra complex security system
        // Literally impossible to crack
        Button buttonSubmit = new Button("Submit");
        buttonSubmit.WidthRequest = 190;
        buttonSubmit.Clicked += delegate
        {
            Console.WriteLine("Button clicked");
            Console.WriteLine("Input: " + input.Text);

            if (input.Text == generatedCaptchaText)
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "You now have access to LaurieWired Youtube channel!");
                md.Run();
                md.Destroy();
            }
            else if (input.Text == "LaurieWired" || input.Text == "lauriewired")
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Weird flex but ok");
                md.Run();
                md.Destroy();
            }
            else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Get some glasses!!!");
                md.Run();
                md.Destroy();
            }
        };

        // In case the user has some skill issues
        Button buttonRegenerate = new Button("Regenerate");
        buttonRegenerate.WidthRequest = 190;
        buttonRegenerate.Clicked += delegate
        {
            generatedCaptchaText = phoneWave.GenerateRandomCaptchaWithOverlay("Assets/captcha.png", 500, 200);

            Pixbuf newPixbufCaptcha = new Pixbuf("Assets/captcha.png");
            Pixbuf newScaledPixbufCaptcha = newPixbufCaptcha.ScaleSimple(400, 200, InterpType.Bilinear);

            overlay.Pixbuf = newScaledPixbufCaptcha;

            input.Text = "";
        };


        hbox.PackStart(buttonSubmit, false, false, 0);
        hbox.PackStart(buttonRegenerate, false, false, 0);

        hbox.WidthRequest = 400;

        fixedContainer.Put(hbox, 0, 260);

        Add(fixedContainer);

        ShowAll();
    }

    // AGI
    static void Main()
    {
        Application.Init();
        new SimpleWindow();
        Application.Run();
    }
}

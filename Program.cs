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

        // Generate captcha image in memory and overlay it on the window
        var result = phoneWave.GenerateRandomCaptchaWithOverlay(500, 200);
        generatedCaptchaText = result.captchaText;
        overlay = new Image(result.captchaPixbuf);

        Fixed fixedContainer = new Fixed();
        fixedContainer.Put(overlay, 0, 0);

        Entry input = new Entry();
        input.WidthRequest = 500;
        fixedContainer.Put(input, 0, 220);

        Box hbox = new Box(Orientation.Horizontal, 5);

        // This is the ultra complex security system
        // Literally impossible to crack
        Button buttonSubmit = new Button("Submit");
        buttonSubmit.WidthRequest = 250;
        buttonSubmit.Clicked += delegate
        {
            Console.WriteLine("Button clicked");
            Console.WriteLine("Input: " + input.Text);

            using (var md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "You now have access to LaurieWired Youtube channel!"))
            {
                if (input.Text == generatedCaptchaText)
                {
                    md.Text = "You now have access to LaurieWired Youtube channel!";
                }
                else if (input.Text == "LaurieWired" || input.Text == "lauriewired")
                {
                    md.Text = "Weird flex but ok";
                }
                else
                {
                    md.MessageType = MessageType.Error;
                    md.Text = "Get some glasses!!!";
                }
                md.Run();
            }
        };

        // In case the user has some skill issues
        Button buttonRegenerate = new Button("Regenerate");
        buttonRegenerate.WidthRequest = 250;
        buttonRegenerate.Clicked += delegate
        {
            var regenerateResult = phoneWave.GenerateRandomCaptchaWithOverlay(500, 200);
            generatedCaptchaText = regenerateResult.captchaText;
            overlay.Pixbuf = regenerateResult.captchaPixbuf;

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

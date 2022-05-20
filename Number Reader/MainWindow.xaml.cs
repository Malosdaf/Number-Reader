using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Number_Reader
{
    public enum UnitEnglish
    {
        Hundred,
        Thousand,
        Million,
        Billion,
        Trillion,
        Quadrillion,
        Quintillion
    }

    public enum Lang
    {
        Vietnamese,
        English
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EnglishReader englishReader = new EnglishReader();
        private VietnameseReader vietnameseReader = new VietnameseReader();
        private Lang CurrentLanguage = Lang.English;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HandleNumericInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int x);
        }

        private void TranslateToEnglish_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLanguage = Lang.English;
        }

        private void TranslateToVietnamese_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLanguage = Lang.Vietnamese;
        }

        private void Translate_Click(object sender, EventArgs e)
        {
            switch (CurrentLanguage)
            {
                case Lang.English:
                    Translate_English(TbInput.Text);
                    return;
                case Lang.Vietnamese:
                    Translate_Vietnamese(TbInput.Text);
                    return;
            }
        }

        private void Translate_English(string number)
        {
            TbOutput.Text = englishReader.Read(number);
        }

        private void Translate_Vietnamese(string number)
        {
            // change number to spelling
            TbOutput.Text = vietnameseReader.Read(number);
        }

        private void TbInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextToSpeak(object sender, EventArgs e)
        {
            Translate_Click(this, null);

            string speech = TbOutput.Text;
            switch (CurrentLanguage)
            {
                case Lang.English:
                    {
                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            using (SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer())
                            {
                                speechSynthesizer.Speak(speech);
                            }
                        }));
                        t.IsBackground = true;
                        t.Start();
                    }
                    return;
                case Lang.Vietnamese:
                    {
                        Thread t = new Thread(new ThreadStart(() => PlaySoundVietnamese(speech)));
                        t.IsBackground = true;
                        t.Start();
                    }
                    return;
            }
        }

        private void PlaySoundVietnamese(string numberText)
        {
            string[] numbers = numberText
                .Split(' ')
                .Select(x => x.ToLower())
                .ToArray();

            foreach (string num in numbers)
            {
                if (string.IsNullOrEmpty(num))
                {
                    continue;
                }

                SoundPlayer player = new SoundPlayer($"Sound/{num}Male.wav");
                player.PlaySync();
            }
        }


    }
}

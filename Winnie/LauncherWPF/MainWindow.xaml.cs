using Core;
using MGUI;
using Saver;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainWindowViewModel VM
        {
            get { return (MainWindowViewModel)this.DataContext; }
        }

        private void race_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image)sender;
            int index = Int16.Parse(im.Tag.ToString());

            VM.SelectRace(index);
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Title = "Import a saved game";
            dlg.DefaultExt = ".sw";
            dlg.Filter = "Small World Files (*.sw)|*.sw|All Files|*.*";

            if (Directory.Exists(MGUI.Saver.SavePath))
            {
                dlg.InitialDirectory = MGUI.Saver.SavePath;
            }

            bool? result = dlg.ShowDialog();
   
            if (result == true)
            {
                string filename = dlg.FileName;
                Stream s = File.OpenRead(filename);
                try
                {
                    Game model = Xml.XmlToGame(s);
                    s.Close();
                    s.Dispose();
                    startGame(model, 0);
                }
                catch (Exception)
                {
                    VM.ErrorMsg = "Cannot use this file as a saved game!";
                    VM.RaisePropertyChanged("ErrorMsg");
                }
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            int seed = 0;

            string a = VM.PlayerAName.Length == 0 ? "Player A" : VM.PlayerAName;
            string b = VM.PlayerBName.Length == 0 ? "Player B" : VM.PlayerBName;

            var p1 = new Player(a, VM.IntToRace(VM.PlayerARace));
            var p2 = new Player(b, VM.IntToRace(VM.PlayerBRace));

            object[] arguments = { p1, p2, VM.CheatMode, seed };

            // Here, we are going some reflexive stuff.
            // The goal is to have a dynamic generic type "GameType" for game creation.
            var GameModel = (Game)typeof(GameBuilder)
                .GetMethod("New")
                .MakeGenericMethod(VM.MapGameType, typeof(PerlinMap))
                .Invoke(null, arguments);

            startGame(GameModel, seed);
        }

        private void startGame(Game model, int seed)
        {
            this.Hide();
            GameCreator.New(model, seed);
            this.Show();
        }
    }
}

using System.ComponentModel;
using Core;

namespace Windows
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    class MainWindowViewModel : ViewModelBase
    {

        private static GameType[] gametypes = {
            new DemoGameType(),
            new SmallGameType(),
            new StandardGameType()
        };

        public MainWindowViewModel()
        {
            PlayerARace = 0;
            PlayerBRace = 1;
            PlayerAName = "";
            PlayerBName = "";
            ErrorMsg = "";
        }

        public string ErrorMsg { get; set; }

        public string MapLabel
        {
            get
            {
                GameType g = gametypes[(int)MapType];
                return g.Name + " (" + g.Size + " x " + g.Size + ")";
            }
        }

        public System.Type MapGameType
        {
            get
            {
                GameType g = gametypes[(int)MapType];
                return g.GetType();
            }
        }

        private double _mapType = 1;
        public double MapType
        {
            get { return this._mapType; }
            set { this._mapType = value; RaisePropertyChanged("MapLabel"); }
        }
        
        public bool CheatMode { get; set; }

        public string PlayerAName { get; set; }
        public string PlayerBName { get; set; }

        public int PlayerARace { get; set; }
        public int PlayerBRace { get; set; }

        public string PlayerARaceStr
        {
            get
            {
                return IntToRace(PlayerARace).GetType().Name;
            }
        }

        public string PlayerBRaceStr
        {
            get
            {
                return IntToRace(PlayerBRace).GetType().Name;
            }
        }

        public Race IntToRace(int i)
        {
            switch(i)
            {
                case 0: return Human.Instance;
                case 1: return Elf.Instance;
                default: return Orc.Instance;
            }
        }

        public double[] RacesOpacities
        {
            get
            {
                double[] array = new double[6];
                for (int i = 0; i < 6; i++)
                {
                    int chosenRace = i < 3 ? PlayerARace : PlayerBRace + 3;
                    array[i] = chosenRace == i ? 1 : 0.3;
                }
                return array;
            }
        }

        public bool SelectRace(int race)
        {
            bool ok = false;
            if (race < 3)
            {
                if (race != PlayerBRace)
                {
                    PlayerARace = race;
                    RaisePropertyChanged("PlayerARaceStr");
                    ok = true;
                }
            }
            else
            {
                if (race != PlayerARace + 3)
                {
                    PlayerBRace = race - 3;
                    RaisePropertyChanged("PlayerBRaceStr");
                    ok = true;
                }
            }

            ErrorMsg = ok ? "" : "Cannot have the same race!";
            RaisePropertyChanged("ErrorMsg");
            RaisePropertyChanged("RacesOpacities");
            return ok;
        }

    }
}

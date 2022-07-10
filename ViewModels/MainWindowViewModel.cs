using DesktopPet.Events;
using DesktopPet.Models;
using DesktopPet.Services;
using DesktopPet.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DesktopPet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly JsonService jsonService = new JsonService();
        private string _title = "Desktop Pet";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _imageNow = @"\Views\Resources\Icons\Icon.png";
        public string ImageNow
        {
            get { return _imageNow; }
            set { SetProperty(ref _imageNow, value); }
        }


        public DelegateCommand CloseButtonCommand { get; private set; }
        public DelegateCommand CharactersButtonCommand { get; private set; }


        private readonly IEventAggregator eventAggregator;


        // 宠物详情窗体
        CharactersWindow charactersWindow { get; set; } = null;


        // 改变宠物执行
        private void OnPetChanged(Pet pet)
        {
            this.ImageNow = "\\Resources\\Loading.gif";
            SettingsHolder.settings.Pet = pet;
            jsonService.SaveSettings();
            Properties.Settings.Default.SelectedPetIndex = charactersWindow.petListBox.SelectedIndex;
            Properties.Settings.Default.Save();
            this.ImageNow = SettingsHolder.settings.Pet.ImageSource[Moves.Stand];
        }
        // CharacterWindowClose窗体关闭
        private void OnCharactersWindowClose(string arg)
        {
            charactersWindow?.Close();
            charactersWindow = null;
        }

        public MainWindowViewModel(IEventAggregator _eventAggregator)
        {
            IInitService initService = new InitService();
            // 如果是第一次执行该程序
            if (Properties.Settings.Default.FirstStart)
            {
                Properties.Settings.Default.FirstStart = false;
                initService.FirstInit();
            }
            jsonService.GetSettings();
            initService.EnvironmentCheck();
            this.ImageNow = SettingsHolder.settings.Pet.ImageSource[Moves.Stand];
            this.eventAggregator = _eventAggregator;
            eventAggregator.GetEvent<MainWindowPetChangedEvent>().Subscribe(OnPetChanged);
            eventAggregator.GetEvent<WindowCloseEvent>().Subscribe(OnCharactersWindowClose, filter: arg =>
            {
                if (arg == "CharactersWindow") return true;
                else return false;
            });
            // 生成宠物详情窗口
            this.CharactersButtonCommand = new DelegateCommand(() =>
            {
                // 判断窗体是否已经拥有一个实例
                if (charactersWindow == null)
                {
                    charactersWindow = new CharactersWindow();
                    charactersWindow.Show();
                }
                else
                {
                    charactersWindow.Activate();
                }

            });

            // 关闭按钮
            this.CloseButtonCommand = new DelegateCommand(() =>
            {
                Application.Current.Shutdown();
                System.Environment.Exit(0);
            });
        }
    }
}

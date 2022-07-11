using DesktopPet.Events;
using DesktopPet.Models;
using DesktopPet.Services;
using DesktopPet.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

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
        private string _imageNow;
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
            // 程序初始化/数据读取
            jsonService.GetSettings();
            initService.EnvironmentCheck();
            this.ImageNow = SettingsHolder.settings.Pet.ImageSource[Moves.Stand];

            // 订阅信息
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
                charactersWindow = new CharactersWindow();
                charactersWindow.ShowDialog();
                charactersWindow = null;

            });

            // 关闭按钮
            this.CloseButtonCommand = new DelegateCommand(() =>
            {
                System.Environment.Exit(0);
            });
        }
    }
}

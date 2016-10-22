using mastermind.body;
using mastermind.contracts;
using mastermind.game;
using mastermind.view;
using mastermind.view.viewmodel;
using nblackbox;
using nblackbox.contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace mastermind.app
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //create
            IBlackBox blackBox = new FolderBlackBox(@"V:\Mastermind Events\" + "EventStore");

            Random random = new Random();
            CodeGenerator code_generator = new CodeGenerator(random);
            Coder coder = new Coder(blackBox, code_generator);
            Hacker hacker = new Hacker(blackBox);
            CodeAnalyzer code_analyzer = new CodeAnalyzer(blackBox);
            Referee referee = new Referee(blackBox);
            GameStats stats = new GameStats(blackBox);
            IBody body = new Body(blackBox, coder, hacker, code_analyzer, referee, stats);

            MainWindow main_view = new MainWindow();
            StatsWindow stats_view = new StatsWindow();
            SecretCodeViewModel secret_code_vm = new SecretCodeViewModel();
            MainViewModel main_vm = new MainViewModel(body, secret_code_vm);
            StatsViewModel stats_vm = new StatsViewModel();

            //bind

            //Mapper benötigt
            stats.OnCreated += _stats =>
            {
                stats_vm.Winner = _stats.Winner;
                stats_vm.Duration = _stats.Duration;
                stats_view.Run();
            };

            //inject
            main_view.Inject(main_vm);
            stats_view.Inject(stats_vm);

            //run
            main_view.Run();
        }
    }
}

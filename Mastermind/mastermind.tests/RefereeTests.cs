using mastermind.contracts.events;
using mastermind.game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nblackbox;
using nblackbox.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.tests
{
    [TestClass]
    public class RefereeTests
    {
        IBlackBox _blackBox;
        Referee _sut;

        [TestInitialize]
        public void ClassInitialize()
        {
            _blackBox = new FolderBlackBox(Environment.CurrentDirectory + "blackBoxTest");
            _sut = new Referee(_blackBox);
        }

        [TestMethod]
        public void Secrect_Code_hacked()
        {
            var games = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play();
            var game_context = games.Count().ToString();

            _blackBox.Record(new BlackPinsLocated(game_context, "BlackPinsLocated", "Yellow,Blue,Black,Red"));

            _sut.Is_Secrect_Code_hacked(() => Assert.IsTrue(true), () => Assert.Fail());
        }

        [TestMethod]
        public void Secrect_Code_not_hacked()
        {
            var games = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play();
            var game_context = games.Count().ToString();

            _blackBox.Record(new BlackPinsLocated(game_context, "BlackPinsLocated", "Yellow,Black,Red"));

            _sut.Is_Secrect_Code_hacked(() => Assert.Fail(), () => Assert.IsTrue(true));
        }

        [TestMethod]
        public void Hackers_trys_consumed()
        {
            _blackBox.Record(new NewGameStarted("", "NewGameStarted", ""));
            var context = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play().Count().ToString();

            for (int i = 0; i < 10; i++)
                _blackBox.Record(new TryPlaced(context, "TryPlaced", "Red,Black,Yellow,Green"));

            _sut.Is_Hackers_try_consumed(() => Assert.IsTrue(true), () => Assert.Fail(), context);
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            _sut = null;
            _blackBox = null;
        }
    }
}

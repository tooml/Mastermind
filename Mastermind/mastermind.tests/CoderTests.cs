using mastermind.contracts.data;
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
    public class CoderTests
    {
        IBlackBox _blackBox;
        Coder _sut;

        [TestInitialize]
        public void ClassInitialize()
        {
            _blackBox = new FolderBlackBox(Environment.CurrentDirectory + "blackBoxTest");
            _sut = new Coder(_blackBox, new CodeGenerator(new Random()));
        }

        [TestMethod]
        public void Register_SecretCode()
        {
            var games = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play();
            var game_context = games.Count().ToString();
            _blackBox.Record(new NewGameStarted(game_context, "NewGameStarted", ""));

            _sut.Register_SecretCode(new Pin[4] { Pin.Black, Pin.Blue, Pin.Brown, Pin.Green });

            var events = _blackBox.Player.ForEvent(typeof(SecretCodeGenerated).Name).Play();
            var pins = events.Last().Data.Split(',');

            var first_pin = Enum.Parse(typeof(Pin), pins.ElementAt(0));
            var last_pin = Enum.Parse(typeof(Pin), pins.ElementAt(3));

            Assert.AreEqual(Pin.Black, first_pin);
            Assert.AreEqual(Pin.Green, last_pin);
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            _sut = null;
            _blackBox = null;
        }
    }
}

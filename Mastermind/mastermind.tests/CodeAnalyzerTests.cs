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
    public class CodeAnalyzerTests
    {
        CodeAnalyzer _sut;
        IBlackBox _blackBox;

        [TestInitialize]
        public void ClassInitialize()
        {
            _blackBox = new FolderBlackBox(Environment.CurrentDirectory + "blackBoxTest");
            _sut = new CodeAnalyzer(_blackBox);
        }

        [TestMethod]
        public void Check_Black_Pins()
        {
            var games = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play();
            var game_context = games.Count().ToString();

            _blackBox.Record(new SecretCodeGenerated(game_context, "SecretCodeGenerated", "Black,Green,White,Yellow"));
            var secret_code_event = _blackBox.Player.ForEvent("SecretCodeGenerated").Play().Last();
            var secret_code = secret_code_event.Data.Split(',');

            _blackBox.Record(new TryPlaced(game_context, "TryPlaced", "Black,Green,White,Yellow"));
            var last_try_event = _blackBox.Player.ForEvent("TryPlaced").Play().Last();
            var last_try = last_try_event.Data.Split(',');
            _sut.Locate_right_placed_Pins(secret_code, last_try, game_context);
            var result = _blackBox.Player.ForEvent("BlackPinsLocated").Play().Last();

            Assert.AreEqual(4, result.Data.Split(',').Count());
            CollectionAssert.AreEqual(secret_code, result.Data.Split(','));

            _blackBox.Record(new TryPlaced(game_context, "TryPlaced", "Green,Green,White,Black"));
            last_try_event = _blackBox.Player.ForEvent("TryPlaced").Play().Last();
            last_try = last_try_event.Data.Split(',');
            _sut.Locate_right_placed_Pins(secret_code, last_try, game_context);
            result = _blackBox.Player.ForEvent("BlackPinsLocated").Play().Last();

            Assert.AreEqual(2, result.Data.Split(',').Count());
            CollectionAssert.AreEqual(new string[] {"Green", "White"} , result.Data.Split(','));
        }

        [TestMethod]
        public void Check_White_Pins()
        {
            var games = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play();
            var game_context = games.Count().ToString();

            _blackBox.Record(new SecretCodeGenerated(game_context, "SecretCodeGenerated", "Black,Green,White,Yellow"));
            var secret_code_event = _blackBox.Player.ForEvent("SecretCodeGenerated").Play().Last();
            var secret_code = secret_code_event.Data.Split(',');

            _blackBox.Record(new TryPlaced(game_context, "TryPlaced", "Black,Green,White,Yellow"));
            var last_try_event = _blackBox.Player.ForEvent("TryPlaced").Play().Last();
            var last_try = last_try_event.Data.Split(',');
            _sut.Locate_right_Color_Pins(secret_code, last_try, new string[] { "Black", "Green", "White", "Yellow" }, game_context);
            var result = _blackBox.Player.ForEvent("WhitePinsLocated").Play().Last();

            Assert.AreEqual(String.Empty, result.Data.Split(',').ElementAt(0));

            _blackBox.Record(new TryPlaced(game_context, "TryPlaced", "Yellow,Green,White,Red"));
            last_try_event = _blackBox.Player.ForEvent("TryPlaced").Play().Last();
            last_try = last_try_event.Data.Split(',');
            _sut.Locate_right_Color_Pins(secret_code, last_try, new string[] { "Green", "White" }, game_context);
            result = _blackBox.Player.ForEvent("WhitePinsLocated").Play().Last();

            Assert.AreEqual(1, result.Data.Split(',').Count());
            CollectionAssert.AreEqual(new string[] { "Yellow" }, result.Data.Split(','));
        }

        [TestMethod]
        public void Check_Combine_black_and_white_pins()
        {
            var result = _sut.Combine_black_and_white_pins(new string[] { "Black", "Red" }, 
                                                            new string[] { "Blue" });

            Assert.AreEqual(Pin.Black, result.Place_One);
            Assert.AreEqual(Pin.Black, result.Place_Two);
            Assert.AreEqual(Pin.White, result.Place_Three);
            Assert.AreEqual(Pin.None, result.Place_Four);

            result = _sut.Combine_black_and_white_pins(new string[] { },
                                                            new string[] { "Blue" });

            Assert.AreEqual(Pin.White, result.Place_One);
            Assert.AreEqual(Pin.None, result.Place_Two);
            Assert.AreEqual(Pin.None, result.Place_Three);
            Assert.AreEqual(Pin.None, result.Place_Four);
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            _sut = null;
            _blackBox = null;
        }
    }
}

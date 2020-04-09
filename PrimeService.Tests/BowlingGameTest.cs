using NUnit.Framework;
using Prime.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.UnitTests.Services
{
    public class BowlingGameTest 
    {
        [SetUp]
        public void Setup()
        {
            _game = new BowlingGame();
        }

        [TearDown]
        public void TearDown()
        {
            _game = null;
        }

        private BowlingGame _game;

        private void RollStrike()
        {
            _game.Roll(10);
        }
        private void RollSpare()
        {
            _game.Roll(5);
            _game.Roll(5);
        }

        [Test]
        public void Can_Get_Calculate_Full_Game_Scores()
        {
            for (int i = 0; i < 12; i++)
                RollStrike();
            Console.WriteLine("Roll Total - {0}, Result - {1}", 300, _game.Score());
            Assert.That(300, Is.EqualTo(_game.Score()));
        }


        [Test]
        public void Can_Get_Calculate_Scores()
        {
            for (int i = 0; i < 20; i++)
                _game.Roll(0);
            Assert.That(0, Is.EqualTo(_game.Score()));
        }

        [Test]
        public void Can_Get_Calculate_Single_Scores()
        {
            _game.Roll(7);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 7, _game.Score());
            Assert.That(7, Is.EqualTo(_game.Score()));
        }
        [Test]
        public void Can_Get_Calculate_SingleFrame_Scores()
        {
            _game.Roll(7);
            _game.Roll(2);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 9, _game.Score());
            Assert.That(9, Is.EqualTo(_game.Score()));
        }


        [Test]
        public void Can_Get_Calculate_Spare_Scores()
        {
            RollSpare();
            _game.Roll(3);
            _game.Roll(2);
            for (int i = 0; i < 16; i++)
                _game.Roll(0);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 18, _game.Score());
            Assert.That(18, Is.EqualTo(_game.Score()));
        }

        [Test]
        public void Can_Get_Calculate_Strike_Scores()
        {
            RollStrike();
            _game.Roll(3);
            _game.Roll(4);
            _game.Roll(2);
            for (int i = 0; i < 15; i++)
                _game.Roll(0);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 26, _game.Score());
            Assert.That(26, Is.EqualTo(_game.Score()));
        }
        [Test]
        public void Can_Get_Calculate_2Strikes_Score()
        {
            RollStrike();
            RollStrike();
            _game.Roll(3);
            _game.Roll(4);
            for (int i = 0; i < 14; i++)
                _game.Roll(0);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 47, _game.Score());
            Assert.That(47, Is.EqualTo(_game.Score()));
        }
        [Test]
        public void Can_Get_Calculate_2Spare_Score()
        {
            RollSpare();
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(6);
            _game.Roll(1);
            for (int i = 0; i < 14; i++)
                _game.Roll(0);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 36, _game.Score());
            Assert.That(36, Is.EqualTo(_game.Score()));
        }
        [Test]
        public void Can_Get_Calculate_3Strikes_Score()
        {
            RollStrike();
            RollStrike();
            RollStrike();
            _game.Roll(3);
            _game.Roll(4);
            for (int i = 0; i < 12; i++)
                _game.Roll(0);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 77, _game.Score());
            Assert.That(77, Is.EqualTo(_game.Score()));
        }
        [Test]
        public void Can_Get_Calculate_2Strikes1Spare_Score()
        {
            RollStrike();
            RollStrike();             
            _game.Roll(4);
            _game.Roll(6);
            _game.Roll(3);
            for (int i = 0; i < 13; i++)
                _game.Roll(0);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 60, _game.Score());
            Assert.That(60, Is.EqualTo(_game.Score()));
        }
        [Test]
        public void Can_Get_Calculate_1Spare_LastFrame_Score()
        {
            for (int i = 0; i < 18; i++)
                _game.Roll(0);
            RollSpare();
            _game.Roll(3);
            Console.WriteLine("Roll Total - {0}, Result - {1}", 13, _game.Score());
            Assert.That(13, Is.EqualTo(_game.Score()));

        }
        [Test]
        public void Can_Get_Calculate_3Strikes_LastFrame_Score()
        {
            for (int i = 0; i < 18; i++)
                _game.Roll(0);
            RollStrike();
            RollStrike();
            RollStrike();
            Console.WriteLine("Roll Total - {0}, Result - {1}", 30, _game.Score());
            Assert.That(30, Is.EqualTo(_game.Score()));

        }
    }
}

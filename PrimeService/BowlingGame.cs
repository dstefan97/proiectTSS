using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Services
{
//    The game consists of 10 frames, in each frame the player has the ability to knock down 10 pins.
//    The score for the frame is the total number of pins knocked down + bonuses for strikes and spares.
//    A spare is when the player knocks down all 10 pins in 2 tries.The bonus for a spare is the next roll.
//    A strike is when the player knocks down all 10 pins in 1 try. The bonus is the next 2 rolls.
//    In the tenth frame a player who rolls a spare / strike gets an extra roll(s) to complete the frame.
//    No more than 3 rolls can be rolled in the 10th frame.
    public class BowlingGame
    {
        private readonly int[] _rolls = new int[21];
        private int _currentRoll;

        public void Roll(int pins) => _rolls[_currentRoll++] = pins;

        public int Score()
        {
            var score = 0;
            var frameIndex = 0;
            for (var frame = 0; frame < 10; frame++)
            {
                if (IsStrike(frameIndex))
                {
                    score += StrikeBonus(frameIndex);
                    frameIndex++;
                }
                else if (IsSpare(frameIndex))
                {
                    score += 10 + SpareBonus(frameIndex);
                    frameIndex += 2;
                }

                else
                {
                    score += SumOfBallsInFrames(frameIndex);
                    frameIndex += 2;
                }
            }

            return score;
        }

        private bool IsStrike(int frameIndex) => _rolls[frameIndex] == 10;

        private int SumOfBallsInFrames(int frameIndex) => _rolls[frameIndex] + _rolls[frameIndex + 1];

        private int SpareBonus(int frameIndex) => _rolls[frameIndex + 2];

        private bool IsSpare(int frameIndex) => _rolls[frameIndex] + _rolls[frameIndex + 1] == 10;

        private int StrikeBonus(int frameIndex) => 10 + _rolls[frameIndex + 1] + _rolls[frameIndex + 2];


    }
}

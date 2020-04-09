using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Services
{
   
    //    Jocul este alcatuit din 10 faze, in fieare faza jucatorul poate darama 10 popice   
    //    Scorul pentru fiecare faza este numarul de popice daramate + bonus pentru strik si spare  
    //    Spare este atunci cand jucatorul darama 10 popice din 2 aruncari
    //    Strike este atunci cand jucatorul darama 10 popice din 1 aruncare
    //    In a 10-a faza un jucator poate sa dea 1 sau 2 aruncari in plus daca loveste un spare sau strike
    //    Se poate arunca de maxim 3 ori in faza a 10-a
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

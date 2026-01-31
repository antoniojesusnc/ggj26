using System.Collections.Generic;
using System.Text;
using MyBox;
using UnityEngine;

namespace ggj26
{
    public class RhythmGameController
    {
        public List<InputsBeat> InputsBeats { get; private set; } = new List<InputsBeat>();
        public RhythmGameConfig LevelConfig { get; private set; }
        
        public void Init(RhythmGameConfig rhythmGameConfig)
        {
            LevelConfig = rhythmGameConfig;
            GenerateLevels();
            DebugLevelGenerated();
        }

        private void DebugLevelGenerated()
        {
            var log = new StringBuilder();
            int lastBeat = 0;
            foreach (var inputsBeat in InputsBeats)
            {
                if ((lastBeat + 1) < inputsBeat.Beat)
                {
                    log.AppendLine($"Wait For: {inputsBeat.Beat}");
                }
                    
                log.AppendLine($"Beat {inputsBeat.Beat}, Input: {inputsBeat.Input}");
                lastBeat = inputsBeat.Beat;
            }
            Debug.Log(log.ToString());
        }

        private void GenerateLevels()
        {
            int temporalBeat = LevelConfig.InitialWait;
            int temporalWave = 1;
            for (int i = 0; i < LevelConfig.Waves; i++)
            {
                temporalWave = i + 1;
                if (i > 0)
                {
                    temporalBeat = AddWaitBeat(temporalBeat, temporalWave/(float)LevelConfig.Waves);
                }
                temporalBeat = AddInputs(temporalBeat, temporalWave/(float)LevelConfig.Waves);
            }
        }

        private int AddWaitBeat(int temporalBeat, float levelRate)
        {
            return temporalBeat + LevelConfig.BeatsBetweenInputsRange.Vector2IntLerpRate(levelRate);
        }

        private int AddInputs(int temporalBeat, float levelRate)
        {
            var inputsTogethers = LevelConfig.AmountInputsTogetherRange.Vector2IntLerpRate(levelRate);
            
            List<InputsTypes> inputsTogethersUsed = new ();
            for (int i = 0; i < inputsTogethers; i++)
            {
                var input = AddSingleInput(inputsTogethersUsed);
                inputsTogethersUsed.Add(input);
                InputsBeats.Add(new InputsBeat(temporalBeat, input));
                temporalBeat++;
            }

            return temporalBeat;
        }

        private InputsTypes AddSingleInput(List<InputsTypes> inputsTogethersUsed)
        {
            return LevelConfig.InputsInLevel.GetWeightedRandom((input) => WeightRate(input, inputsTogethersUsed));
        }

        private double WeightRate(InputsTypes input, List<InputsTypes> inputsTogethersUsed)
        {
            if (inputsTogethersUsed.Contains(input))
            {
                return LevelConfig.RateForRepeatInput;
            }
            else
            {
                return LevelConfig.RateForNewInput;
            }
        }
    }

    public class InputsBeat
    {
        public int Beat { get; private set; } 
        public InputsTypes Input { get; private set; }

        public InputsBeat(int beat, InputsTypes input)
        {
            Beat = beat;
            Input = input;
        }
    }
}

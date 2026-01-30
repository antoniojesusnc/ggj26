using System.Collections.Generic;
using MyBox;
using UnityEditorInternal;

namespace ggj26
{
    public class RhythmGameController
    {
        public List<InputsBeat> InputsBeats { get; private set; } = new List<InputsBeat>();

        private int currentBeat = 0;
        private RhythmGameConfig _rhythmGameConfig;
        
        public void Init(RhythmGameConfig rhythmGameConfig)
        {
            GenerateLevels();
        }

        private void GenerateLevels()
        {
            currentBeat = _rhythmGameConfig.InitialWait;
            for (int i = 0; i < _rhythmGameConfig.Waves; i++)
            {
                if (i > 0)
                {
                    AddWaitBeat(currentBeat/(float)_rhythmGameConfig.Waves);
                }
                AddInputs(currentBeat/(float)_rhythmGameConfig.Waves);
            }
        }

        private void AddWaitBeat(float levelRate)
        {
            currentBeat = _rhythmGameConfig.BeatsBetweenInputsRange.Vector2LerpRate(levelRate);
        }

        private void AddInputs(float levelRate)
        {
            var inputsTogethers = _rhythmGameConfig.AmountInputsTogetherRange.Vector2LerpRate(levelRate);
            
            List<InputsTypes> inputsTogethersUsed = new ();
            for (int i = 0; i < inputsTogethers; i++)
            {
                var input = AddSingleInput(inputsTogethersUsed);
                inputsTogethersUsed.Add(input);
                InputsBeats.Add(new InputsBeat(currentBeat, input));
                currentBeat++;
            }
        }

        private InputsTypes AddSingleInput(List<InputsTypes> inputsTogethersUsed)
        {
            return _rhythmGameConfig.InputsInLevel.GetWeightedRandom((input) => wightRate(input, inputsTogethersUsed));
        }

        private double wightRate(InputsTypes input, List<InputsTypes> inputsTogethersUsed)
        {
            if (inputsTogethersUsed.Contains(input))
            {
                return _rhythmGameConfig.RateForRepeatInput;
            }
            else
            {
                return _rhythmGameConfig.RateForNewInput;
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

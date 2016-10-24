using AudioSynthesis.Bank.Components.Generators;
using AudioSynthesis.Bank.Descriptors;

namespace AudioSynthesis.Bank.Components
{
    public class Lfo
    {
        private LfoStateEnum lfoState;
        private double phase;
        private double increment;
        private int delayTime;
        private Generator generator;

        public LfoStateEnum CurrentState
        {
            get { return lfoState; }
        }
        public double Frequency { get; private set; }
        public double Depth { get; set; }
        public double Value { get; set; }

        public void QuickSetup(int sampleRate,LfoDescriptor lfoInfo)
        {
            generator = lfoInfo.Generator;
            delayTime = (int)(sampleRate * lfoInfo.DelayTime);
            Frequency = lfoInfo.Frequency;
            Depth = lfoInfo.Depth;
            increment = generator.Period * Frequency / sampleRate;
            Reset();
        }
        public void Increment(int amount)
        {
            if(lfoState == LfoStateEnum.Delay)
            {
                phase -= amount;
                if(phase <= 0.0)
                {
                    phase = generator.LoopStartPhase + increment * -phase;
                    Value = generator.GetValue(phase);
                    lfoState = LfoStateEnum.Sustain;
                }
            }
            else
            {
                phase += increment * amount;
                if(phase >= generator.LoopEndPhase)
                    phase = generator.LoopStartPhase + (phase - generator.LoopEndPhase) % (generator.LoopEndPhase - generator.LoopStartPhase);
                Value = generator.GetValue(phase);
            }
        }
        public void Reset()
        {
            Value = 0;
            if(delayTime > 0)
            {
                phase = delayTime;
                lfoState = LfoStateEnum.Delay;
            }
            else
            {
                phase = generator.LoopStartPhase;
                lfoState = LfoStateEnum.Sustain;
            }
        }
        public override string ToString()
        {
            return string.Format("State: {0}, Frequency: {1}Hz, Value: {2:0.00}",lfoState,Frequency,Value);
        }
    }
}

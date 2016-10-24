using System;
using AudioSynthesis.Bank.Descriptors;

namespace AudioSynthesis.Bank.Components.Effects
{
    public class Flanger:IAudioEffect
    {
        private int baseDelay;
        private int minDelay;
        private float[] iBL;
        private float[] oBL;
        private int pL;
        private float[] iBR;
        private float[] oBR;
        private int pR;

        public float FeedBack { get; set; }
        public float WetMix { get; set; }
        public float DryMix { get; set; }
        public Lfo Lfo { get; set; }

        public Flanger(int sampleRate,double minDelay,double maxDelay)
        {
            FeedBack = .15f;
            WetMix = .5f;
            DryMix = .5f;

            LfoDescriptor description = new LfoDescriptor();
            Lfo = new Lfo();
            Lfo.QuickSetup(sampleRate,description);

            if(minDelay > maxDelay)
            {
                double m = minDelay;
                minDelay = maxDelay;
                maxDelay = m;
            }
            baseDelay = (int)(sampleRate * (maxDelay - minDelay));
            this.minDelay = (int)(sampleRate * minDelay);

            int size = (int)(sampleRate * maxDelay) + 1;
            iBL = new float[size];
            oBL = new float[size];
            pL = 0;
            iBR = new float[size];
            oBR = new float[size];
            pR = 0;
        }
        public void ApplyEffect(float[] wave)
        {
            for(int i = 0, j; i < wave.Length; i++)
            {
                Lfo.Increment(1);
                j = pL - (int)(baseDelay * (.5 * Lfo.Value + .5) + minDelay);

                while(j < 0)
                    j += iBL.Length;

                iBL[pL] = wave[i];
                oBL[pL] = DryMix * iBL[pL] + WetMix * iBL[j] + FeedBack * oBL[j];
                wave[i] = oBL[pL++];

                if(pL == iBL.Length)
                    pL = 0;
            }
        }
        public void ApplyEffect(float[] waveLeft,float[] waveRight)
        {
            for(int i = 0, j; i < waveLeft.Length; i++)
            {
                Lfo.Increment(1);
                double lfoValue = (.5 * Lfo.Value + .5);
                //source 1
                j = pL - (int)(baseDelay * lfoValue + minDelay);
                while(j < 0)
                    j += iBL.Length;
                iBL[pL] = waveLeft[i];
                oBL[pL] = DryMix * iBL[pL] + WetMix * iBL[j] + FeedBack * oBL[j];
                waveLeft[i] = oBL[pL++];
                if(pL == iBL.Length)
                    pL = 0;
                //source 2
                j = pR - (int)(baseDelay * (1.0 - lfoValue) + minDelay);
                while(j < 0)
                    j += iBR.Length;
                iBR[pR] = waveRight[i];
                oBR[pR] = DryMix * iBR[pR] + WetMix * iBR[j] + FeedBack * oBR[j];
                waveRight[i] = oBR[pR++];
                if(pR == iBR.Length)
                    pR = 0;

            }
        }
        public void Reset()
        {
            Lfo.Reset();
            Array.Clear(iBL,0,iBL.Length);
            Array.Clear(oBL,0,oBL.Length);
            Array.Clear(iBR,0,iBR.Length);
            Array.Clear(oBR,0,oBR.Length);
            pL = 0;
            pR = 0;
        }
    }
}

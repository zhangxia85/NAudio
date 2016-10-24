using System;
using AudioSynthesis.Bank.Patches;

namespace AudioSynthesis.Bank
{
    public class PatchAsset
    {
        public string Name { get; private set; }
        public Patch Patch { get; private set; }

        public PatchAsset(string name,Patch patch)
        {
            if(name == null)
                throw new ArgumentNullException("An asset must be given a valid name.");
            Name = name;
            Patch = patch;
        }
        public override string ToString()
        {
            return Patch == null ? "null" : Patch.ToString();
        }
    }
}

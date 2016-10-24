using System.Collections.Generic;

namespace AudioSynthesis.Bank
{
    public class AssetManager
    {
        public List<PatchAsset> PatchAssets { get; private set; }
        public List<SampleDataAsset> SampleDataAssets { get; private set; }

        public AssetManager()
        {
            PatchAssets = new List<PatchAsset>();
            SampleDataAssets = new List<SampleDataAsset>();
        }
        public PatchAsset FindPatch(string name)
        {
            for (int i = 0; i < PatchAssets.Count; i++)
            {
                if (PatchAssets[i].Name.Equals(name))
                    return PatchAssets[i];
            }
            return null;
        }
        public SampleDataAsset FindSample(string name)
        {
            for (int i = 0; i < SampleDataAssets.Count; i++)
            {
                if (SampleDataAssets[i].Name.Equals(name))
                    return SampleDataAssets[i];
            }
            return null;
        }
        //public void LoadSampleAsset(string assetName, string patchName, string directory)
        //{
        //    string assetNameWithoutExtension;
        //    string extension;
        //    if (Path.HasExtension(assetName))
        //    {
        //        assetNameWithoutExtension = Path.GetFileNameWithoutExtension(assetName);
        //        extension = Path.GetExtension(assetName).ToLower();
        //    }
        //    else
        //    {
        //        assetNameWithoutExtension = assetName;
        //        assetName += ".wav"; //assume .wav
        //        extension = ".wav";
        //    }
        //    if (FindSample(assetNameWithoutExtension) == null)
        //    {
        //        string waveAssetPath;
        //        if (CrossPlatformHelper.ResourceExists(assetName))
        //            waveAssetPath = assetName; //ex. "asset.wav"
        //        else if (CrossPlatformHelper.ResourceExists(directory + Path.DirectorySeparatorChar + assetName))
        //            waveAssetPath = directory + Path.DirectorySeparatorChar + assetName; //ex. "C:\asset.wav"
        //        else if (CrossPlatformHelper.ResourceExists(directory + "/SAMPLES/" + assetName))
        //            waveAssetPath = directory + "/SAMPLES/" + assetName; //ex. "C:\SAMPLES\asset.wav"
        //        else if (CrossPlatformHelper.ResourceExists(directory + Path.DirectorySeparatorChar + patchName + Path.DirectorySeparatorChar + assetName))
        //            waveAssetPath = directory + Path.DirectorySeparatorChar + patchName + Path.DirectorySeparatorChar + assetName; //ex. "C:\Piano\asset.wav"
        //        else
        //            throw new IOException("Could not find sample asset: (" + assetName + ") required for patch: " + patchName);
        //        using (BinaryReader reader = new BinaryReader(CrossPlatformHelper.OpenResource(waveAssetPath)))
        //        {
        //            switch (extension)
        //            {
        //                case ".wav":
        //                    sampleAssets.Add(new SampleDataAsset(assetNameWithoutExtension, WaveFileReader.ReadWaveFile(reader)));
        //                    break;
        //            }
        //        }
        //    }
        //}
    }
}

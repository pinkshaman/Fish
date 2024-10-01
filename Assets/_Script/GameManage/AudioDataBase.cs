using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/newAudio")]

public class AudioDataBase: ScriptableObject
{
    public List<AudioData> audioDataBases;
}

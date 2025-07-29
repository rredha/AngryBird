using UnityEngine;

namespace Project.Scripts.Runtime.Core.AudioPlayer
{
    [CreateAssetMenu(fileName = "SoundTrack", menuName = "Audio/SoundTrack", order = 100)]
    public class AudioDataScriptableObject : ScriptableObject
    {
        public string gameName;
        public SoundTrackConfigurationScriptableObject[] soundTrackConfigArray;
    }
}

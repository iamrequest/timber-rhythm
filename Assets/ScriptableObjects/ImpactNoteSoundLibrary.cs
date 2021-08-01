using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Music/Impact Note Sound Library")]
public class ImpactNoteSoundLibrary : BaseSoundLibrary {
    public ImpactNotePlayedEventChannel notePlayedEventChannel;
    public List<AudioClip> sounds;
}

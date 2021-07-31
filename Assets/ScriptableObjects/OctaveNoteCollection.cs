using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Music/Octave Note Collection")]
public class OctaveNoteCollection : ScriptableObject {
    public List<AudioClip> notes = new List<AudioClip>(12);

    public AudioClip GetNote(ToneNote noteName) {
        return notes[(int) noteName];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Music/Sustained Note Sound Library")]
public class SustainedNoteSoundLibrary : ScriptableObject {
    [Tooltip("If true, this instrument will call StopNote() just after the note has finished its attack. Eg: A xylophone")]
    public bool skipSustain;

    [Range(0f, 2f)]
    public float attackDuration, releaseDuration;

    public List<OctaveNoteCollection> octaves = new List<OctaveNoteCollection>(5);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSource : MonoBehaviour {
    // The set of layers that will cause this gameobject to play a note on collision
    public LayerMask noiseMakingLayers;

    protected bool DoesCollisonPlayNote(int colliderLayer) {
        // If in the layer mask...
        // Source: https://www.codegrepper.com/code-examples/csharp/unity+how+to+use+layermask+and+compare+with+collision+layer
        return (noiseMakingLayers.value & (1 << colliderLayer)) > 0;
    }
}

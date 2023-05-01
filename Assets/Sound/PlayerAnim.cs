using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    private void PlayFootstep()
    {
        AkSoundEngine.PostEvent("PC_Footstep", this.gameObject);
    }

    private void CollectObject()
    {
        AkSoundEngine.PostEvent("Pc_collect", this.gameObject);
    }
}

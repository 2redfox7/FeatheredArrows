using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ButtonClickMP : NetworkBehaviour
{
    public AudioSource mySound;
    public AudioClip clickSound;

    public void ClickSound()
    {
        mySound.PlayOneShot(clickSound);
    }
}

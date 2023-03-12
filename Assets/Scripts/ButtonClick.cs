using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioSource mySound;
    public AudioClip clickSound;

    public void ClickSound()
    {
        mySound.PlayOneShot(clickSound);
    }
}

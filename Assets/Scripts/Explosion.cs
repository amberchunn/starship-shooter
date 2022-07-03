using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _boomSound;
    void Start()
    {
        AudioSource.PlayClipAtPoint(_boomSound, transform.position);
        Destroy(this.gameObject, 2.8f);
    }
}

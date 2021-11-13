using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    [SerializeField]TMPro.TMP_Text text = null;
    [SerializeField] Image image = null;

    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void UpdateDialog(DialogScriptableObject dialog )
    {
        text.text = dialog.dialog;
        this.audioSource.clip = dialog.vo;
        audioSource.Play();
    }
}

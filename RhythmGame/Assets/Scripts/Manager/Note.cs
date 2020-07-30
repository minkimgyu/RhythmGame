using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    float noteSpeed = 2000; // 설정에서 바꿀 수 있도록 조정한다.
    Image noteImage;

    void OnEnable()
    {
        if(noteImage == null)
            noteImage = GetComponent<Image>();

        noteImage.enabled = true;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    void Update()
    {
        transform.localPosition += Vector3.down * noteSpeed * Time.deltaTime;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}

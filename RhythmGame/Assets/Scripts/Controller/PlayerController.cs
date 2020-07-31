using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TimingManager[] theTimingManager;
    public GameObject Note;

    private void Start()
    {
        theTimingManager = Note.GetComponentsInChildren<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.B1]))
        {
            theTimingManager[3].CheckTiming();
            theTimingManager[3].setClickColor();
        }
        else if (Input.GetKeyUp(KeySetting.keys[KeyAction.B1]))
        {
            theTimingManager[3].setUpColor();
        }
        //
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.B2]))
        {
            theTimingManager[2].CheckTiming();
            theTimingManager[2].setClickColor();
        }
        else if (Input.GetKeyUp(KeySetting.keys[KeyAction.B2]))
        {
            theTimingManager[2].setUpColor();
        }
        //
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.B3]))
        {
            theTimingManager[1].CheckTiming();
            theTimingManager[1].setClickColor();
        }
        else if (Input.GetKeyUp(KeySetting.keys[KeyAction.B3]))
        {
            theTimingManager[1].setUpColor();
        }
        //
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.B4]))
        {
            theTimingManager[0].CheckTiming();
            theTimingManager[0].setClickColor();
        }
        else if (Input.GetKeyUp(KeySetting.keys[KeyAction.B4]))
        {
            theTimingManager[0].setUpColor();
        }
    }
}

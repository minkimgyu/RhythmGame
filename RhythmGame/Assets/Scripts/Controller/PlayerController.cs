using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TimingManager[] theTimingManager;
    public GameObject Note;
    DatabaseManager TheDatabaseManager;

    private void Start()
    {
        theTimingManager = Note.GetComponentsInChildren<TimingManager>();
        TheDatabaseManager = DatabaseManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(TheDatabaseManager.returnData.keySetting[KeyAction.B1]))
        {
            theTimingManager[0].CheckTiming();
            theTimingManager[0].setClickColor();
        }
        else if (Input.GetKeyUp(TheDatabaseManager.returnData.keySetting[KeyAction.B1]))
        {
            theTimingManager[0].setUpColor();
        }
        //
        if (Input.GetKeyDown(TheDatabaseManager.returnData.keySetting[KeyAction.B2]))
        {
            theTimingManager[1].CheckTiming();
            theTimingManager[1].setClickColor();
        }
        else if (Input.GetKeyUp(TheDatabaseManager.returnData.keySetting[KeyAction.B2]))
        {
            theTimingManager[1].setUpColor();
        }
        //
        if (Input.GetKeyDown(TheDatabaseManager.returnData.keySetting[KeyAction.B3]))
        {
            theTimingManager[2].CheckTiming();
            theTimingManager[2].setClickColor();
        }
        else if (Input.GetKeyUp(TheDatabaseManager.returnData.keySetting[KeyAction.B3]))
        {
            theTimingManager[2].setUpColor();
        }
        //
        if (Input.GetKeyDown(TheDatabaseManager.returnData.keySetting[KeyAction.B4]))
        {
            theTimingManager[3].CheckTiming();
            theTimingManager[3].setClickColor();
        }
        else if (Input.GetKeyUp(TheDatabaseManager.returnData.keySetting[KeyAction.B4]))
        {
            theTimingManager[3].setUpColor();
        }
    }
}
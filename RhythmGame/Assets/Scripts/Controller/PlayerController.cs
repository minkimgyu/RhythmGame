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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            theTimingManager[3].CheckTiming();
            theTimingManager[3].setClickColor();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            theTimingManager[3].setUpColor();
        }
        //
        if (Input.GetKeyDown(KeyCode.X))
        {
            theTimingManager[2].CheckTiming();
            theTimingManager[2].setClickColor();
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            theTimingManager[2].setUpColor();
        }
        //
        if (Input.GetKeyDown(KeyCode.N))
        {
            theTimingManager[1].CheckTiming();
            theTimingManager[1].setClickColor();
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            theTimingManager[1].setUpColor();
        }
        //
        if (Input.GetKeyDown(KeyCode.M))
        {
            theTimingManager[0].CheckTiming();
            theTimingManager[0].setClickColor();
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            theTimingManager[0].setUpColor();
        }
    }
}

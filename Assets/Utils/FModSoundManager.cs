using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FModSoundManager : Singleton<FModSoundManager>
{
    float currentAmbienceIntensity;
    public bool loaded = false;
    string currentEvent;
    // FMOD.Studio.EventInstance[] ambiences = new FMOD.Studio.EventInstance[2];
    FMOD.Studio.EventInstance ambience;
    int currentId = 0;
    float volumnValue = 0.2f;
    public float defaultVolumn = 0.2f;
    public bool pressedStart = false;
    bool finished = false;
    //[FMODUnity.EventRef]
    //public string eventName;
    // Start is called before the first frame update

    void Start()
    {
        //ambience = FMODUnity.RuntimeManager.CreateInstance(eventName);
        //ambience.start();
        // Invoke("delayTest", 0.1f);
        DontDestroyOnLoad(gameObject);
    }
    FMOD.Studio.EventInstance currentAmbience()
    {
        return ambience;
    }

    public void resetVolumn()
    {
        SetVolumn(defaultVolumn);
    }
    public void startEvent(string eventName)
    {
        
        if(eventName == "")
        {

            currentEvent = eventName;
            return;
        }
        if (eventName != currentEvent)
        {
            ambience = FMODUnity.RuntimeManager.CreateInstance(eventName);

            // ambience.setVolume(0.1f);
            currentAmbience().start();

            currentAmbience().setVolume(defaultVolumn);
            currentEvent = eventName;
            Debug.Log("start even " + eventName);
        }
    }
    public void SetParam(string paramName, float value)
    {

        ambience.setParameterByName(paramName, value);
    }
    public void SetVolumn(float value)
    {

        //transform.DOMove(new Vector3(2, 2, 2), 1);
﻿﻿﻿﻿﻿﻿﻿﻿// The generic way
//﻿﻿﻿﻿﻿﻿﻿﻿DOTween.To(() => transform.position, x => transform.position = x, new Vector3(2, 2, 2), 1);
        currentAmbience().setVolume(value);
    }
    public void SetAmbienceParamter(float param)
    {
        //if (currentAmbienceIntensity != param)
        {
            print("set ambience to " + param);
            currentAmbienceIntensity = param;
            currentAmbience().setParameterByName("Intensity", param);
            //ambience.setParameterByName()
        }
    }


    public void Pause()
    {
        SetParam("Pause", 1);
    }
    public void Resume()
    {
        SetParam("Pause", 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            return;
        }
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master") && !loaded)
        {
            loaded = true;
            Debug.Log("Master Bank Loaded");
            setDifficultyParam(0);
            finished = true;
            //SceneManager.LoadScene(1);
        }
       // if(pressedStart && loaded)
        {
          //  finished = true;

           // SceneManager.LoadScene(1);


        }
    }

    public  void setDifficultyParam(int va)
    {
        for(int i = 0; i < 4; i++)
        {
            SetParam("To Difficulty " + (i + 1).ToString(), 0);
        }
        SetParam("To Difficulty " + (va + 1).ToString(), 1);
    }
    
    private void OnDestroy()
    {
        currentAmbience().stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //Destroy(ambience);
    }
    public void startGame()
    {
        pressedStart = true;
    }
}

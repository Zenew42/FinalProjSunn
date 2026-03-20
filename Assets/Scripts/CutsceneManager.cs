using System;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector  director;
    
    public void StartCutscene(PlayableAsset cutscene)
    {
        Debug.Log("Started" + cutscene.name);
        director.Play(cutscene);
    }
    
    public void PauseCutscene()
    {
        director.Pause();
    }

    /*private void Start()
    {
        StartCutscene(clip);
    }*/
}

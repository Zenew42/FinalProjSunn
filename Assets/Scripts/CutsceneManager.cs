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

    public void StopCutscene()
    {
        director.Stop();
    }
    

    /*void DiaryScene()
    {
        ExitDialogue();
        _cutsceneManager.StartCutscene(Cutscenes[1]);
        Debug.Log("Is called");
    }
    
    _currentStory.BindExternalFunction("DiaryScene", DiaryScene);*/
}

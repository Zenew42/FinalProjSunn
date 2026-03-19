using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector  director;
    
    public void StartCutscene(PlayableAsset cutscene)
    {
        director.Play(cutscene);
    }
    
    public void PauseCutscene()
    {
        director.Pause();
    }
}

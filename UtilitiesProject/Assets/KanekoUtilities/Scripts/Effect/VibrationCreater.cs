using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class VibrationCreator : Singleton<VibrationCreator>
{
    [HideInInspector]
    public bool IsPlaying = false;
    [HideInInspector]
    public bool IsRepert = false;

    Coroutine coroutine;
    MonoBehaviour mono;

    public void Play(VibrationAction[] actions, MonoBehaviour mono)
    {
        this.mono = mono;
        IsPlaying = true;

        coroutine = mono.StartCoroutine(VibrateCoroutine(actions));
    }

    public void Stop()
    {
        if(!IsPlaying) return;
        IsPlaying = false;
        mono.StopCoroutine(coroutine);
    }

    IEnumerator VibrateCoroutine(VibrationAction[] actions)
    {
        do
        {
            foreach(var a in actions) yield return a.Play();
        } while(IsRepert);

        Stop();
    }
}

public class VibrationAction
{
    enum ActionType { Vibrate, WaitFrame, WaitTime }

    VibrationType vibrateType;
    ActionType actionType;
    int waitFrameCount;
    float waitTime;

    public VibrationAction(string action)
    {
        //actionからTypeに変更
        string first = action[0].ToString();
        int type;
        if(int.TryParse(first, out type))
        {
            actionType = ActionType.Vibrate;
            vibrateType = (VibrationType) type;
        }
        else
        {
            string v = action.Remove(0, 1);
            if(first == "t")
            {
                actionType = ActionType.WaitTime;
                float.TryParse(v, out waitTime);
            }
            else if(first == "f")
            {
                actionType = ActionType.WaitFrame;
                int.TryParse(v, out waitFrameCount);
            }
        }
    }

    public IEnumerator Play()
    {
        switch(actionType)
        {
            case ActionType.Vibrate:
            MyVibrationManager.Instance.Vibrate(vibrateType);
            yield return null;
            break;

            case ActionType.WaitFrame:
            yield return WaitFrame(waitFrameCount);
            break;

            case ActionType.WaitTime:
            yield return new WaitForSeconds(waitTime);
            break;
        }
    }

    IEnumerator WaitFrame(int frameCount)
    {
        for(int i = 0 ; i < frameCount ; i++)
        {
            yield return null;
        }
    }

    public static VibrationAction[] StringToActions(string input)
    {
        var actions = input.Split(',');
        List<VibrationAction> actionList = new List<VibrationAction>();
        for(int i = 0 ; i < actions.Length ; i++)
        {
            if(string.IsNullOrEmpty(actions[i])) continue;
            actionList.Add(new VibrationAction(actions[i]));
        }

        return actionList.ToArray();
    }
}

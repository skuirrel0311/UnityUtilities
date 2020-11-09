using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StageClearPanel : GameStatePanel
{
    [SerializeField]
    UGUIButton nextButton = null;

    [SerializeField]
    AbstractUGUIText message = null;

    [SerializeField]
    RectTransform rightPraticlePoint = null;

    [SerializeField]
    RectTransform leftPraticlePoint = null;

    [SerializeField]
    ParticleSystem rightParticle = null;

    [SerializeField]
    ParticleSystem leftParticle = null;
    
    void Start()
    {
        var mainCamera = Camera.main;
        Vector3 right = rightPraticlePoint.position;
        Vector3 left = leftPraticlePoint.position;
        right.z = 15.0f;
        left.z = 15.0f;

        rightParticle.transform.position = mainCamera.ScreenToWorldPoint(right);
        leftParticle.transform.position = mainCamera.ScreenToWorldPoint(left);
    }

    public override void Activate()
    {
        message.Text = "LEVEL " + MyGameManager.Instance.CurrentLevel + "\n COMPLETED!!";
        message.transform.localScale = Vector3.zero;
        message.Alpha = 0.0f;
        nextButton.gameObject.SetActive(false);

        base.Activate();

        PlayClearMessageAnimation();
    }

    void PlayClearMessageAnimation()
    {
        this.FloatLerp(0.2f, (t) =>
        {
            message.Alpha = Mathf.Lerp(0.0f, 1.0f, Easing.OutQuad(t));
            message.transform.localScale = Vector3.Lerp(Vector3.one * 2.5f, Vector3.one, Easing.InQuad(t));
        });

        this.Delay(0.4f, () =>
        {
            rightParticle.Play(true);
            leftParticle.Play(true);            
        });

        this.Delay(1f, () =>
        {
            nextButton.transform.localScale = Vector3.zero;
            nextButton.gameObject.SetActive(true);

            this.FloatLerp(0.5f, (t) =>
            {
                nextButton.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, Easing.OutBack(t));
            });
        });
    }

    public IEnumerator SuggestRestart()
    {
        yield return KKUtilities.WaitAction(nextButton.OnClickEvent);
    }
}

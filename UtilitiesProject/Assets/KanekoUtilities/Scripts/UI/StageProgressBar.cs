using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class StageProgressBar : MonoBehaviour
    {
        [SerializeField]
        Image bar = null;
        [SerializeField]
        Image nextStageCircle = null;
        [SerializeField]
        NumberText currentLevel = null;
        [SerializeField]
        NumberText nextLevel = null;
        [SerializeField]
        Color defaultCircleColor = Color.gray;
        [SerializeField]
        Color stageClearCircleColor = Color.yellow;

        [SerializeField]
        Transform playerTransform = null;
        
        float progress = 0.0f;
        float Progress
        {
            get { return progress; }
            set
            {
                if (progress == value) return;

                progress = value;
                bar.fillAmount = progress;
            }
        }

        float stageLength;
        Vector3 goalPosition;
        
        void Update()
        {
            Progress = (goalPosition - playerTransform.position).magnitude / stageLength;

            if(Progress >= 1.0f)
            {
                nextStageCircle.color = stageClearCircleColor;
            }
        }

        public void Init(int currentLevel, Vector3 goalPosition)
        {
            Progress = 0.0f;
            nextStageCircle.color = defaultCircleColor;

            this.currentLevel.SetValue(currentLevel);
            nextLevel.SetValue(currentLevel + 1);

            this.goalPosition = goalPosition;
            stageLength = (goalPosition - playerTransform.position).magnitude;
        }
    }
}
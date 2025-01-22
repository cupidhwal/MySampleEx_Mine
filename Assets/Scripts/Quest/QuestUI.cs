using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MySampleEx
{
    /// <summary>
    /// 퀘스트 진행 창, 퀘스트 정보 창, 퀘스트 목록 등
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        // 필드
        #region Variables
        private Quest quest;            // 퀘스트 정보 창에 보이는 퀘스트

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;

        public TextMeshProUGUI goalText;
        public TextMeshProUGUI rewardGoldText;
        public TextMeshProUGUI rewardExpText;
        public TextMeshProUGUI rewardItemText;
        public Image rewardItemImage;

        public GameObject acceptButton;
        public GameObject giveupButton;
        public GameObject okButton;

        public Action OnCloseQuest;     // 퀘스트 종료 시 실행되는 이벤트
        #endregion



        void SetQuestUI(Quest _quest)
        {
            // 정보 창
            quest = _quest;

            nameText.text = quest.name;
            descriptionText.text = quest.description;

            goalText.text = quest.questGoal.currentAmount.ToString() + " / " + quest.questGoal.goalAmount.ToString();
            rewardGoldText.text = quest.rewardGold.ToString();
            rewardExpText.text = quest.rewardExp.ToString();
            Debug.Log(quest.rewardExp);
            Debug.Log(quest.rewardItem);

            if (quest.rewardItem >= 0)
            {
                ItemObject rewardItem = UIManager.Instance.database.itemObjects[quest.rewardItem];
                rewardExpText.text = rewardItem.name;
                rewardItemImage.sprite = rewardItem.icon;
                rewardItemImage.enabled = true;
            }
            else
            {
                rewardExpText.text = "";
                rewardItemImage.sprite = null;
                rewardItemImage.enabled = false;
            }

            // 버튼 세팅
            ResetButton();
            switch (quest.questState)
            {
                case QuestState.Ready:
                    acceptButton.SetActive(true);
                    break;

                case QuestState.Accept:
                    giveupButton.SetActive(true);
                    break;

                case QuestState.Complete:
                    okButton.SetActive(true);
                    break;
            }
        }

        void ResetButton()
        {
            // 버튼 세팅
            acceptButton.SetActive(false);
            giveupButton.SetActive(false);
            okButton.SetActive(false);
        }

        public void OpenQuestUI()
        {
            if (QuestManager.Instance.currentQuest == null)
            {
                //
                return;
            }

            SetQuestUI(QuestManager.Instance.currentQuest);
        }
    }
}
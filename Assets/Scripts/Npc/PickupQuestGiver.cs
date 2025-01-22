using System.Collections.Generic;
using UnityEngine;

namespace MySampleEx
{
    /// <summary>
    /// 퀘스트를 가진 NPC
    /// </summary>
    public class PickupQuestGiver : PickupNpc
    {
        #region Variables
        public List<Quest> quests;
        #endregion

        protected override void Start()
        {
            base.Start();
            quests = GetNPCQuest(npc.number);
        }

        // NPC의 해당 인덱스에 지정된 퀘스트 목록 가져오기
        public List<Quest> GetNPCQuest(int npcNumber)
        {
            List<Quest> questList = new();

            foreach (Quest quest in DataManager.GetQuestData().Quests.quests)
            {
                if (quest.npcNumber == npcNumber)
                {
                    Quest newQuest = new()
                    {
                        number = quest.number,
                        npcNumber = quest.npcNumber,
                        name = quest.name,
                        description = quest.description,
                        dialogIndex = quest.dialogIndex,
                        level = quest.level,

                        questGoal = new()
                        {
                            questType = quest.questType,
                            goalIndex = quest.goalIndex,
                            goalAmount = quest.goalAmount,
                            currentAmount = 0
                        },

                        rewardGold = quest.rewardGold,
                        rewardExp = quest.rewardExp,
                        rewardItem = quest.rewardItem,

                        questState = QuestState.Ready,
                    };
                    questList.Add(newQuest);
                }
            }

            return questList;
        }

        public override void DoAction()
        {
            if (quests.Count == 0)
            {
                Debug.Log("모든 퀘스트 클리어!");
                return;
            }

            QuestManager.Instance.currentQuest = quests[0];

            switch (quests[0].questState)
            {
                case QuestState.Ready:
                    UIManager.Instance.OpenDialogUI(quests[0].dialogIndex, npc.npcType);
                    break;

                case QuestState.Accept:
                    UIManager.Instance.OpenDialogUI(quests[0].dialogIndex+1, npc.npcType);
                    break;

                case QuestState.Complete:
                    UIManager.Instance.OpenDialogUI(quests[0].dialogIndex+2, npc.npcType);
                    break;
            }
        }
    }
}
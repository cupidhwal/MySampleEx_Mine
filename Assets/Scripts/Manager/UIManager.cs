using UnityEngine;

namespace MySampleEx
{
    public class UIManager : Singleton<UIManager>
    {
        #region Variables
        public ItemDataBase database;

        public DynamicInventoryUI palyerInventoryUI;
        public StaticInventoryUI palyerEquipmentUI;
        public PlayerStatsUI playerStatsUI;
        public DialogUI dialogUI;
        public QuestUI questUI;

        public int itemId = 0;
        #endregion

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {   
                Toggle(palyerInventoryUI.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                Toggle(palyerEquipmentUI.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Toggle(playerStatsUI.gameObject);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                AddNewItem();
            }
        }

        public void Toggle(GameObject go)
        {
            go.SetActive(!go.activeSelf);
        }

        public void AddNewItem()
        {
            ItemObject itemObject = database.itemObjects[itemId];
            Item newItem = itemObject.CreateItem();

            palyerInventoryUI.inventoryObject.AddItem(newItem, 1);
        }

        public void OpenDialogUI(int dialogIndex, NpcType npcType)
        {
            Toggle(dialogUI.gameObject);
            dialogUI.OnCloseDialogue += CloseDialogUI;
            if (npcType == NpcType.QuestGiver)
            {
                //Quest UI 열기
                dialogUI.OnCloseDialogue += OpenQuestUI;
            }

            dialogUI.StartDialog(dialogIndex);
        }

        public void CloseDialogUI()
        {
            Toggle(dialogUI.gameObject);
        }

        public void OpenQuestUI()
        {
            Toggle(questUI.gameObject);
            questUI.OnCloseQuest += CloseQuestUI;
            questUI.OpenQuestUI();
        }

        public void CloseQuestUI()
        {
            Toggle(questUI.gameObject);
        }
    }
}
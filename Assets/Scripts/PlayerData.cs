using UnityEngine;

namespace SaveSystemTutorial
{    
    public class PlayerData : MonoBehaviour
    {

        #region Fields

        [SerializeField] string playerName = "Player Name";
        [SerializeField] int level = 0;
        [SerializeField] int coin = 0;

        const string PLAYER_DATA_KEY = "PlayerData";

        /// <summary>
        /// 需要System.Serializable，否则JsonUtility不可转换
        /// </summary>
        [System.Serializable]class SaveDate
        {
            public string playerName;
            public int playerLevel;
            public int playerCoin;
            public Vector3 playerPos;
        }

        #endregion

        #region Properties

        public string Name => playerName;

        public int Level => level;
        public int Coin => coin;

        public Vector3 Position => transform.position;

        #endregion

        #region Save and Load
        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            SaveByPlayerPrefs();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public void Load()
        {
            LoadByPlayerPrefs();   
        }

        #endregion

        #region PlayerPrefs方式

        /// <summary>
        /// PlayerPrefs方式保存
        /// </summary>
        void SaveByPlayerPrefs()
        {
            #region 不封装数据的方法
            /*
            //保存玩家名字
            PlayerPrefs.SetString("PlayerName", playerName);

            //保存玩家等级
            PlayerPrefs.SetInt("PlayerLevel", level);
            //保存玩家金币
            PlayerPrefs.SetInt("PlayerCoin", coin);

            //保存玩家位置
            PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);       
            PlayerPrefs.SetFloat("PlayerPosy", transform.position.y);       
            PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

            PlayerPrefs.Save();
            */
            #endregion

            #region 封装数据的方法

            var saveData = new SaveDate();

            saveData.playerName = playerName;
            saveData.playerLevel = level;
            saveData.playerCoin = coin;
            saveData.playerPos = transform.position;

            SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, saveData);

            #endregion
        }

        /// <summary>
        /// PlayerPrefs方式读取
        /// </summary>
        void LoadByPlayerPrefs()
        {
            #region 不封装数据的方法
            /*
            playerName = PlayerPrefs.GetString("PlayerName","None");

            level = PlayerPrefs.GetInt("PlayerLevel",0);
            coin = PlayerPrefs.GetInt("PlayerCoin",0);

            transform.position = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX", 0f),
                PlayerPrefs.GetFloat("PlayerPosy", 0f),
                PlayerPrefs.GetFloat("PlayerPosZ", 0f));
            */
            #endregion

            #region 封装数据的方法

            var json = SaveSystem.LoadByPlayerPrefs(PLAYER_DATA_KEY);
            var saveData = JsonUtility.FromJson<SaveDate>(json);

            playerName = saveData.playerName;
            level = saveData.playerLevel;
            coin= saveData.playerCoin;
            transform.position = saveData.playerPos;

            #endregion
        }

        /// <summary>
        /// 删除存档
        /// </summary>
        [UnityEditor.MenuItem("工具/删除存档")]
        public static void DeletePlayerDataPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion

    }
}
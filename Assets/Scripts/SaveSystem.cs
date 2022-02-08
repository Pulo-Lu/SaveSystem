using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTutorial
{
    public static class SaveSystem
    {
        /// <summary>
        /// Json方式保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void SaveByPlayerPrefs(string key,object data)
        {
            //将数据转为Json格式
            var json = JsonUtility.ToJson(data);
            //保存
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();

            #if UNITY_EDITOR
            Debug.Log("存档成功！");
            #endif
        }

        /// <summary>
        /// Json方式读取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string LoadByPlayerPrefs(string key)
        {
            return PlayerPrefs.GetString(key, null);
        }

    }
}
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeSystem.Core
{
    /// <summary>
    /// Singleton manager untuk menyimpan global variables & flags alur cerita (Blackboard Pattern).
    /// </summary>
    public class StoryStateManager : MonoBehaviour
    {
        public static StoryStateManager Instance { get; private set; }

        private Dictionary<string, bool> _boolFlags = new Dictionary<string, bool>();
        private Dictionary<string, int> _intState = new Dictionary<string, int>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #region Flag (Boolean) Operations
        public void SetFlag(string key, bool value)
        {
            if (string.IsNullOrEmpty(key)) return;
            _boolFlags[key] = value;
        }

        public bool GetFlag(string key)
        {
            if (string.IsNullOrEmpty(key)) return true; // Default true jika tidak ada syarat
            return _boolFlags.TryGetValue(key, out bool val) && val;
        }
        #endregion

        #region Integer State Operations
        public void SetInt(string key, int value)
        {
            if (string.IsNullOrEmpty(key)) return;
            _intState[key] = value;
        }

        public int GetInt(string key)
        {
            if (string.IsNullOrEmpty(key)) return 0;
            return _intState.TryGetValue(key, out int val) ? val : 0;
        }
        #endregion
    }
}

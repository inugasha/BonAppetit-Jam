using System;
using UnityEngine;

namespace Utils.Runtime
{
    public class UpdateCaller : MonoBehaviour
    {
        #region Exposed

        public static UpdateCaller m_instance;

        public static Action<float> OnUpdate;

        #endregion


        #region Unity API

        private void Awake() => m_instance = this;

        private void Update()
        {
            OnUpdate?.Invoke(Time.deltaTime * Time.timeScale);
        }

        #endregion
    }
}
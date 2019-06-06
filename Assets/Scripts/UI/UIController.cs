﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PopBlast.UI
{
    public class UIController : MonoBehaviour
    {
        #region MEMBERS

        [SerializeField] private Button restartBtn = null;
        [SerializeField] private Text scoreTxt = null;
        [SerializeField] private Text hiScoreTxt = null;
        [SerializeField] private Text feedbackTxt = null;

        [Header("Feedback")]
        [Space()]
        [SerializeField] private float feedbackTimer = 1f;
        [SerializeField] private KeyValue[] keyValues;

        public event Action RaiseNewGame;

        

        #endregion

        #region UNITY_LIFECYCLE

        private void Awake()
        {
            restartBtn.onClick.AddListener(() =>
            {
                RaiseNewGame?.Invoke();
            });
            SetRestartPanel(false);
            UpdateScore(0.ToString());
            SetFeedbackPanelOff();
        }

        #endregion

        #region PUBLIC_METHODS

        public void SetRestartPanel(bool active)
        {
            restartBtn.gameObject.SetActive(active);
        }

        /// <summary>
        /// Update the user score
        /// </summary>
        /// <param name="score">Value to be displayed</param>
        public void UpdateScore(string score)
        {
            if (string.IsNullOrEmpty(score))
            {
                return;
            }
            scoreTxt.text = $"Score : {score}";
        }

        /// <summary>
        /// Update hi score with new score
        /// </summary>
        /// <param name="score"></param>
        public void UpdateHiScore(string score)
        {
            if (string.IsNullOrEmpty(score))
            {
                return;
            }
            hiScoreTxt.text = $"1st : {score}";
        }

        /// <summary>
        /// Set feedback panel based on item destroyed amount
        /// </summary>
        /// <param name="amount"></param>
        public void SetFeedback(int amount)
        {
            if (amount < keyValues[0].amount)
            {
                return;
            }
            string message = null;
            if (amount > keyValues[keyValues.Length - 1].amount)
            {
                message = keyValues[keyValues.Length - 1].message;
            }
            else
            {
                int index = Array.FindIndex(keyValues, (o) => { return o.amount == amount; });
                message = message = keyValues[index].message;
            }

            feedbackTxt.transform.parent.gameObject.SetActive(true);
            feedbackTxt.text = message;
            Invoke("SetFeedbackPanelOff", feedbackTimer);
        }

        #endregion

        #region PRIVATE_METHODS

        private void SetFeedbackPanelOff()
        {
            feedbackTxt.transform.parent.gameObject.SetActive(false);
        }

        #endregion

        #region DATA_TYPES
        
        [Serializable]
        public struct KeyValue
        {
            public int amount;
            public string message;
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UntitledLogicGame.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Unity Properties

        [Header("Components")]
        public GameObject GateBar;
        public GameObject MovingBar;

        [Header("Prefabs")]
        public UIGate UIGatePrefab;

        #endregion

        #region Public Properties

        #endregion

        #region Private Properties

        private bool _lastMouseInteracting;

        #endregion

        #region Unity Methods

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => GameManager.Instance != null);
            // TODO calculate max width
            var currentPos = 0f;
            foreach(var gatePrefab in GameManager.Instance.GatePrefabs)
            {
                var uiGate = Instantiate(UIGatePrefab, GateBar.transform);
                uiGate.GatePrefab = gatePrefab;
                uiGate.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPos, 0);
                currentPos += 100f;
            }

            MovingBar.SetActive(false);
        }

        private void FixedUpdate()
        {
            if(PointerManager.Instance.Interacting != _lastMouseInteracting)
            {
                //TODO animate go down
                GateBar.SetActive(!PointerManager.Instance.Interacting);
                MovingBar.SetActive(PointerManager.Instance.MovingObject);
                _lastMouseInteracting = PointerManager.Instance.Interacting;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}
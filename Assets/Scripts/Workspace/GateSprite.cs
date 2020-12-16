using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UntitledLogicGame.Workspace.Gates;

namespace UntitledLogicGame.Workspace
{
    public class GateSprite : MonoBehaviour
    {
        #region Unity Properties

        #endregion

        #region Public Properties

        #endregion

        #region Private Properties

        private Gate _gate;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _gate = GetComponentInParent<Gate>();
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void OnMouseEnter()
        {
            GameManager.Instance.CurrentGate = _gate;
        }

        private void OnMouseExit()
        {
            if (_gate.Equals(GameManager.Instance.CurrentGate))
                GameManager.Instance.CurrentGate = null;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }
}
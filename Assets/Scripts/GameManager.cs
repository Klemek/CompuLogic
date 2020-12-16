using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UntitledLogicGame.Workspace;

namespace UntitledLogicGame
{
    public class GameManager : MonoBehaviour
    {
        #region Static Properties

        public static GameManager Instance { 
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }
        private static GameManager _instance;

        #endregion

        #region Unity Properties

        [Header("Prefabs")]
        public Cable CablePrefab;

        [Header("Groups")]
        public Transform GatesGroup;
        public Transform CablesGroup;

        [Header("Colors")]
        public Color DeadColor;
        public Color ActivatedColor;

        [Header("Gates")]
        public List<Gate> GatePrefabs;

        #endregion

        #region Public Properties

        public Anchor CurrentAnchor { get; set; }
        public Gate CurrentGate { get; set; }
        public PointerManager PointerManager 
        {
            get
            {
                if (_pointerManager == null)
                    _pointerManager = GetComponent<PointerManager>();
                return _pointerManager;
            }
        }

        #endregion

        #region Private Properties

        private PointerManager _pointerManager;

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        public void CreateGate(Gate gatePrefab)
        {
            var gate = Instantiate(gatePrefab, GatesGroup);
            gate.transform.position = PointerManager.MousePos - gate.Box.transform.position;
            PointerManager.DragGate(gate, true);
        }

        #endregion

        #region Private Methods

        #endregion
    }

}
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

        public static GameManager Instance { get; set; }

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

        public PointerManager MouseManager { get; private set; }

        #endregion

        #region Private Properties

        #endregion

        #region Unity Methods

        // Start is called before the first frame update
        private void Start()
        {
            if (Instance != null)
                throw new InvalidOperationException("More than one GameManager in scene");
            Instance = this;
            MouseManager = GetComponent<PointerManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        #endregion

        #region Public Methods

        public void CreateGate(Gate gatePrefab, Vector3 position)
        {
            var gate = Instantiate(gatePrefab, GatesGroup);
            gate.transform.position = PointerManager.MousePos - gate.Box.transform.position;
            MouseManager.DragGate(gate, true);
        }

        #endregion

        #region Private Methods

        #endregion
    }

}
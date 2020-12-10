﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UntitledLogicGame.Gates;

namespace UntitledLogicGame
{
    public class Gate : MonoBehaviour
    {
        #region Unity Properties

        public bool HasState;
        public GateType GateType;

        #endregion

        #region Public Properties

        public List<Anchor> Anchors { get; set; }
        public IEnumerable<Anchor> InputAnchors => Anchors.Where(a => a.IsInput);
        public IEnumerable<Anchor> OutputAnchors => Anchors.Where(a => !a.IsInput);

        #endregion

        #region Private Properties

        private GateDefinition _definition;
        private int _lastState = -1;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Utils.RandomName(GateType.ToString(), gameObject);
            Anchors = GetComponentsInChildren<Anchor>().ToList();
            _definition = GateDefinition.Get(GateType, this);   
        }

        // Update is called once per frame
        private void Update()
        {
            var state = _definition.GetState(this).ToInt();
            if(state != _lastState)
            {
                _definition.Compute(this);
                _lastState = state;
            }
            
        }

        #endregion

        #region Public Methods

        public bool HasInputAnchor(Anchor target)
        {
            return !HasState && (
                    InputAnchors.Contains(target) ||
                    InputAnchors.Any(a => a.HasInputAnchor(target))
                );
        }

        #endregion

        #region Private Methods

        #endregion

    }
}
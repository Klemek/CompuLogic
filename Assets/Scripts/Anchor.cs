﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    #region Unity Properties

    public bool IsInput;
    public string Name;
    public float ScaleIncrease;

    #endregion

    #region Public Properties

    public List<Cable> Cables { get; set; }
    public Gate Gate { get; set; }
    public bool Activated
    {
        get
        {
            if (IsInput)
                return Cables.Count > 0 && Cables.First().Activated;
            else
                return _activated;
        }
        set
        {
            if (!IsInput)
                _activated = value;
        }
    }

    #endregion

    #region Private Properties

    private Vector3 _scale;
    private SpriteRenderer _sprite;
    private bool _activated;
    private bool? _lastActivated;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    private void Start()
    {
        Utils.RandomName("Anchor", gameObject);
        _scale = transform.localScale;
        _sprite = GetComponent<SpriteRenderer>();
        Cables = new List<Cable>();
        Gate = GetComponentInParent<Gate>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(_lastActivated == null || _lastActivated != Activated)
        {
            _sprite.color = Activated ? GameManager.Instance.ActivatedColor : GameManager.Instance.DeadColor;
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale = _scale * ScaleIncrease;
        GameManager.Instance.CurrentAnchor = this;
    }

    private void OnMouseExit()
    {
        transform.localScale = _scale;
        if(Equals(GameManager.Instance.CurrentAnchor))
            GameManager.Instance.CurrentAnchor = null;
    }

    #endregion

    #region Public Methods

    public bool HasInputAnchor(Anchor target)
    {
        if (IsInput)
            return Cables.Any(c => c.HasInputAnchor(target));
        else
            return Gate.HasInputAnchor(target);
    }

    #endregion

    #region Private Methods

    #endregion
}

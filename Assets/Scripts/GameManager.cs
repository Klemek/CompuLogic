using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion

    #region Public Properties

    public Anchor CurrentAnchor { get; set; }

    #endregion

    #region Private Properties

    private Cable _currentCable;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    private void Start()
    {
        if (Instance != null)
            throw new InvalidOperationException("More than one GameManager in scene");
        Instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (CurrentAnchor != null && _currentCable == null)
            {
                _currentCable = Instantiate(CablePrefab, CablesGroup, true);
                _currentCable.StartAnchor = CurrentAnchor;
            }
        }
        else if (_currentCable != null)
        {
            if (CurrentAnchor == null || _currentCable.StartAnchor.IsInput == CurrentAnchor.IsInput)
            {
                Destroy(_currentCable.gameObject);
            }
            else
            {
                _currentCable.EndAnchor = CurrentAnchor;
            }
            _currentCable = null;
        }
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    #endregion
}

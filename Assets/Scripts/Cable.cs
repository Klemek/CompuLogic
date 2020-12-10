using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    #region Unity Properties

    #endregion

    #region Public Properties

    public Anchor StartAnchor
    {
        get => _startAnchor;
        set
        {
            _startAnchor = value;
        }
    }
    public Anchor EndAnchor
    {
        get => _endAnchor;
        set
        {
            if (!value.IsInput)
            {
                _endAnchor = StartAnchor;
                StartAnchor = value;
            }
            else
            {
                _endAnchor = value;
            }

            if (StartAnchor.HasInputAnchor(EndAnchor))
            {
                // Loop detected
                Destroy(gameObject);
            }
            else
            {
                StartAnchor.Cables.Add(this);
                EndAnchor.Cables = new List<Cable> { this };
            }
        }
    }
    public bool Activated => StartAnchor != null && !StartAnchor.IsInput && StartAnchor.Activated;

    #endregion

    #region Private Properties

    private Anchor _startAnchor;
    private Anchor _endAnchor;
    private LineRenderer _line;
    private bool? _lastActivated;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        Utils.RandomName("Cable", gameObject);

    }

    // Update is called once per frame
    private void Update()
    {
        if (_lastActivated == null || _lastActivated != Activated)
        {
            _line.startColor = Activated ? GameManager.Instance.ActivatedColor : GameManager.Instance.DeadColor;
            _line.endColor = Activated ? GameManager.Instance.ActivatedColor : GameManager.Instance.DeadColor;
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        var startPos = StartAnchor == null ? mousePos : StartAnchor.transform.position;
        var endPos = EndAnchor == null ? mousePos : EndAnchor.transform.position;
        _line.positionCount = 2;
        _line.SetPosition(0, startPos);
        _line.SetPosition(1, endPos);
    }

    #endregion

    #region Public Methods

    public bool HasInputAnchor(Anchor target)
    {
        return StartAnchor.HasInputAnchor(target);
    }

    #endregion

    #region Private Methods

    #endregion

}

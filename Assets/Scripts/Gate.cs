using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gate : MonoBehaviour
{
    #region Unity Properties

    private bool HasState;

    #endregion

    #region Public Properties

    public List<Anchor> Anchors { get; set; }
    public IEnumerable<Anchor> InputAnchors => Anchors.Where(a => a.IsInput);
    public IEnumerable<Anchor> OutputAnchors => Anchors.Where(a => !a.IsInput);

    #endregion

    #region Private Properties

    #endregion

    #region Unity Methods

    private void Start()
    {
        Utils.RandomName("Gate", gameObject);
        Anchors = GetComponentsInChildren<Anchor>().ToList();
    }

    // Update is called once per frame
    private void Update()
    {

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

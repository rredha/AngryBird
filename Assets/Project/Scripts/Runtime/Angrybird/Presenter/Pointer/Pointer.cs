using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.View.Pointer;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
  public class Pointer : MonoBehaviour
  {
    public PointerBehaviour Behaviour { get; private set; }
    private PointerVisual _visual;

    public void Setup()
    {
      Behaviour = GetComponent<PointerBehaviour>();
      _visual = GetComponent<PointerVisual>();
    }
  }
}
using System;
using Model.Bubble;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Bubble
{
    public class BubbleHandler : MonoBehaviour
    {
        private BubbleData  _data;
        public bool IsBubblePopped => _data.Popped;
        private bool _isClicked;

        public event EventHandler OnBubbleClicked;
        private void Awake()
        {
            _data = new BubbleData(false);
            OnBubbleClicked += Destroy;
        }
        private void Destroy(object sender, EventArgs e)
        {
            _data.Popped = true;
            Destroy(gameObject);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.GetComponent<Pointer>()) return;
            _isClicked = Input.GetMouseButtonDown(0);
            
            if (_isClicked) 
                OnBubbleClicked?.Invoke(this, EventArgs.Empty);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.GetComponent<Pointer>()) return;
            _isClicked = false;
        }
    }
}
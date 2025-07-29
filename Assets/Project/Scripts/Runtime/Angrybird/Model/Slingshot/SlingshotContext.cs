using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Slingshot;
using Project.Scripts.Runtime.Angrybird.View.Slingshot;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Model.Slingshot
{
    public class SlingshotContext
    {
        public bool ReleasedTriggered { get; set; } = false;
        public Vector3 PointerWorldPosition { get; set; }
        public GameObject Holder { get; set; }
        public DropZone DropZone { get; set; }
        public Rubber Rubber { get; set; }
        public Projectile Projectile { get; set; }

        public SlingshotContext(GameObject holder, DropZone dropZone, Rubber rubber)
        {
            Holder = holder;
            DropZone = dropZone;
            Rubber = rubber;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ObjectMover : BaseObject
    {
        public ObjectMoverManager moverManager;

        protected override void OnEnable()
        {
            base.OnEnable();
            moverManager.AddMover(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            moverManager.RemoveMover(this);
        }

        public void Move(Vector3 direction)
        {
            Transform.Translate(direction * Time.deltaTime);
        }
    }
}

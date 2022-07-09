using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ObjectMoverManager : BaseComponent
    {
        [SerializeField] List<ObjectMover> listMovers;

        Vector3 direction;
        public Vector3 Direction { get => direction; }

        public void StartMove(Vector3 direction)
        {
            this.direction = direction;
            LoopManager.OnUpdate.AddListener(() => Move(direction));
        }

        public void StopMove()
            => LoopManager.OnUpdate.RemoveListener(() => Move(direction));


        public void AddMover(ObjectMover mover)
        {
            if (listMovers.Contains(mover) == false)
                listMovers.Add(mover);
        }

        public void RemoveMover(ObjectMover mover)
        {
            if (listMovers.Contains(mover) == false)
                listMovers.Remove(mover);
        }

        public void Move(Vector3 direction)
        {
            if (listMovers.Exists())
                for (int i = 0; i < listMovers.Count; i++)
                {
                    listMovers[i].Move(direction);
                }
        }
    }
}
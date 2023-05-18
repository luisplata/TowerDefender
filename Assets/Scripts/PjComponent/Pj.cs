using System;
using System.Collections.Generic;

namespace PjComponent
{
    public class Pj
    {
        private Queue<INodeCustom> path;
        private INodeCustom concurrentNode;
        private int count;
        private IPath _shortestPath;
        private readonly IPj _pjbase;
        private float speed;
        private float _life;

        public Pj(IPath shortestPath, float speedFather, IPj pjbase, float lifeInComming)
        {
            speed = speedFather;
            _shortestPath = shortestPath;
            _pjbase = pjbase;
            _life = lifeInComming;
            path = new Queue<INodeCustom>();
            var listOfNodes = _shortestPath.Nodes();
            
            if (listOfNodes.Count <= 0)
            {
                throw new Exception("Nodes is empty");
            }
            
            foreach (var no in listOfNodes)
            {
                path.Enqueue(no);
            }
            count = listOfNodes.Count - 1;
            concurrentNode = listOfNodes[count];
        }

        public void Action()
        {
            var positionFinal = concurrentNode.GetGameObjectPosition();
            positionFinal.y += 1;
            _pjbase.Move(_pjbase.GetPosition(), positionFinal, speed);
            if (!_pjbase.IsClose(_pjbase.GetPosition(), concurrentNode.GetGameObjectPosition()) || count <= 0) return;
            count--;
            concurrentNode = _shortestPath.Nodes()[count];
        }

        internal void ApplyDamange(float damage)
        {
            _life -= damage;
            if(_life <= 0){
                _pjbase.DestroyMoster();
            }
        }

        internal bool CanMove()
        {
            return _life > 0;
        }
    }
}
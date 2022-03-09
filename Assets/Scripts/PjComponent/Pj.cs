using System.Collections.Generic;

namespace PjComponent
{
    public class Pj
    {
        private Queue<Node> path;
        private Node concurrentNode;
        private bool hasUse;
        private int count;
        private Path _shortestPath;
        private readonly IPj _pjbase;
        private float speed;

        public Pj(Path shortestPath, float speedFather, IPj pjbase)
        {
            speed = speedFather;
            _shortestPath = shortestPath;
            _pjbase = pjbase;
            path = new Queue<Node>();
            foreach (var no in _shortestPath.nodes)
            {
                path.Enqueue(no);
            }
            count = _shortestPath.nodes.Count - 1;
            concurrentNode = _shortestPath.nodes[count];
            hasUse = true;
        }

        public void Action()
        {
            if (!hasUse) return;
            var positionFinal = concurrentNode.gameObject.transform.position;
            positionFinal.y += 1;
            _pjbase.Move(_pjbase.GetPosition(), positionFinal, speed);
            if (_pjbase.IsClose(_pjbase.GetPosition(), concurrentNode.gameObject.transform.position) && count >0)
            {
                count--;
                concurrentNode = _shortestPath.nodes[count];
            }
        }
    }
}
using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace PjComponent.PjComponentTDD
{
    public class CreatingPj
    {
        private IPath path;
        private IPj iPj;
        private INodeCustom nodeSub;

        [SetUp]
        public void SetUp()
        {
            path = Substitute.For<IPath>();
            iPj = Substitute.For<IPj>();
            nodeSub = Substitute.For<INodeCustom>();
        }
    
        [Test]
        public void CreatingPjSimplePasses()
        {
            path.Nodes().Returns(new List<INodeCustom> {nodeSub});
            var pj = new Pj(path, 1, iPj);
            path.Received(1).Nodes();
        }

        [Test]
        public void CreatingPjSimple_WhenNodesIsEmpty_ThrowException()
        {
            path.Nodes().Returns(new List<INodeCustom>());
            Assert.Throws<Exception>(() =>
            {
                var pj = new Pj(path, 1, iPj);
            });
        }

        [Test]
        public void ActionPj_inNormalSituation_CanMove()
        {
            path.Nodes().Returns(new List<INodeCustom>{nodeSub});
            var pj = new Pj(path, 1, iPj);
            
            pj.Action();

            iPj.Received(1).Move(Arg.Any<Vector3>(), Arg.Any<Vector3>(), 1);
        }

        [TearDown]
        public void TearDown()
        {
        
        }
    }
}

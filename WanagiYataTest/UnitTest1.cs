using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using Assets.Scripts.Extension;

namespace WanagiYataTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector3 vetor = new Vector3(1,0);
            vetor = vetor.AvoidCollision();

        }
    }
}

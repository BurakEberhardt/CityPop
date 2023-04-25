using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.Character
{
    [DataBinding(typeof(TestData))]
    public partial class TestView : View
        , TestData.IAddedListener
        , TestData.IRemovedListener
        , TestData.ITestListener
        , TestData.IColorListener
    {
        protected TestData _testData;
        public TestData TestData
        {
            get => _testData;
            set
            {
                if (_testData != null)
                {
                    _testData.RemoveTestListener(this);
                    _testData.RemoveColorListener(this);
                    (this as TestData.IRemovedListener).OnRemoved();
                }

                _testData = value;

                if (_testData != null)
                {
                    _testData.AddTestListener(this);
                    _testData.AddColorListener(this);
                    (this as TestData.IAddedListener).OnAdded(_testData);
                }
            }
        }

        void TestData.IAddedListener.OnAdded(TestData testData)
        {
        }

        void TestData.IRemovedListener.OnRemoved()
        {
        }

        void TestData.ITestListener.OnTest(int test)
        {
        }

        void TestData.IColorListener.OnColor(Color32 color)
        {
        }
    }
}
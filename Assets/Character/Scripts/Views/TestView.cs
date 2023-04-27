﻿using CityPop.Character;
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
        void TestData.IAddedListener.OnAdded(TestData testData)
        {
        }

        void TestData.IRemovedListener.OnRemoved()
        {
        }

        [UpdateOnInitialize]
        void TestData.ITestListener.OnTest(int test)
        {
        }

        [UpdateOnInitialize]
        void TestData.IColorListener.OnColor(Color32 color)
        {
        }
    }
}
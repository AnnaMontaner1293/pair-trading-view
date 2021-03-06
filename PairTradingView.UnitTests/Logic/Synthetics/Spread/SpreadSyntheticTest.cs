﻿/*
Copyright 2015 Denis Lebedev

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PairTradingView.Data;
using PairTradingView.Logic.Synthetics.Spread;
using System;
using System.Linq;

namespace PairTradingView.UnitTests.Logic.Synthetics.Spread
{
    [TestClass]
    public class SpreadSyntheticTest
    {

        [TestMethod]
        public void Test()
        {
            try
            {
                var synth1 = new SpreadSynthetic(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("values", ex.ParamName);
            }

            var provider = new ExampleDataProvider();

            InputData[] input =
                {
                    new InputData(provider.GetStockInfo("AAPL"), provider.GetValues("AAPL", 100)),
                    new InputData(provider.GetStockInfo("GOOG"), provider.GetValues("GOOG", 100))
                };

            var synth2 = new SpreadSynthetic(input);

            Assert.AreEqual("GOOG|AAPL", synth2.Name);

            Assert.AreNotEqual(null, synth2.Regression);

            Assert.AreEqual(658.88M, synth2.Values.First());
            Assert.AreEqual(634.11M, synth2.Values.Last());

            for (int i = 0; i < 1000; i++)
            {
                var aaplInfo = provider.GetStockInfo("AAPL");
                var googInfo = provider.GetStockInfo("GOOG");

                synth2.StockInfoUpdated(new[] { googInfo, aaplInfo });

                var spread = (googInfo.Price * googInfo.Lot) + (aaplInfo.Price * aaplInfo.Lot);

                Assert.AreEqual(spread, synth2.Value);
            }

            try
            {
                synth2.SetRiskParameters(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("riskParameters", ex.ParamName);
            }

        }
    }
}

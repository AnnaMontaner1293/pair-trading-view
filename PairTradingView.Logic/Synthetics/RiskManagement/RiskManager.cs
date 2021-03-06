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

using Statistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PairTradingView.Logic.Synthetics.RiskManagement
{
    public class RiskManager
    {
        private Synthetic[] synthetics;
        private decimal[] synthIndex;

        public decimal Balance { get; protected set; }

        public RiskManager(Synthetic[] synthetics, decimal balance)
        {
            this.synthetics = synthetics;
            this.Balance = balance;

            SetSynthIndex();
        }

        private void SetSynthIndex()
        {
            synthIndex = new decimal[synthetics.First().Values.Length];

            for (int i = 0; i < synthetics.First().Values.Length; i++)
            {
                decimal value = 0;

                for (int j = 0; j < synthetics.Length; j++)
                {
                    value += synthetics[j].Values[i];
                }

                synthIndex[i] += (value / synthetics.Length);
            }
        }

        public void Calculate()
        {
            var result = new Dictionary<string, RiskParameters>();

            decimal summary = 0;

            foreach (var synthetic in synthetics)
            {
                var regression = new LinearRegression(synthIndex, synthetic.Values);

                RiskParameters p = new RiskParameters(0, 1 / (1 + Math.Abs(regression.Beta)));

                summary += p.Weight;

                result.Add(synthetic.Name, p);
            }

            foreach (var item in result.Values)
            {
                item.Weight = item.Weight / summary;
                item.Balance = this.Balance * item.Weight;
            }

            foreach (var item in synthetics)
            {
                var riskParam = result[item.Name];

                item.SetRiskParameters(riskParam);
            }
        }
    }
}

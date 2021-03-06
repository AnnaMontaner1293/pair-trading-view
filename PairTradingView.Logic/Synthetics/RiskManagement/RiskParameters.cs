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

using System.Collections.Generic;

namespace PairTradingView.Logic.Synthetics.RiskManagement
{
    public class RiskParameters
    {
        public decimal Balance { get; set; }
        public decimal Weight { get; set; }
        public Dictionary<string, decimal> SymbolsBalances { get; private set; }

        public RiskParameters(decimal balance, decimal weight)
        {
            Balance = balance;
            Weight = weight;
            SymbolsBalances = new Dictionary<string, decimal>();
        }
    }
}

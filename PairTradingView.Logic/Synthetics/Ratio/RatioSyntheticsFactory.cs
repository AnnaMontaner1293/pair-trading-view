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

using System.Linq;
using System.Collections.Generic;
using PairTradingView.Data;

namespace PairTradingView.Logic.Synthetics.Ratio
{
    public class RatioSyntheticsFactory : SyntheticsFactory
    {

        public RatioSyntheticsFactory(InputData[] values)
            : base(values)
        {

        }

        public override IEnumerable<Synthetic> CreateSynthetics()
        {
            List<Synthetic> synthetics = new List<Synthetic>();

            for (int i = 0; i < values.Count(); i++)
            {
                for (int j = i + 1; j < values.Count(); j++)
                {
                    var x = values[i];
                    var y = values[j];

                    Synthetic synthetic = new RatioSynthetic(new[] { x, y });

                    synthetics.Add(synthetic);
                }
            }

            return synthetics;
        }
    }
}

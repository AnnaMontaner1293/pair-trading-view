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

using System;

namespace PairTradingView.Data
{
    public class StockInfo
    {
        private string symbol;
        private string name;
        private string type;
        private int lot;
        private decimal price;
        private long volume;


        public string Symbol
        {
            get
            {
                return symbol;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Symbol");

                if (value == string.Empty)
                    throw new ArgumentException("Symbol can't be equals emply string.", "Symbol");

                symbol = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Name");

                if (value == string.Empty)
                    throw new ArgumentException("Name can't be equals emply string.", "Name");

                name = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Type");

                if (value == string.Empty)
                    throw new ArgumentException("Type can't be equals emply string.", "Type");

                type = value;
            }
        }

        public int Lot
        {
            get
            {
                return lot;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Stocks lot can't be less or equals zero.", "Lot");

                lot = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Stocks price can't be less or equals zero.", "Price");

                price = value;
            }
        }

        public long Volume
        {
            get
            {
                return volume;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Stocks volume can't be less than zero.", "Volume");

                volume = value;
            }
        }


        public StockInfo(string symbol, string name, string type,
            int lot, decimal price, long volume)
        {
            Symbol = symbol;
            Name = name;
            Type = type;
            Lot = lot;
            Price = price;
            Volume = volume;
        }
    }
}

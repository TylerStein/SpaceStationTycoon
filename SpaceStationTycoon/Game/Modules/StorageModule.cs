using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Game.Modules
{
    class StorageModule : IModule
    {
        private int _storageTypeIdentifier = 0;

        public int Units { get => Tier; }
        public int Tier { get; }
        public bool IsExternal { get => false; }
        public double BasePrice { get => Tier * 50; }
        public int Identifier { get => "StorageModule".GetHashCode() ^ _storageTypeIdentifier; }
        public int Space { get => Tier * Tier * 1000; }

        public StorageModule(string storageType, int tier) {
            if (tier < 1) throw new Exception("StorageModule tier must be >= 1");
            Tier = tier;
            _storageTypeIdentifier = storageType.GetHashCode();
        }
    }
}

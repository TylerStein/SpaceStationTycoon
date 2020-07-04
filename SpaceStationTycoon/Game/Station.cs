using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpaceStationTycoon.Game
{
    public class Station
    {
        public int TotalExternalUnits { get; private set; }
        public int OccupiedExternalUnits { get; private set; }
        public int FreeExternalUnits { get => TotalExternalUnits - OccupiedExternalUnits; }

        public int TotalInternalUnits { get; private set; }
        public int OccupiedInternalUnits { get; private set; }
        public int FreeInternalUnits { get => TotalInternalUnits - OccupiedInternalUnits; }

        private List<IModule> _modules;
        public ReadOnlyCollection<IModule> Modules { get => _modules.AsReadOnly(); }

        public bool TryAddModule(IModule module) {
            if (module.IsExternal) {
                if (FreeExternalUnits >= module.Units) {
                    _modules.Add(module);
                    OccupiedExternalUnits -= module.Units;
                    return true;
                }
            } else {
                if (FreeInternalUnits >= module.Units) {
                    _modules.Add(module);
                    OccupiedInternalUnits -= module.Units;
                    return true;
                }
            }
            return false;
        }

        public void AddInternalUnits(int count) {
            TotalInternalUnits += count;
        }

        public void AddExternalUnits(int count) {
            TotalExternalUnits += count;
        }
    }
}

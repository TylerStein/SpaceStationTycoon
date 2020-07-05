using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpaceStationTycoon.Gameplay
{
    public class Station
    {
        public int MaxEventHistory { get; set; } = 30;
        public LinkedList<string> EventLog { get; private set; } = new LinkedList<string>();

        public int TotalExternalUnits { get; private set; } = 0;
        public int OccupiedExternalUnits { get; private set; } = 0;
        public int FreeExternalUnits { get => TotalExternalUnits - OccupiedExternalUnits; }

        public int TotalInternalUnits { get; private set; } = 0;
        public int OccupiedInternalUnits { get; private set; } = 0;
        public int FreeInternalUnits { get => TotalInternalUnits - OccupiedInternalUnits; }

        private List<IModule> _modules = new List<IModule>();
        public ReadOnlyCollection<IModule> Modules { get => _modules.AsReadOnly(); }

        public Station(int extraExternalUnits, int extraInternalUnits, IModule[] startingModules) {
            _modules = new List<IModule>();
            foreach (IModule module in startingModules) {
                if (module.IsExternal) TotalExternalUnits += module.Units;
                else TotalInternalUnits += module.Units;
                TryAddModule(module, false);
            }
        }

        public bool TryAddModule(IModule module, bool logEvent) {
            if (module.IsExternal) {
                if (FreeExternalUnits >= module.Units) {
                    _modules.Add(module);
                    OccupiedExternalUnits += module.Units;
                    if (logEvent) LogEvent("Built a module: " + module.GetType().Name);
                    return true;
                }
            } else {
                if (FreeInternalUnits >= module.Units) {
                    _modules.Add(module);
                    OccupiedInternalUnits += module.Units;
                    if (logEvent) LogEvent("Built a module: " + module.GetType().Name);
                    return true;
                }
            }
            if (logEvent) LogEvent("Failed to build a module: " + module.GetType().Name);
            return false;
        }

        public void AddInternalUnits(int count) {
            LogEvent($"Added {count} Internal Units");
            TotalInternalUnits += count;
        }

        public void AddExternalUnits(int count) {
            LogEvent($"Added {count} External Units");
            TotalExternalUnits += count;
        }

        public void LogEvent(string message) {
            EventLog.AddLast(message);
            if (EventLog.Count > MaxEventHistory) {
                EventLog.RemoveFirst();
            }
        }

        public void Update(double deltaTimeSeconds) {
            foreach (IModule module in _modules) {
                module.Update(deltaTimeSeconds);
            }
        }
    }
}

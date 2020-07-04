using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Game.Modules
{
    using Visitors;

    public class HabitationCrewStatus {
        public Crew Crew { get; set; }
        public double Timer { get; set; }
    }

    public class HabitationModule : IModule
    {
        public static int GetIdentifier(int tier) => "HabitationModule".GetHashCode() ^ tier;

        public readonly double RestTimeSeconds = 10;

        public int Tier { get; } = 1;
        public double BasePrice { get => Tier * 50; }
        public int Identifier { get => GetIdentifier(Tier); }
        public bool IsExternal { get => false; }
        public int Units { get => Tier * Tier; }

        public int TotalSlots { get => Tier * Tier * 10; }
        public int OccupiedSlots { get; private set; } = 0;
        public int FreeSlots { get => TotalSlots - OccupiedSlots; }

        public List<HabitationCrewStatus> OccupyingCrew { get; private set; } = new List<HabitationCrewStatus>();

        public HabitationModule(int tier) {
            Tier = tier;
        }

        public void AddCrew(Crew crew) {
            if (FreeSlots < crew.Count) throw new Exception("HabitationModule does not have enough slots to accomodate the requested visitor count!");
             OccupyingCrew.Add(new HabitationCrewStatus() { Crew = crew, Timer = 0.0 });
            OccupiedSlots += crew.Count;
        }

        public void RemoveCrew(Crew crew) {
            int crewIndex = OccupyingCrew.FindIndex((HabitationCrewStatus status) => status.Crew == crew);
            if (crewIndex == -1) {
                throw new Exception("HabitationModule does not contain the crew you tried to remove!");
            }
            OccupyingCrew.RemoveAt(crewIndex);
            OccupiedSlots -= crew.Count;
            crew.Ship.RemoveShipModuleUse(this);
        }

        public void Update(double deltaTimeSeconds) {
            List<Crew> crewToRemove = new List<Crew>();
            foreach (HabitationCrewStatus status in OccupyingCrew) {
                status.Timer += deltaTimeSeconds;
                if (status.Timer >= RestTimeSeconds) {
                    crewToRemove.Add(status.Crew);
                }
            }

            foreach (Crew crew in crewToRemove) {
                RemoveCrew(crew);
            }
        }
    }

    public class HabitationModuleQuery : ModuleQuery<HabitationModule>
    {
        public int MinimumSpace { get; set; }
        public int MinimumTier { get; set; }

        public override bool Check(HabitationModule module) {
            return module.FreeSlots >= MinimumSpace
                && module.Tier >= MinimumTier;
        }
    }
}

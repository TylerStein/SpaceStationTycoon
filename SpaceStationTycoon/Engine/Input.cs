using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using SpaceStationTycoon.Extensions;
namespace SpaceStationTycoon.Engine
{
    public class Input
    {
        private BlockingCollection<ConsoleKeyInfo> _keyInputs = new BlockingCollection<ConsoleKeyInfo>(new ConcurrentStack<ConsoleKeyInfo>(), 64);
        private Task _listenerTask;
        private ConsoleKey _abortKey;
        public bool IsRunning { get; private set; }

        public Input(ConsoleKey abortKey, bool startListening = true) {
            _abortKey = abortKey;
            if (startListening) {
                StartKeyListener();
            }
        }

        public InputState ConsumeLast() {
            ConsoleKeyInfo lastKeyInfo;
            bool hasKey = _keyInputs.TryTake(out lastKeyInfo);
            _keyInputs.Clear();
            return new InputState(hasKey, lastKeyInfo);
        }

        public void StartKeyListener() {
            if (IsRunning) {
                throw new Exception("Input is already listening for key inputs!");
            }

            IsRunning = true;
            _listenerTask = Task.Run(() => {
                while (IsRunning) {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    _keyInputs.Add(keyInfo);
                    if (keyInfo.Key == _abortKey) {
                        IsRunning = false;
                        break;
                    }
                }
            });
        }
    }

    public struct InputState
    {
        public bool HasKey;
        public ConsoleKeyInfo KeyInfo;
        public ConsoleKey Key { get => KeyInfo.Key; }
        public char KeyChar { get => KeyInfo.KeyChar; }

        public InputState(bool hasKey, ConsoleKeyInfo key) {
            HasKey = hasKey;
            KeyInfo = key;
        }
    }
}

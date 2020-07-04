using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon
{
    public class View
    {
        private string[] _currentLines;
        
        public View() {
            Console.Title = "Space Station Tycoon";
            ClearLines();
        }

        public void DrawLines() {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < _currentLines.Length; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write(_currentLines[i]);
            }
            Console.SetCursorPosition(0, 0);
        }

        public void ClearLines() {
            _currentLines = new string[Console.WindowHeight];
            for (int i = 0; i < Console.WindowHeight; i++) {
                _currentLines[i] = "".PadRight(Console.WindowWidth);
            }
        }

        public void SetLineContent(int row, string content) {
            string paddedContent = "";
            if (content.Length > Console.WindowWidth) {
                paddedContent = content.Substring(0, Console.WindowWidth);
            } else {
                paddedContent = content.PadRight(Console.WindowWidth);
            }
            _currentLines[row] = paddedContent;
        }

        public void RenderGameState(GameState state) {
            ClearLines();

            SetLineContent(0, "");
            SetLineContent(1, $"> Press (Enter) to Quit");
            SetLineContent(2, "");

            SetLineContent(3, $"> Credits:  {state.credits} (+{state.TotalIncomePerSecond})");
            SetLineContent(4, $"> Docks:    {state.docks}");

            if (state.credits >= state.dockPrice) {
                SetLineContent(5, $"> Press (1) to build a Dock for {state.dockPrice} Credits");
            }

            DrawLines();
        }
    }
}

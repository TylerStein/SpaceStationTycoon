using System;

namespace SpaceStationTycoon.Engine
{
    public interface IRenderable
    {
        string[] Render();
    }

    public class Renderer
    {
        private string[] _currentLines;
        
        public Renderer() {
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

        public void RenderView(IRenderable view) {
            ClearLines();

            string[] lines = view.Render();
            for (int i = 0; i < lines.Length; i++) {
                SetLineContent(i, lines[i]);
            }

            DrawLines();
        }
    }
}

using System.IO;

namespace Sokoban_Imperative
{
    public class SokobanTester
    {
        static void Test()
        {
            //want to go through a folder, and for each item in the folder, run that puzzle
            string folderPath = "../../testPuzzles";
            string[] puzzles = Directory.GetFiles(folderPath);
            foreach (string puzzle in puzzles)
            {
                
            }
        }
    }
}
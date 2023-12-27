using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace HCI_Project
{
    class TextFileHandler
    {
        public string filePath;

        public TextFileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string> ReadFile()
        {
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Console.WriteLine(line);
                        lines.Add(line);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }

            return lines;
        }

        public void WriteToFile(List<string> lines)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        public void UpdateFile(int lineNumber, string newContent)
        {
            List<string> lines = ReadFile();

            if (lineNumber >= 0 && lineNumber < lines.Count)
            {
                lines[lineNumber] = newContent;
                WriteToFile(lines);
            }
            else
            {
                Console.WriteLine("Invalid line number for update.");
            }
        }

        public void DeleteLine(int lineNumber)
        {
            List<string> lines = ReadFile();

            if (lineNumber >= 0 && lineNumber < lines.Count)
            {
                lines.RemoveAt(lineNumber);
                WriteToFile(lines);
            }
            else
            {
                Console.WriteLine("Invalid line number for deletion.");
            }
        }
    }
}

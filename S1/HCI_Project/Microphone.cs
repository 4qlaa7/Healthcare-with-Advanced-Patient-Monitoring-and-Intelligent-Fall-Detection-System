using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace HCI_Project
{
    class Microphone
    {
        SpeechRecognitionEngine recognizer;
        public int value=0;
        public Microphone()
        {
            recognizer = new SpeechRecognitionEngine();
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(new Choices("Home", "go to home", "open home", "main menu", "start page",
            "Rooms", "show rooms", "list rooms", "available rooms",
            "SOS", "call for help", "emergency", "urgent assistance",
            "History", "view history", "check past actions",
            "How to use", "help", "instructions", "guide",
            "Exit", "quit", "close", "stop"))));

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(RecognizedSpeech);
        }        

        private void RecognizedSpeech(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text;
            Console.WriteLine(command);
            switch (command)
            {
                case "Home":
                case "go to home":
                case "open home":
                case "main menu":
                case "start page":
                    value = 0;                  
                    break;
                case "Rooms":
                case "show rooms":
                case "list rooms":
                case "available rooms":                    
                    value = 1;
                    break;
                case "SOS":
                case "call for help":
                case "emergency":
                case "urgent assistance":
                    value = 2;
                    break;
                case "History":
                case "view history":
                case "check past actions":
                    value = 3;
                    break;
                case "How to use":
                case "help":
                case "instructions":
                case "guide":
                    value = 4;
                    break;
                case "Exit":
                case "quit":
                case "close":
                case "stop":
                    value = 5;
                    break;
                default:
                    Console.WriteLine("Unrecognized command: " + command);
                    break;
            }
        }
        public void StartListening()
        {
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
        public void StopListening()
        {
            recognizer.RecognizeAsyncStop();
        }
    }
}

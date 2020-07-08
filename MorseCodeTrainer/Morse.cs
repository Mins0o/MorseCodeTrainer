using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MorseCodeTrainer
{
    class Morse
    {
        private string userInput;
        private string morseCode;
        private Dictionary<char, string> engMorse = new Dictionary<char, string>()
        {
            {'A',"12"}, {'B',"2111"},   {'C',"2121"},   {'D',"211"},    {'E',"1"},  {'F',"1121"},   {'G',"221"},    {'H',"1111"},   {'I',"11"}, {'J',"1222"},   {'K',"212"},    {'L',"1211"},   {'M',"22"}, {'N',"21"}, {'O',"222"},    {'P',"1221"},   {'Q',"2212"},   {'R',"121"},    {'S',"111"},    {'T',"2"},  {'U',"112"},    {'V',"1112"},   {'W',"122"},    {'X',"2112"},   {'Y',"2122"},   {'Z',"2211"},   {'a',"12"}, {'b',"2111"},   {'c',"2121"},   {'d',"211"},    {'e',"1"},  {'f',"1121"},   {'g',"221"},    {'h',"1111"},   {'i',"11"}, {'j',"1222"},   {'k',"212"},    {'l',"1211"},   {'m',"22"}, {'n',"21"}, {'o',"222"},    {'p',"1221"},   {'q',"2212"},   {'r',"121"},    {'s',"111"},    {'t',"2"},  {'u',"112"},    {'v',"1112"},   {'w',"122"},    {'x',"2112"},   {'y',"2122"},   {'z',"2211"},   {'0',"22222"},  {'1',"12222"},  {'2',"11222"},  {'3',"11122"},  {'4',"11112"},  {'5',"11111"},  {'6',"21111"},  {'7',"22111"},  {'8',"22211"},  {'9',"22221"},  {' ',"00"},  {'.',"0000"},  {',',"000"},  {'\'',""}
        };
        public Morse(string userInput)
        {
            this.userInput = userInput;
            this.morseCode = "";
            for (int i = 0; i < userInput.Length; i++)
            {
                try
                {
                    var nextPattern = engMorse[userInput[i]];
                    morseCode = morseCode + nextPattern+"0";
                }
                catch
                {
                    System.Windows.MessageBox.Show(string.Format("Wrong character{0}", userInput[i]));
                    var nextPattern = "0000";
                    morseCode = morseCode + nextPattern;
                }
            }
        }
        public string translateToMorse()
        {
            return morseCode;
        }
    }
}

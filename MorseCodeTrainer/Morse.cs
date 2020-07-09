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
            {'A',"12"}, {'B',"2111"},   {'C',"2121"},   {'D',"211"},    {'E',"1"},  {'F',"1121"},   {'G',"221"},    {'H',"1111"},   {'I',"11"}, {'J',"1222"},   {'K',"212"},    {'L',"1211"},   {'M',"22"}, {'N',"21"}, {'O',"222"},    {'P',"1221"},   {'Q',"2212"},   {'R',"121"},    {'S',"111"},    {'T',"2"},  {'U',"112"},    {'V',"1112"},   {'W',"122"},    {'X',"2112"},   {'Y',"2122"},   {'Z',"2211"},   {'a',"12"}, {'b',"2111"},   {'c',"2121"},   {'d',"211"},    {'e',"1"},  {'f',"1121"},   {'g',"221"},    {'h',"1111"},   {'i',"11"}, {'j',"1222"},   {'k',"212"},    {'l',"1211"},   {'m',"22"}, {'n',"21"}, {'o',"222"},    {'p',"1221"},   {'q',"2212"},   {'r',"121"},    {'s',"111"},    {'t',"2"},  {'u',"112"},    {'v',"1112"},   {'w',"122"},    {'x',"2112"},   {'y',"2122"},   {'z',"2211"},   {'0',"22222"},  {'1',"12222"},  {'2',"11222"},  {'3',"11122"},  {'4',"11112"},  {'5',"11111"},  {'6',"21111"},  {'7',"22111"},  {'8',"22211"},  {'9',"22221"},  {' ',"00"},  {'.',"0000"},  {',',"000"},  {'\'',""}, {'ㄱ',"1211"},  {'ㄴ',"1121"},  {'ㄷ',"2111"},  {'ㄹ',"1112"},  {'ㅁ',"22"},    {'ㅂ',"122"},   {'ㅅ',"221"},   {'ㅇ',"212"},   {'ㅈ',"1221"},  {'ㅊ',"2121"},  {'ㅋ',"2112"},  {'ㅌ',"2211"},  {'ㅍ',"222"},   {'ㅎ',"1222"},  {'ㅏ',"1"},  {'ㅐ',"2212"},   {'ㅑ',"11"}, {'ㅓ',"2"},  {'ㅔ',"2122"},   {'ㅕ',"111"},    {'ㅗ',"12"}, {'ㅛ',"21"}, {'ㅜ',"1111"},   {'ㅠ',"121"},    {'ㅡ',"211"},    {'ㅣ',"112"}
        };
        private string[] korFirstConsonant = { "ㄱ", "ㄱㄱ", "ㄴ", "ㄷ", "ㄷㄷ", "ㄹ", "ㅁ", "ㅂ", "ㅂㅂ", "ㅅ", "ㅅㅅ", "ㅇ", "ㅈ", "ㅈㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        private string[] korVowel ={"ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅗㅏ", "ㅗㅐ", "ㅗㅣ", "ㅛ", "ㅜ", "ㅜㅓ", "ㅜㅔ", "ㅜㅣ", "ㅠ", "ㅡ", "ㅡㅣ", "ㅣ"};
        private string[] korLastConsonant = { " ", "ㄱ", "ㄱㄱ", "ㄱㅅ", "ㄴ", "ㄴㅈ", "ㄴㅎ", "ㄷ", "ㄹ", "ㄹㄱ", "ㄹㅁ", "ㄹㅂ", "ㄹㅅ", "ㄹㅌ", "ㄹㅍ", "ㄹㅎ", "ㅁ", "ㅂ", "ㅂㅅ", "ㅅ", "ㅅㅅ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        public Morse(string userInput)
        {
            this.userInput = KoreanSeparator(userInput);
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
                    System.Diagnostics.Debug.Print(userInput[i].ToString());
                    var nextPattern = "0";
                    morseCode = morseCode + nextPattern;
                }
            }
        }
        public string translateToMorse()
        {
            return morseCode;
        }
        private string KoreanSeparator(string userInput)
        {
            string processed = "";
            for (int i = 0; i < userInput.Length; i++)
            {
                int offsetFromGa = userInput[i] - '가';
                if (offsetFromGa >= 0 && offsetFromGa <= '힣' - '가')
                {
                    int consonant1 = (offsetFromGa) / 588;
                    int vowel = (offsetFromGa % 588) / 28;
                    int consonant2 = (offsetFromGa % 588) % 28;
                    processed += korFirstConsonant[consonant1] + korVowel[vowel] + korLastConsonant[consonant2];
                }
                else { processed += userInput[i].ToString(); }

            }
            return processed;
        }
    }
}

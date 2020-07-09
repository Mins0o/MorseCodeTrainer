﻿using System;
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
        private Dictionary<char, string> morseMap = new Dictionary<char, string>()
        {
            {'A',"12"}, {'B',"2111"},   {'C',"2121"},   {'D',"211"},    {'E',"1"},  {'F',"1121"},   {'G',"221"},    {'H',"1111"},   {'I',"11"}, {'J',"1222"},   {'K',"212"},    {'L',"1211"},   {'M',"22"}, {'N',"21"}, {'O',"222"},    {'P',"1221"},   {'Q',"2212"},   {'R',"121"},    {'S',"111"},    {'T',"2"},  {'U',"112"},    {'V',"1112"},   {'W',"122"},    {'X',"2112"},   {'Y',"2122"},   {'Z',"2211"},   {'a',"12"}, {'b',"2111"},   {'c',"2121"},   {'d',"211"},    {'e',"1"},  {'f',"1121"},   {'g',"221"},    {'h',"1111"},   {'i',"11"}, {'j',"1222"},   {'k',"212"},    {'l',"1211"},   {'m',"22"}, {'n',"21"}, {'o',"222"},    {'p',"1221"},   {'q',"2212"},   {'r',"121"},    {'s',"111"},    {'t',"2"},  {'u',"112"},    {'v',"1112"},   {'w',"122"},    {'x',"2112"},   {'y',"2122"},   {'z',"2211"},   {'0',"22222"},  {'1',"12222"},  {'2',"11222"},  {'3',"11122"},  {'4',"11112"},  {'5',"11111"},  {'6',"21111"},  {'7',"22111"},  {'8',"22211"},  {'9',"22221"},  {' ',"00"},  {'.',"0000"},  {',',"000"},  {'\'',""}, {'ㄱ',"1211"},  {'ㄴ',"1121"},  {'ㄷ',"2111"},  {'ㄹ',"1112"},  {'ㅁ',"22"},    {'ㅂ',"122"},   {'ㅅ',"221"},   {'ㅇ',"212"},   {'ㅈ',"1221"},  {'ㅊ',"2121"},  {'ㅋ',"2112"},  {'ㅌ',"2211"},  {'ㅍ',"222"},   {'ㅎ',"1222"},  {'ㅏ',"1"},  {'ㅐ',"2212"},   {'ㅑ',"11"}, {'ㅓ',"2"},  {'ㅔ',"2122"},   {'ㅕ',"111"},    {'ㅗ',"12"}, {'ㅛ',"21"}, {'ㅜ',"1111"},   {'ㅠ',"121"},    {'ㅡ',"211"},    {'ㅣ',"112"}, {'Á',"12"},{'Ấ',"12"},{'Ắ',"12"},{'Ề',"1"},{'É',"1"},{'Ó',"222"},{'Ồ',"222"},{'Ớ',"222"},{'Ụ',"112"},{'Ứ',"112"},{'á',"12"},{'ấ',"12"},{'ắ',"12"},{'ề',"1"},{'é',"1"},{'ó',"222"},{'ồ',"222"},{'ớ',"222"},{'ụ',"112"},{'ứ',"112"},{'À',"12"},{'Ầ',"12"},{'Ằ',"12"},{'Ế',"1"},{'È',"1"},{'Ò',"222"},{'Ộ',"222"},{'Ờ',"222"},{'Ú',"112"},{'Ừ',"112"},{'à',"12"},{'ầ',"12"},{'ằ',"12"},{'ế',"1"},{'è',"1"},{'ò',"222"},{'ộ',"222"},{'ờ',"222"},{'ú',"112"},{'ừ',"112"},{'Ã',"12"},{'Ẫ',"12"},{'Ẵ',"12"},{'Ễ',"1"},{'Ẽ',"1"},{'Õ',"222"},{'Ổ',"222"},{'Ở',"222"},{'Ũ',"112"},{'Ữ',"112"},{'ã',"12"},{'ẫ',"12"},{'ẵ',"12"},{'ễ',"1"},{'ẽ',"1"},{'õ',"222"},{'ổ',"222"},{'ở',"222"},{'ũ',"112"},{'ữ',"112"},{'Ả',"12"},{'Ẩ',"12"},{'Ẳ',"12"},{'Ể',"1"},{'Ẻ',"1"},{'Ỏ',"222"},{'Ỗ',"222"},{'Ợ',"222"},{'Ủ',"112"},{'Ử',"112"},{'ả',"12"},{'ẩ',"12"},{'ẳ',"12"},{'ể',"1"},{'ẻ',"1"},{'ỏ',"222"},{'ỗ',"222"},{'ợ',"222"},{'ủ',"112"},{'ử',"112"},{'Ạ',"12"},{'Ậ',"12"},{'Ặ',"12"},{'Ệ',"1"},{'Ẹ',"1"},{'Ọ',"222"},{'Ố',"222"},{'Ỡ',"222"},{'Ù',"112"},{'Ự',"112"},{'ạ',"12"},{'ậ',"12"},{'ặ',"12"},{'ệ',"1"},{'ẹ',"1"},{'ọ',"222"},{'ố',"222"},{'ỡ',"222"},{'ù',"112"},{'ự',"112"},{'Ă',"12"},{'Â',"12"},{'Đ',"211"},{'Ê',"1"},{'Ô',"222"},{'Ơ',"222"},{'Ư',"112"},{'ă',"12"},{'â',"12"},{'đ',"211"},{'ê',"1"},{'ô',"222"},{'ơ',"222"},{'ư',"112"}

        };
        private string[] korFirstConsonant = { "ㄱ", "ㄱㄱ", "ㄴ", "ㄷ", "ㄷㄷ", "ㄹ", "ㅁ", "ㅂ", "ㅂㅂ", "ㅅ", "ㅅㅅ", "ㅇ", "ㅈ", "ㅈㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        private string[] korVowel ={"ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅗㅏ", "ㅗㅐ", "ㅗㅣ", "ㅛ", "ㅜ", "ㅜㅓ", "ㅜㅔ", "ㅜㅣ", "ㅠ", "ㅡ", "ㅡㅣ", "ㅣ"};
        private string[] korLastConsonant = { " ", "ㄱ", "ㄱㄱ", "ㄱㅅ", "ㄴ", "ㄴㅈ", "ㄴㅎ", "ㄷ", "ㄹ", "ㄹㄱ", "ㄹㅁ", "ㄹㅂ", "ㄹㅅ", "ㄹㅌ", "ㄹㅍ", "ㄹㅎ", "ㅁ", "ㅂ", "ㅂㅅ", "ㅅ", "ㅅㅅ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        public Morse(string userInp)
        {
            this.userInput = KoreanSeparator(userInp);
            this.morseCode = "";
            for (int i = 0; i < userInput.Length; i++)
            {
                try
                {
                    var nextPattern = morseMap[userInput[i]];
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

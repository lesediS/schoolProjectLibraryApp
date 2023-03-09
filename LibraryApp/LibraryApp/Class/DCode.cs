using System;
using System.Linq;

namespace LibraryApp.Class
{
    public class DCode
    {
        public string bookCode { get; set; }
        public string decCode { get; set; }
        public string authorCode { get; set; }
        public string Code { get; set; }


        private static readonly Random Random = new Random();

        public DCode()
        {
            //(see How to Generate Random Alphanumeric in C#, 2017) A YouTube video by JE Tutoriales
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //(see C# Generate Random Letter, 2019) This is a YouTube video by code technique
            const string numbers = "0123456789";

             /*Randomising elements for call numbers and adding them to an array.
              * This code is from (and adapted from) Chand, 2013 from C# Corner on how to randomly generate numbers and letters.
              * 
             */
            var num = Enumerable.Repeat(numbers, 3)
                .Select(s => s[Random.Next(s.Length)]).ToArray();
            var dec = Enumerable.Repeat(numbers, 2)
                .Select(s => s[Random.Next(0,9)]).ToArray();
            var str = Enumerable.Repeat(chars, 3)
                .Select(s => s[Random.Next(s.Length)]).ToArray();

            bookCode = new string (num);
            decCode = new string( dec);
            authorCode = new string(str);

            Code = bookCode + "." + decCode + authorCode; //Call number
        }
    }
}
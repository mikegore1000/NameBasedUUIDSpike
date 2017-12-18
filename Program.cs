using System;
using System.Collections.Generic;

namespace NameBasedUUIDSpike
{
    class Program
    {
        private static Guid UuidNamespace = new Guid("b4512622-d725-45e1-81fc-c322f2a423b4");
        private static HashSet<Guid> Generated = new HashSet<Guid>();

        static void Main(string[] args)
        {
            try
            {
                long progress = 0;
                Combination("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", value => {
                    var a = GuidUtility.Create(UuidNamespace, value);
                    Generated.Add(UuidNamespace);

                    if(++progress%10000 == 0)
                    {
                        Console.WriteLine($"Generated {progress} UUIDs under namespace {UuidNamespace}");
                    }
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Possible collission. Error: {ex.Message}");
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void Combination(string str, Action<string> valueGenerated)
        {
            // Working buffer to build new sub-strings
            char[] buffer = new char[str.Length];

            CombinationRecurse(str.ToCharArray(), 0, buffer, 0, valueGenerated);
        }

        public static void CombinationRecurse(char[] input, int inputPos, char[] buffer, int bufferPos, Action<string> valueGenerated)
        {
            if (inputPos >= input.Length)
            {
                // Add only non-empty strings
                if (bufferPos > 0)
                    valueGenerated(new string(buffer, 0, bufferPos));

                return;
            }

            // Recurse 2 times - one time without adding current input char, one time with.
            CombinationRecurse(input, inputPos + 1, buffer, bufferPos, valueGenerated);

            buffer[bufferPos] = input[inputPos];
            CombinationRecurse(input, inputPos + 1, buffer, bufferPos + 1, valueGenerated);
        }
    }
}

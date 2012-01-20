using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleRulesEngine;
using SimpleRulesEngine.Evaluators;
using SimpleRulesEngine.Rules;

namespace SimpleConsoleApp
{
    internal class Program
    {
        private static int _startInt, _endInt;
        private static string _rulesFilePath = "rules.file";
        private static IEnumerable<IRule> _rulesFromFile;

        private static void Main(string[] args)
        {
            var matchingRulesAggregator = new MatchingRulesAggregator();
            if (!ParseArguments(args))
            {
                Console.ReadLine();
                return;
            }
            List<int> ints = Enumerable.Range(_startInt, _endInt).ToList();
            if (!GetRulesFromFile())
            {
                Console.ReadLine();
                return;
            }

            ints.ForEach(x =>
                         Console.WriteLine(matchingRulesAggregator
                                               .StringToWrite(_rulesFromFile, x)));

            Console.ReadLine();
        }

        private static bool ParseArguments(string[] args)
        {
            try
            {
                _startInt = int.Parse(args.First(x => x.StartsWith("s=")).Substring(2));
                _endInt = int.Parse(args.First(x => x.StartsWith("e=")).Substring(2));
                if (args.Any(x => x.StartsWith("f=")))
                    _rulesFilePath = args.First(x => x.StartsWith("f=")).Substring(2);
            }
            catch (Exception e)
            {
                WriteException(e.Message);
                return false;
            }
            if (_startInt >= _endInt)
            {
                WriteException("starting number must be smaller than ending");
                return false;
            }
            return true;
        }

        private static void WriteException(string message)
        {
            Console.Write("CheckingIntegers: ");
            Console.WriteLine(message);
            Console.WriteLine(@"usage ""CheckingIntegers s={START INT} e={END INT} f={FILE WITH RULES}""");
        }

        private static bool GetRulesFromFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(_rulesFilePath);
                _rulesFromFile = lines.Select(l =>
                                                  {
                                                      string[] vals = l.Split(',');
                                                      return new DivisibleByRule(divisor: int.Parse(vals[0]),
                                                                                 alternateResult: vals[1])
                                                          ;
                                                  });
            }
            catch (Exception e)
            {
                WriteException("Rules file cannot be parsed.\n" + e.Message + Environment.NewLine);
                return false;
            }
            return true;
        }
    }
}
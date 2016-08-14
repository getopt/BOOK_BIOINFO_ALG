using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BioinfoAlgorithms
{
    class RunChapter02:Chapter02
    {
        public RunChapter02(string excercise)
        {
            Chapter02 chapter = new Chapter02();

            List<string> dnaStrings;
            List<ProfileMatrixEntry> pm;
            List<string> bestProfile;
            int k;
            int d;
            switch (excercise)
            {
                case "2A":
                    dnaStrings = new List<string>
                    {
                        "AAAAAAAAATTTAAAAAAA",
                        "AAAAATATTTAAAAAA",
                        "AAAAATTTTAAAAA",
                    };
                    k = 4;
                    d = 0;
                    Console.WriteLine(string.Join("\n", chapter.MotifEnumerator(dnaStrings, k, d)));
                    Console.ReadLine();
                    break;
                case "2B":
                    dnaStrings = new List<string>
                    {
                        "TCTCTCTCTC",
                        "TGTGTGTGTG",
                        "TTTTTTTTTT",
                    };
                    k = 5;
                    Console.WriteLine(chapter.MedianString(dnaStrings, k));
                    Console.ReadLine();
                    break;
                case "2C":
                    dnaStrings = new List<string> {"ATGC",
                                              "TACG",
                                              "GGGG",
                                              "CAAA" };
                    pm = chapter.MotifsToProfileMatrix(dnaStrings);
                    string dna = "AAATGCTCGGAA";
                    k = 4;
                    Console.WriteLine(chapter.MostProbableKmer(dna, k, pm));
                    chapter.PrintPm(pm);
                    break;
                case "2D":
                    dnaStrings = new List<string> {"TTACCTTAACTTTT",
                                                   "GATGTCTGTCACCT",
                                                   "ACGGCGTTAGACCT",
                                                   "CCCTAACGAGACCT",
                                                   "CGTCAGAGGTACCT"};
                    k = 4;
                    bestProfile = GreedyMotifSearch(dnaStrings,k,dnaStrings.Count);
                    PrintProfile(bestProfile, "Best profile:");
                    Console.ReadLine();
                    break;
                case "2E":
                    dnaStrings = new List<string> {"TTACCTTAAC",
                                                   "GATGTCTGTC",
                                                   "ACGGCGTTAG",
                                                   "CCCTAACGAG",
                                                   "CGTCAGAGGT"};
                    k = 4;
                    bestProfile = GreedyMotifSearchWithPseudoCounts(dnaStrings,k,dnaStrings.Count);
                    PrintProfile(bestProfile, "Best profile:");
                    Console.ReadLine();
                    break;
                case "2H":
                    dnaStrings = new List<string>
                    {
                        "AAAAAAAAATTTAAAAAAA",
                        "AAAAATATTTAAAAAAGCG",
                        "AAAAATTTTAAAAAGGATT",
                    };
                    string pattern = "AAAAG";
                    Console.WriteLine(chapter.DistanceBetweenPatternAndStrings(pattern, dnaStrings).ToString());
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Cannot interpret exercise number");
                    Console.ReadLine();
                    break;
            }
        }
    }

    class Chapter02:Chapter01
    {
        /// <summary>
        /// The same algorythm GreedyMotifSearch, but when pseudocounts are added when generating
        /// profile matrice (wth MotifsToProfileMatrixWithPsudoCounts()).
        /// Greey motif algorythms trie to find the most probable profile matrix by starting from 
        /// any of the k-mers in first dnaString from dnaStrings. The pseudocount trick is known
        /// as Laplace's Rule of succession.
        /// </summary>
        public List<string> GreedyMotifSearchWithPseudoCounts(List<string> dnaStrings, int k, int t)
        {
            // get pm from first k-mers in each dnaString
            List<string> bestMotifs = new List<string>();
            foreach (string dnaString in dnaStrings)
            {
                string motif = dnaString.Substring(0, k);
                bestMotifs.Add(motif);
            }
            PrintProfile(bestMotifs, "First profile:");

            // loop through every k-mer of the first dnaString
            List<int[]> windows = StringSlidingWindows(dnaStrings.First(), k);

            foreach (int[] window in windows)
            {
                string motif = dnaStrings.First().Substring(window[0], k);

                List<string> motifs = new List<string> {motif};

                for (int i = 1; i < t; i++)
                {
                    string motifCurrent = MostProbableKmer(dnaStrings[i], k, 
                                    MotifsToProfileMatrixWithPseudoCounts(motifs));
                    motifs.Add(motifCurrent);
                }
                PrintProfile(motifs);

                if(ScoreProfileMatrix(MotifsToProfileMatrixWithPseudoCounts(motifs)) < 
                                ScoreProfileMatrix(MotifsToProfileMatrixWithPseudoCounts(bestMotifs)))
                {
                    bestMotifs = motifs;
                }
            }
            return bestMotifs;
        }

        /// <summary>
        /// Tries to find most probable profile matrix by starting from any of the k-mers 
        /// in first dnaString from dnaStrings.
        /// </summary>
        public List<string> GreedyMotifSearch(List<string> dnaStrings, int k, int t)
        {
            // get pm from first k-mers in each dnaString
            List<string> bestMotifs = new List<string>();
            foreach (string dnaString in dnaStrings)
            {
                string motif = dnaString.Substring(0, k);
                bestMotifs.Add(motif);
            }
            PrintProfile(bestMotifs, "First profile:");

            // loop through every k-mer of the first dnaString
            List<int[]> windows = StringSlidingWindows(dnaStrings.First(), k);

            foreach (int[] window in windows)
            {
                string motif = dnaStrings.First().Substring(window[0], k);

                List<string> motifs = new List<string> {motif};

                for (int i = 1; i < t; i++)
                {
                    string motifCurrent = MostProbableKmer(dnaStrings[i], k, MotifsToProfileMatrix(motifs));
                    motifs.Add(motifCurrent);
                }
                PrintProfile(motifs);

                if(ScoreProfileMatrix(MotifsToProfileMatrix(motifs)) < ScoreProfileMatrix(MotifsToProfileMatrix(bestMotifs)))
                {
                    bestMotifs = motifs;
                }
            }
            return bestMotifs;
        }

        /// <summary>
        /// Print strings in profile to console with a message
        /// </summary>
        public void PrintProfile(List<string> profile, string message)
        {
            Console.WriteLine(message);
            foreach (string text in profile)
                Console.WriteLine(text);
            Console.WriteLine("Score: " + ScoreProfileMatrix(MotifsToProfileMatrix(profile)).ToString());
            Console.WriteLine();
        }
        /// <summary>
        /// Print strings in profile to console 
        /// </summary>
        public void PrintProfile(List<string> profile)
        {
            foreach (string text in profile)
                Console.WriteLine(text);
            Console.WriteLine("Score: " + ScoreProfileMatrix(MotifsToProfileMatrix(profile)).ToString());
            Console.WriteLine();
        }
        
        /// <summary>
        /// Given profile matrix 'pm' return 'scoreFrac', which is the fractional 
        /// score of the profile matrix equal to the sum of probabilies of lowercase
        /// (i.e. unpopular) nucleotides. Note that if two nucleotides have equal
        /// probability in pm, then the popular nucleotide is chosen at random
        /// between the two.
        /// </summary>
        public double ScoreProfileMatrix(List<ProfileMatrixEntry> pm)
        {
            double scoreFrac = 0.0;

            Dictionary<string, double> ntPosition = new Dictionary<string, double>();
            foreach (string nt in Alphabet) 
                ntPosition[nt] = 0.0;

            for (int i = 0; i < pm.Count/Alphabet.Count; i++)
            {
                foreach(string nt in Alphabet)
                {
                    IEnumerable<double> entries = from a in pm
                                                  where a.Base == nt
                                                  where a.Pos == i
                                                  select a.Prob;

                    ntPosition[nt] = entries.First();
                }

                double maxProb = 0.0;
                foreach (string nt in ntPosition.Keys)
                    if (maxProb < ntPosition[nt])
                        maxProb = ntPosition[nt];
                double lowerCaseLettersProb = 1.0 - maxProb;
                scoreFrac += lowerCaseLettersProb;
            }
            return scoreFrac;
        } 
        
        /// <summary>
        /// Returns a profile matrix given a list of dnaStrings sequences 'dnaStrings'.
        /// MotifsToProfileMatrixWithPseudoCounts() uses additional pseudo count of 1
        /// for four nucleotides in order to prevent profile matrix from having 0
        /// probablities. The pseudocount trick is known as Laplace's rule of succession.
        /// </summary>
        public List<ProfileMatrixEntry> MotifsToProfileMatrixWithPseudoCounts(List<string> dnaStrings)
        {
            List<ProfileMatrixEntry> pm = new List<ProfileMatrixEntry>();

            for(int i = 0; i < dnaStrings.First().Length; i++)
            {
                List<string> sequence = new List<string>();

                Dictionary<string, int> ntPosition = new Dictionary<string, int>();
                foreach (string nt in Alphabet) {
                    // ntPosition[nt] = 0;
                    ntPosition[nt] = 1;  // initialzing nt counts at 1 which serves as a pseudocount
                }

                for (int j = 0; j < dnaStrings.Count; j++)
                {
                    if (dnaStrings[j].Length != dnaStrings.First().Length)
                    {
                        Console.WriteLine("DNA strings are of unequal length!");
                        Environment.Exit(1); 
                    }
                
                    string currentDna = dnaStrings[j];
                    string currentNt = currentDna[i].ToString();
                    ntPosition[currentNt] += 1;
                }

                foreach (string nt in Alphabet)
                {
                    double prob = ntPosition[nt]/(double)dnaStrings.Count;
                    pm.Add(new ProfileMatrixEntry {Base = nt, Pos = i, Prob = prob});
                }
            }

            return pm;
        }
           
        /// <summary>
        /// Returns a profile matrix given a list of dnaStrings sequences 'dnaStrings'
        /// </summary>
        public List<ProfileMatrixEntry> MotifsToProfileMatrix(List<string> dnaStrings)
        {
            List<ProfileMatrixEntry> pm = new List<ProfileMatrixEntry>();

            for(int i = 0; i < dnaStrings.First().Length; i++)
            {
                List<string> sequence = new List<string>();

                Dictionary<string, int> ntPosition = new Dictionary<string, int>();
                foreach (string nt in Alphabet) {
                    ntPosition[nt] = 0;
                }

                for (int j = 0; j < dnaStrings.Count; j++)
                {
                    if (dnaStrings[j].Length != dnaStrings.First().Length)
                    {
                        Console.WriteLine("DNA strings are of unequal length!");
                        Environment.Exit(1); 
                    }
                
                    string currentDna = dnaStrings[j];
                    string currentNt = currentDna[i].ToString();
                    ntPosition[currentNt] += 1;
                }

                foreach (string nt in Alphabet)
                {
                    double prob = ntPosition[nt]/(double)dnaStrings.Count;
                    pm.Add(new ProfileMatrixEntry {Base = nt, Pos = i, Prob = prob});
                }
            }

            return pm;
        }
        
        /// <summary>
        /// Print profile matrix 'profileMatrix' to console
        /// </summary>
        public void PrintPm(List<ProfileMatrixEntry> profileMatrix)
        {
            int PmLength = profileMatrix.Count/Alphabet.Count;
            foreach (string nt in Alphabet)
            {
                Console.Write(nt);
                for (int i = 0; i < PmLength; i++)
                {
                    IEnumerable<double> entries = from a in profileMatrix
                                                  where a.Base == nt
                                                  where a.Pos == i
                                                  select a.Prob;

                    Console.Write("\t" + entries.First().ToString());
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        
        /// <summary>
        /// Given a sequence 'text' and a profile matrix 'profileMatrix', return a
        /// k-mer from text that is most probable given the 'profileMatrix'  
        /// </summary>
        public string MostProbableKmer( string text, int k, List<ProfileMatrixEntry> profileMatrix )
        {
            string pattern = text.Substring(0,k);
            double prob = 0.0;

            List<int[]> windows = StringSlidingWindows(text, k);

            foreach (int[] window in windows)
            {
                string currentPattern = text.Substring(window[0], k);
                double currentProb = KmerProbabilityFromPm(profileMatrix, currentPattern);
                // Console.WriteLine(currentProb.ToString());
                if (currentProb > prob)
                {
                    pattern = currentPattern;
                    prob = currentProb;
                }
            }

            return pattern;
        }
        
        /// <summary>
        /// Return the probabiliy of a sequence 'pattern' given profile matrix 'pm'
        /// </summary>
        public double KmerProbabilityFromPm(List<ProfileMatrixEntry> pm, string pattern)
        {
            double prob = 1.0;
            for (int i = 0; i < pattern.Length; i++)
            {
                var i1 = i;
                var i2 = i;
                IEnumerable<double> currentProbs = from a in pm
                    where a.Base == pattern[i1].ToString()
                    where a.Pos == i2
                    select a.Prob;

                prob *= currentProbs.First();
            }

            return prob;
        }
        
        /// <summary>
        /// Find a k-mer pattern that minimazies d(pattern, dnaString)
        /// over all k-mers in dnaStrings 
        /// </summary>
        public string MedianString(List<string> dnaStrings, int k)
        {
            string median = "";

            int distance = 1000000;
            for (int i = 0; i < Math.Pow(EnumUtil.GetValues<BioinfoAlgorithms.AlphabetEnum>().Count(), k) ; i++)
            {
                string pattern = NumberToPattern(i, k);
                int currentDist = DistanceBetweenPatternAndStrings(pattern, dnaStrings);
                if (distance > currentDist)
                {
                    distance = currentDist;
                    median = pattern;
                }

            }

            return median;
        }

        /// <summary>
        /// The sum of distances between pattern and each string in dnaStrings
        /// </summary>
        public int DistanceBetweenPatternAndStrings(string pattern, List<string> dnaStrings)
        {
            int k = pattern.Length;
            int distance = 0;

            foreach (string text in dnaStrings)
            {
                int hammingDistance = 1000000;
                List<int[]> windows = StringSlidingWindows(text, k);
                foreach (int[] window in windows)
                {
                    string patternP = text.Substring(window[0], k);
                    int latestHammingDist = HammingDistance(pattern, patternP); 
                    if (hammingDistance > latestHammingDist)
                    {
                        hammingDistance = latestHammingDist;
                    }
                }

                distance += hammingDistance;
            }

            return distance;
        }
        
        /// <summary>
        /// Brute force approach to solving Implanted Motif Problem.
        /// It's based on observation that any (k,d)-motif must be at most d mismatches
        /// apart from some k-mer appearing in one of the strings of dnaStrings.
        /// </summary>
        public List<string> MotifEnumerator(List<string> dnaStrings, int k, int d)
        {   
            var patternsDict = new MyListDictionary();

            foreach (string dna in dnaStrings)
            {
                List<int[]> windows = StringSlidingWindows(dna, k);
                foreach (int[] window in windows)
                {
                    string pattern = dna.Substring(window[0], k);
                    List<string> neighbors = Neighbors(pattern, d);
                    foreach (string neighbor in neighbors)
                    {
                        foreach (string dnaP in dnaStrings)
                        {
                            List<int[]> windowsP = StringSlidingWindows(dnaP, k);
                            foreach (int[] windowP in windowsP)
                            {
                                string patternP = dnaP.Substring(windowP[0], k);
                                if (neighbor == patternP)
                                {
                                    patternsDict.Add(pattern,dnaP);
                                }
                            }
                        } 
                    } 
                }
            }

            var patternsList = new List<string>();
            var patterns = new List<string>(patternsDict.InternalDictionary.Keys);
            foreach (string pattern in patterns)
            {
                if (patternsDict.InternalDictionary[pattern].Count == dnaStrings.Count)
                {
                   patternsList.Add(pattern); 
                }
                
            }
            return patternsList;
        }

        /// <summary>
        /// Generate list of overlapping k-wide windows covering sequence  
        /// </summary>
        public List<int[]> StringSlidingWindows(string sequence, int k)
        {
            List<int[]> windows = new List<int[]>();
            for (int i = 0; i <= sequence.Length - k; i++)
            {
                int[] coords = new int[2];
                coords[0] = i;
                coords[1] = i + k;
                windows.Add(coords);
            }
            return windows;
        }
    }
}

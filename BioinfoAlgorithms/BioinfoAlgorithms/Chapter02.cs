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
            List<string> bestMotifs;
            int k;
            int d;
            int iterations;
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
                    bestMotifs = GreedyMotifSearch(dnaStrings,k,dnaStrings.Count);
                    PrintMotifs(bestMotifs, "Best motifs:");
                    Console.ReadLine();
                    break;
                case "2E":
                    dnaStrings = new List<string> {"TTACCTTAAC",
                                                   "GATGTCTGTC",
                                                   "ACGGCGTTAG",
                                                   "CCCTAACGAG",
                                                   "CGTCAGAGGT"};
                    k = 4;
                    bestMotifs = GreedyMotifSearchWithPseudoCounts(dnaStrings,k,dnaStrings.Count);
                    PrintMotifs(bestMotifs, "Best motifs:");
                    pm = MotifsToProfileMatrix(bestMotifs);
                    PrintPm(pm);
                    Console.ReadLine();
                    break;
                case "2F":
                    dnaStrings = new List<string> {"TTACCTTAAC",
                                                   "GATGTCTGTC",
                                                   "CCGGCGTTAG",
                                                   "CACTAACGAG",
                                                   "CGTCAGAGGT"};
                    k = 4;
                    bestMotifs = RandomizedMotifSearch(dnaStrings,k,dnaStrings.Count);
                    PrintMotifs(bestMotifs, "Best motifs:");
                    pm = MotifsToProfileMatrix(bestMotifs);
                    PrintPm(pm);
                    Console.ReadLine();
                    break; 
                case "2F_iterative":
                    dnaStrings = new List<string> {"TTACCTTAAC",
                                                   "GATGTCTGTC",
                                                   "CCGGCGTTAG",
                                                   "CACTAACGAG",
                                                   "CGTCAGAGGT"};
                    k = 4;
                    iterations = 100;
                    bestMotifs = IterationsOfRandomizedMotifSearch(dnaStrings,k,dnaStrings.Count, iterations);
                    PrintMotifs(bestMotifs, "Best motifs:");
                    pm = MotifsToProfileMatrix(bestMotifs);
                    PrintPm(pm);
                    Console.ReadLine();
                    break;
                case "2G":
                    dnaStrings = new List<string> {"TTACCTTAAC",
                                                   "GATGTCTGTC",
                                                   "CCGGCGTTAG",
                                                   "CACTAACGAG",
                                                   "CGTCAGAGGT"};
                    
                    k = 4;
                    int n_iter = 1000;
                    bestMotifs = GibbsSampler(dnaStrings, k, dnaStrings.Count, n_iter);
                    PrintMotifs(bestMotifs, "Best GibbsSampler motif:");
                    Console.ReadLine();
                    break;
                case "2G_iterative":
                    dnaStrings = new List<string> {"TTACCTTAAC",
                                                   "GATGTCTGTC",
                                                   "CCGGCGTTAG",
                                                   "CACTAACGAG",
                                                   "CGTCAGAGGT"};
                    k = 4;
                    iterations = 100;
                    bestMotifs = IterationsOfGibbsSampler(dnaStrings, k, dnaStrings.Count, iterations);
                    PrintMotifs(bestMotifs, "Best motifs:");
                    pm = MotifsToProfileMatrix(bestMotifs);
                    PrintPm(pm);
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
        /// Ancilary funtionc that helps to run GibsSampler() an 'iteration' number of times.
        /// </summary>
        public List<string> IterationsOfGibbsSampler(List<string> dnaStrings, int k, int t, int iterations)
        {
            List<string> bestMotifs = new List<string>();
            double bestMotifScore = 1000000.0;

            for (int i = 0; i < iterations; i++)
            {
                List<string> motif = GibbsSampler(dnaStrings, k, t, iterations);
                double motifScore = ScoreProfileMatrix(MotifsToProfileMatrix(motif));

                if (motifScore < bestMotifScore)
                {
                    bestMotifs = motif;
                    bestMotifScore = motifScore;
                }

            }

            return bestMotifs;
        }

        /// <summary>
        /// GibbsSampler, the same as RandomizedMotifSearch(), at first creates a bogus motifs matrix
        /// from the first kmers in each dna strings from dnaStrings. But what is different in GibbsSampler
        /// is the motifs matrix updating step. Instead of creating an updated matrix that consists of the list
        /// of all entirely new motifs (the ones that are most likely given the motifs matrix via MostProbableKmer()), the 
        /// Gibbs Sampler almost randomly removes one kmer from motifs matrix, and then replace that kmer with 
        /// an almost randomly chosen kmer from the respective dna strings, where randomness of choice is biased
        /// by the motifs matrix implemented via ProfileRandomlyGeneratedKmer().
        /// </summary>
        public List<string> GibbsSampler(List<string> dnaStrings, int k, int t, int n )
        {
            // first time around select motifs from each dnaString at random
            List<string> motifs = new List<string>();
            int i;
            foreach (string dna in dnaStrings)
            {
                i = Program.Rnd.Next(0, dna.Length - k + 1);
                motifs.Add(dna.Substring(i, k));
            }

            List<string> bestMotifs = motifs;

            for (int j = 0; j < n; j++)
            {
                i = Program.Rnd.Next(0,t); // index for temporary removal of a corresponding motif

                List<string> motifsMinusOne = new List<string>();
                for(int m = 0; m < t; m++)
                    motifsMinusOne.Add(m != i ? bestMotifs[m] : "");

                motifs = motifsMinusOne;
                string motif = ProfileRandomlyGeneratedKmer(motifsMinusOne, dnaStrings[i]);
                motifs[i] = motif;

                if (ScoreProfileMatrix(MotifsToProfileMatrix(motifs)) <
                    ScoreProfileMatrix(MotifsToProfileMatrix(bestMotifs)))
                    bestMotifs = motifs;
            }

            return bestMotifs;
        }

        /// <summary>
        /// Given a motifs matrix 'motifsMinusOne' and the dna string 'dna' choose at random a kmer from dna
        /// where randomness of choice is biased be the motifs matrix.
        /// </summary>
        public string ProfileRandomlyGeneratedKmer(List<string> motifsMinusOne, string dna)
        {
            List<string> motifs = new List<string>();
            foreach(string motifCurrnt in motifsMinusOne)
                if(motifCurrnt != "")
                    motifs.Add(motifCurrnt);
            
            List<ProfileMatrixEntry> pm = MotifsToProfileMatrixWithPseudoCounts(motifs);

            List<int[]> windows = StringSlidingWindows(dna, motifs.First().Length);

            List<string> kmers = new List<string>(); 
            List<double> probs = new List<double>(); 
            foreach (int[] window in windows)
            {
                string kmer = dna.Substring(window[0], motifs.First().Length);
                double currentProb = KmerProbabilityFromPm(pm, kmer);
                kmers.Add(kmer);
                probs.Add(currentProb);
            }

            double rnd = GetRandomNumber(0.0, probs.Sum());

            double current = 0.0;
            for (int i = 0; i < kmers.Count; i++)
            {
                current += probs[i];
                if (rnd <= current)
                {
                    return kmers[i];
                }
            }
            return null;
        }
        
        /// <summary>
        /// A function that returns a randomly generate double number in the region between
        /// 'minimum' and 'maximum' numbers.
        /// </summary>
        public double GetRandomNumber(double minimum, double maximum)
        {
            return Program.Rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Ancilary funtionc that helps to run RandomizedMotifSearch() an 'iteration' number of times.
        /// </summary>
        public List<string> IterationsOfRandomizedMotifSearch(List<string> dnaStrings, int k, int t, int iterations)
        {
            List<string> bestMotifs = new List<string>();
            double bestMotifScore = 1000000.0;

            for (int i = 0; i < iterations; i++)
            {
                List<string> motif = RandomizedMotifSearch(dnaStrings, k, t);
                double motifScore = ScoreProfileMatrix(MotifsToProfileMatrix(motif));

                if (motifScore < bestMotifScore)
                {
                    bestMotifs = motif;
                    bestMotifScore = motifScore;
                }

            }

            return bestMotifs;
        }

        /// <summary>
        /// At first we randomly select kmers from each dna string in dnaStrings to create the original
        /// purely random motifs matrix. Next we use the motifs matrix to create a new motifs matrix by
        /// selecting most probable kmers from each dna string in the dnaSstrings list. Each time we create
        /// a new motifs matrix, we calculate its profile matrix and the associate score. Once the algorythm
        /// doesn't any more improve the score, we return the motifs matrix.
        /// </summary>
        public List<string> RandomizedMotifSearch(List<string> dnaStrings, int k, int t)
        {
            // first time around select motifs from each dnaString at random
            List<string> motifs = new List<string>();
            foreach (string dna in dnaStrings)
            {
                int i = Program.Rnd.Next(dna.Length - k + 1);
                // Console.WriteLine("First random index: " + i.ToString());
                string motif = dna.Substring(i, k);
                motifs.Add(motif);
            }
            PrintMotifs(motifs, "First motif:");
            Console.WriteLine("First PM:");
            PrintPm(MotifsToProfileMatrix(motifs));

            List<string> bestMotifs = motifs;

            while (true)
            {
                motifs = MotifsFromPmAndDnaStrings(MotifsToProfileMatrix(motifs), dnaStrings);

                if (ScoreProfileMatrix(MotifsToProfileMatrix(motifs)) <
                    ScoreProfileMatrix(MotifsToProfileMatrix(bestMotifs)))
                {
                    bestMotifs = motifs;
                }
                else
                {
                    return bestMotifs;
                }
            }
        }
        
        /// <summary>
        /// Given a profile matrix (pm) and a list of dna sequences (dnaStrings) return motifs matrix
        /// where each motif is the most probable kmer given the pm and each of dna strings.
        /// </summary>
        public List<string> MotifsFromPmAndDnaStrings(List<ProfileMatrixEntry> pm, List<string> dnaStrings)
        {
            List<string> motifs = new List<string>();
            foreach (string dna in dnaStrings)
            {
                string motif = MostProbableKmer(dna, pm.Count/Program.AlpabetLength, pm);
                motifs.Add(motif);
            }
            return motifs;
        }       

        /// <summary>
        /// The same algorythm as GreedyMotifSearch, but when pseudocounts are added when generating
        /// profile matrixe (wth MotifsToProfileMatrixWithPsudoCounts()).
        /// Greey motif algorythms tries to find the most probable motifs by starting buidling profile matrix from 
        /// any of the k-mers in the first dnaString from dnaStrings. The pseudocount trick is known
        /// as Laplace's Rule of Succession.
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
            PrintMotifs(bestMotifs, "First profile:");

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
                PrintMotifs(motifs);

                if(ScoreProfileMatrix(MotifsToProfileMatrixWithPseudoCounts(motifs)) < 
                                ScoreProfileMatrix(MotifsToProfileMatrixWithPseudoCounts(bestMotifs)))
                {
                    bestMotifs = motifs;
                }
            }
            return bestMotifs;
        }

        /// <summary>
        /// Tries to find most probable motifs by starting building profile matrix from any of the k-mers 
        /// in the first dnaString from dnaStrings.
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
            PrintMotifs(bestMotifs, "First profile:");

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
                PrintMotifs(motifs);

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
        public void PrintMotifs(List<string> profile, string message)
        {
            Console.WriteLine(message);
            foreach (string text in profile)
                Console.WriteLine(text);
            Console.WriteLine("Weight: " + ScoreProfileMatrix(MotifsToProfileMatrix(profile)).ToString());
            Console.WriteLine();
        }
        /// <summary>
        /// Print strings in profile to console 
        /// </summary>
        public void PrintMotifs(List<string> profile)
        {
            foreach (string text in profile)
                Console.WriteLine(text);
            Console.WriteLine("Weight: " + ScoreProfileMatrix(MotifsToProfileMatrix(profile)).ToString());
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
                int totalNt = 0;
                foreach (string nt in Alphabet) {
                    // ntPosition[nt] = 0;
                    ntPosition[nt] = 1;  // initialzing nt counts at 1 which serves as a pseudocount
                    totalNt += 1;
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
                    totalNt += 1;
                }

                foreach (string nt in Alphabet)
                {
                    double prob = ntPosition[nt]/(double)totalNt; // x2 since each nt is initated at 1
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
            // Console.ReadLine();
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

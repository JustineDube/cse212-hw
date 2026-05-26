using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        // Plan:
        // 1. Add every word into a HashSet for O(1) lookup.
        // 2. Iterate through each word once (O(n)).
        // 3. For each word, build its reverse.
        // 4. Skip words where both characters are the same (e.g. "aa") — they can't have a symmetric pair.
        // 5. Check if the reverse exists in the set.
        // 6. To avoid adding duplicate pairs (am&ma and ma&am), only add the pair when
        //    the current word is lexicographically less than its reverse.
        // 7. Return the collected pairs as an array.

        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // Build the reverse of the 2-character word
            var reverse = $"{word[1]}{word[0]}";

            // Skip same-character words like "aa" — no symmetric pair possible
            if (word[0] == word[1])
                continue;

            // Only add once: use lexicographic order to avoid duplicates
            if (wordSet.Contains(reverse) && string.Compare(word, reverse) < 0)
            {
                pairs.Add($"{reverse} & {word}");
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            // Plan:
            // 1. The degree is in column index 3 (4th column, 0-based).
            // 2. Trim any whitespace from the degree string.
            // 3. If the degree already exists in the dictionary, increment its count.
            // 4. If it doesn't exist, add it with a count of 1.

            if (fields.Length > 3)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // Plan:
        // 1. Normalize both words: convert to lowercase and remove all spaces.
        // 2. Build a frequency dictionary for word1: for each character, count occurrences.
        // 3. For each character in word2, decrement its count in the dictionary.
        //    If a character from word2 isn't in the dictionary (count 0 or missing), return false.
        // 4. After processing word2, check that all counts in the dictionary are exactly 0.
        //    If any count is non-zero, the words are not anagrams.
        // 5. Return true if all counts are zero.

        // Step 1: Normalize
        var w1 = word1.ToLower().Replace(" ", "");
        var w2 = word2.ToLower().Replace(" ", "");

        // Step 2: Build frequency map for word1
        var freq = new Dictionary<char, int>();
        foreach (var c in w1)
        {
            if (freq.ContainsKey(c))
                freq[c]++;
            else
                freq[c] = 1;
        }

        // Step 3: Decrement counts using word2
        foreach (var c in w2)
        {
            if (!freq.ContainsKey(c) || freq[c] == 0)
                return false;
            freq[c]--;
        }

        // Step 4: All counts must be zero
        foreach (var count in freq.Values)
        {
            if (count != 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // Plan:
        // - featureCollection.Features is a list of Feature objects.
        // - Each Feature has a Properties object with Place (string) and Mag (double?).
        // - Format each as "{place} - Mag {mag}" and return as array.

        return featureCollection?.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray() ?? [];
    }
}
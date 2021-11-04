using System.Collections.Generic;
using System.Linq;

namespace EnrolleeCSV
{
    /* 6. Availity receives enrollment files from various benefits management and enrollment solutions...
    For files in CSV format, write a program to:
    - read the content of the file and separate enrollees by insurance company in its own file
    - sort the contents of each file by last and first name (ascending)
    - for duplicate User Ids within an Insurance Company, keep only the record with the highest version 
    The following data points are included in the file:
    - User Id (string)
    - First and Last Name (string)
    - Version (integer)
    - Insurance Company (string)
    */
    public class EnrolleeTransform
    {
        public List<InsCoList> Transform(IEnumerable<string> strings)
        {
            var enrollments = new List<Enrollment>();
            var insCoOutput = new List<InsCoList>();

            //*** Transform should expect comma-separated values.
            foreach (string csvLine in strings)
            {
                var csvParts = csvLine.Split(',');
                enrollments.Add(new Enrollment {
                    UserId = csvParts[0],
                    FullName = csvParts[1],
                    LastNamePointer = csvParts[1].Split(' ').Length - 1,
                    Version = int.Parse(csvParts[2]),
                    InsuranceCo = csvParts[3]
                });
            }

            //*** A separate file should be written for each insurance co.
            //Dev note: Could add .OrderBy(e => e) below if sorting by co was a requirement.
            foreach (string insCo in enrollments.Select(e => e.InsuranceCo).Distinct())
            {
                var sortedUsersForCo = enrollments
                    .Where(e => e.InsuranceCo == insCo)
                    //*** Output should be sorted by Last and First Name.
                    .OrderBy(e => e.FullName.Split(' ')[e.LastNamePointer])
                    .ThenBy(e => e.FullName);
                //*** Output should omit duplicate UserIDs, keeping only records with the highest version.
                var maxVersionByUserId = sortedUsersForCo
                    .GroupBy(e => e.UserId, (key, result) => new {
                        UserId = key,
                        Version = result.Max(e => e.Version)
                    });
                var outputLines = new List<string>();
                foreach (var group in maxVersionByUserId)
                {
                    outputLines.Add(sortedUsersForCo
                                    .Where(e => e.UserId == group.UserId
                                             && e.Version == group.Version)
                                    .Select(e => $"{e.UserId},{e.FullName},{e.Version},{insCo}")
                                    .FirstOrDefault()
                                );
                }
                insCoOutput.Add(new InsCoList { FileName = insCo + ".csv", CsvLines = outputLines });
            }
            return insCoOutput;
        }

        public class Enrollment
        {
            public string UserId { get; set; }
            public string FullName { get; set; }
            public int LastNamePointer { get; set; }
            public int Version { get; set; }
            public string InsuranceCo { get; set; }
        }
    }

    public class InsCoList
    {
        public string FileName { get; set; }
        public List<string> CsvLines { get; set; }
    }
}

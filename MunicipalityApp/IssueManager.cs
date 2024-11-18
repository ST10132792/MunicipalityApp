using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{
    public class IssueManager
    {
        // List to hold all reported issues
        public List<ReportedIssue> ReportedIssues { get; private set; }

        public IssueManager()
        {
            ReportedIssues = new List<ReportedIssue>();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Method to add a new issue to the list
        public void AddIssue(ReportedIssue issue)
        {
            ReportedIssues.Add(issue);
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class ReportedIssue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaFilePath { get; set; }
    }

}

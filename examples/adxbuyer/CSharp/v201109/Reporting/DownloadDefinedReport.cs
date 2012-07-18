// Copyright 2011, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets and downloads a report from a report definition.
  /// </summary>
  class DownloadDefinedReport : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      DownloadDefinedReport codeExample = new DownloadDefinedReport();
      Console.WriteLine(codeExample.Description);
      try {
        long reportDefinitionId = long.Parse("INSERT_REPORT_DEFINITION_ID_HERE");
        string fileName = "INSERT_FILE_NAME_HERE";
        codeExample.Run(new AdWordsUser(), reportDefinitionId, fileName);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets and downloads a report from an existing report definition.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="reportDefinitionId">Id of the report to be downloaded.
    /// </param>
    /// <param name="fileName">File to which report is downloaded.</param>
    public void Run(AdWordsUser user, long reportDefinitionId, string fileName) {
      string filePath = ExampleUtilities.GetHomeDir() + Path.DirectorySeparatorChar + fileName;

      try {
        // If you know that your report is small enough to fit in memory, then
        // you can instead use
        // ClientReport report = new ReportUtilities(user).GetClientReport(reportDefinitionId);
        //
        // // Get the text report directly if you requested a text format
        // // (e.g. xml)
        // string reportText = report.Text;
        //
        // // Get the binary report if you requested a binary format
        // // (e.g. gzip)
        // byte[] reportBytes = report.Contents;
        //
        // // Deflate a zipped binary report for further processing.
        // string deflatedReportText = Encoding.UTF8.GetString(
        //     MediaUtilities.DeflateGZipData(report.Contents));
        new ReportUtilities(user).DownloadClientReport(reportDefinitionId, filePath);
        Console.WriteLine("Report with definition id '{0}' was downloaded to '{1}'.",
            reportDefinitionId, filePath);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to download report.", ex);
      }
    }
  }
}

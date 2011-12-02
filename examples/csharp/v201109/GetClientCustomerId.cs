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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example illustrates how to find a client customer ID for a
  /// client email. We recommend to use this script as a one off to convert your
  /// identifiers to IDs and store them for future use.
  ///
  /// Tags: InfoService.get
  /// </summary>
  class GetClientCustomerId : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to find a client customer ID for a client " +
            "email. We recommend to use this script as a one off to convert your identifiers " +
            "to IDs and store them for future use.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetClientCustomerId();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the InfoService.
      InfoService infoService = (InfoService) user.GetService(AdWordsService.v201109.InfoService);

      // Ensure the clientCustomerId is not set, so that requests are made to
      // the MCC.
      infoService.RequestHeader.clientCustomerId = null;

      string clientEmail = _T("INSERT_EMAIL_ADDRESS_HERE");

      // Create selector.
      InfoSelector selector = new InfoSelector();
      selector.clientEmails = new string[] {clientEmail};
      selector.includeSubAccounts = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;

      // The date used doesn't matter, so use today.
      DateRange dateRange = new DateRange();
      dateRange.max = DateTime.Now.ToString("yyyyMMdd");
      dateRange.min = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
      selector.dateRange = dateRange;

      try {
        // Get the information for the client email address.
        ApiUsageInfo info = infoService.get(selector);

        if (info != null && info.apiUsageRecords != null) {
          foreach (ApiUsageRecord record in info.apiUsageRecords) {
            Console.WriteLine("Found record with client email '{0}' and customer ID " +
                "'{1:###-###-####}'.", record.clientEmail, record.clientCustomerId);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get client customer id. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

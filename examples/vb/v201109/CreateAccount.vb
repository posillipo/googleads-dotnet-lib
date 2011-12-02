' Copyright 2011, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201109

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example illustrates how to create an account. Note by default,
  ''' this account will only be accessible via parent MCC.
  '''
  ''' Tags: GeoLocationService.get, AdExtensionOverrideService.mutate
  ''' </summary>
  Class CreateAccount
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create an account. Note by default," & _
            " this account will only be accessible via parent MCC."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New CreateAccount
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CreateAccountService.
      Dim createAccountService As CreateAccountService = user.GetService( _
          AdWordsService.v201109.CreateAccountService)

      Dim account As New Account()
      account.currencyCode = "EUR"
      account.dateTimeZone = "Europe/London"

      ' Prepare operation to create an account.
      Dim operation As New CreateAccountOperation()
      operation.operator = [Operator].ADD
      operation.operand = account
      operation.descriptiveName = "Account created with CreateAccountService"

      Try
        ' Create the account. It is possible to create multiple accounts with
        ' one request by sending an array of operations.
        Dim accounts As Account() = createAccountService.mutate( _
            New CreateAccountOperation() {operation})
        If (Not accounts Is Nothing AndAlso accounts.Length > 0) Then
          Dim newAccount As Account = accounts(0)
          Console.WriteLine("Account with customer ID '{0:###-###-####}' was successfully " & _
              "created.", newAccount.customerId)
        Else
          Console.WriteLine("No accounts were created.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create accounts. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

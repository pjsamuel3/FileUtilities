Imports System.IO
Imports System.Text.RegularExpressions

Module FileUtilities

    Sub Main(ByVal args As String())

        Dim startTime As DateTime = DateTime.Now

        If (FileExists(args)) Then
            For Each argument As String In args
                Console.WriteLine(argument)
                ReplaceInFile(argument, ",", ";")
            Next
        End If

        DisplayElapsedTime(startTime)

        Console.ReadLine()

    End Sub

    Private Function FileExists(ByVal arguments As String()) As Boolean
        Dim canContinue As Boolean = True

        If arguments.Count < 1 Then
            Console.WriteLine("Please specify at least one file path")
            canContinue = False
        End If

        For Each path In arguments
            Dim fi As New FileInfo(path)

            If Not fi.Exists Then
                Console.WriteLine("{0} does not exist!", path)
                canContinue = False
            End If
        Next

        Return canContinue

    End Function

    Sub ReplaceInFile(ByVal filePath As String, ByVal searchText As String, ByVal replaceText As String)

        Dim content As String = GetFileContents(filePath)

        CountReplacements(searchText, content)

        content = Regex.Replace(content, searchText, replaceText)

        WriteTofile(filePath, content)

    End Sub

    Private Function GetFileContents(ByVal filePath As String) As String
        Dim reader As New StreamReader(filePath)
        Dim content = reader.ReadToEnd()
        reader.Close()
        Return content
    End Function

    Private Sub WriteTofile(ByVal filePath As String, ByVal content As String)

        Dim writer As New StreamWriter(filePath)
        writer.Write(content)
        writer.Close()

    End Sub

    Private Sub CountReplacements(ByVal searchText As String, ByVal content As String)
        Dim counter As Int16 = 0

        For Each found As Char In content

            Dim charArr = searchText.ToCharArray()
            Dim i As Int16 = charArr.Count()

            If i = 1 Then
                If found = charArr Then
                    counter = counter + 1
                End If
            End If
        Next

        Console.WriteLine("Replacing {0} instances of {1}", counter, searchText)
    End Sub

    Private Sub DisplayElapsedTime(ByVal startTime As Date)
        Dim elapsed As TimeSpan = DateTime.Now - startTime
        Console.WriteLine("Operation took: {0} seconds", elapsed.TotalSeconds)
    End Sub

End Module

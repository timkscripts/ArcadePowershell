Imports System.Collections.ObjectModel
Imports System.Management.Automation
Imports System.Management.Automation.Runspaces
Imports System.Text
Imports System.IO
Imports System.Windows.Threading
Imports System.Timers
Imports System.Media

Class MainWindow


    Dim randomValue1 As Object = 0
    Dim randomValue2 As Object = 20
    Dim randomValue3 As Object = 30
    Dim randomValue4 As Object = 150
    Dim randomValue5 As Object = 180
    Dim randomValue6 As Object = 190
    Dim upperbound As Object = 1.0
    Dim lowerbound As Object = 0.0
    Dim upperbound2 As Object = 255
    Dim lowerbound2 As Object = 0
    Dim goingUp As Boolean = True
    Dim goingUp2 As Boolean = True
    Dim goingUp3 As Boolean = True
    Dim goingUp4 As Boolean = True
    Dim goingUp5 As Boolean = True


    Dim rotateTransform0 As New RotateTransform(18 * 5)
    Dim rotateTransform1 As New RotateTransform(18 * 4)
    Dim rotateTransform2 As New RotateTransform(18 * 3)
    Dim rotateTransform3 As New RotateTransform(18 * 2)
    Dim rotateTransform4 As New RotateTransform(18 * 1)
    Dim rotateTransform5 As New RotateTransform(18 * 0)
    Dim rotateTransform6 As New RotateTransform(18 * -1)
    Dim rotateTransform7 As New RotateTransform(18 * -2)
    Dim rotateTransform8 As New RotateTransform(18 * -3)
    Dim rotateTransform9 As New RotateTransform(18 * -4)
    Dim rotateTransform10 As New RotateTransform(18 * -5)
    Dim goingUp6 As Boolean = True

    Dim whichPulse As Integer = 0
    Dim wheelCtr As Integer = 0
    Dim wheelStr(8000) As String
    Dim wheelPos As Integer = 0


    Dim romLocation As String = "F:\arcade\snes\Roms\Roms\"
    Dim roms As String = "F:\arcade\snes\Roms\Roms"
    Dim ext As String = "smc"
    Dim curLocation As String
    Dim proName As String = "snes9x"
    Dim exeLocation As String = "F:\arcade\snes\Emu\snes9x.exe"
    Dim vidLocation As String = "F:\arcade\snes\Roms\vid\"



    Private Sub Grid_Initialized(sender As Object, e As EventArgs)
        sender.Width = System.Windows.SystemParameters.PrimaryScreenWidth
        sender.Height = System.Windows.SystemParameters.PrimaryScreenHeight
        selectedText.Margin = New Thickness(520, 282, 0, 0)
        selectedText2.Margin = New Thickness(520, 282, 0, 0)
        selectedText3.Margin = New Thickness(520, 282, 0, 0)
        selectedText4.Margin = New Thickness(520, 282, 0, 0)
        selectedText5.Margin = New Thickness(520, 282, 0, 0)
        selectedText6.Margin = New Thickness(520, 282, 0, 0)
        selectedText7.Margin = New Thickness(520, 282, 0, 0)
        selectedText8.Margin = New Thickness(520, 282, 0, 0)
        selectedText9.Margin = New Thickness(520, 282, 0, 0)
        selectedText10.Margin = New Thickness(520, 282, 0, 0)

        selectedText.RenderTransform = rotateTransform0
        selectedText1.RenderTransform = rotateTransform1
        selectedText2.RenderTransform = rotateTransform2
        selectedText3.RenderTransform = rotateTransform3
        selectedText4.RenderTransform = rotateTransform4
        selectedText5.RenderTransform = rotateTransform5
        selectedText6.RenderTransform = rotateTransform6
        selectedText7.RenderTransform = rotateTransform7
        selectedText8.RenderTransform = rotateTransform8
        selectedText9.RenderTransform = rotateTransform9
        selectedText10.RenderTransform = rotateTransform10
        err.Focus()
        NewTimer()

        selectedText.FontFamily = New FontFamily("Impact")
        selectedText1.FontFamily = New FontFamily("Impact")
        selectedText2.FontFamily = New FontFamily("Impact")
        selectedText3.FontFamily = New FontFamily("Impact")
        selectedText4.FontFamily = New FontFamily("Impact")
        selectedText5.FontFamily = New FontFamily("Impact")
        selectedText6.FontFamily = New FontFamily("Impact")
        selectedText7.FontFamily = New FontFamily("Impact")
        selectedText8.FontFamily = New FontFamily("Impact")
        selectedText9.FontFamily = New FontFamily("Impact")
        selectedText10.FontFamily = New FontFamily("Impact")
        selectedText.TextAlignment = HorizontalAlignment.Right
        selectedText1.TextAlignment = HorizontalAlignment.Right
        selectedText2.TextAlignment = HorizontalAlignment.Right
        selectedText3.TextAlignment = HorizontalAlignment.Right
        selectedText4.TextAlignment = HorizontalAlignment.Right
        selectedText5.TextAlignment = HorizontalAlignment.Right
        selectedText6.TextAlignment = HorizontalAlignment.Right
        selectedText7.TextAlignment = HorizontalAlignment.Right
        selectedText8.TextAlignment = HorizontalAlignment.Right
        selectedText9.TextAlignment = HorizontalAlignment.Right
        selectedText10.TextAlignment = HorizontalAlignment.Right


        selectedText10.Width = 900
        selectedText9.Width = 900
        selectedText8.Width = 900
        selectedText7.Width = 900
        selectedText6.Width = 900
        selectedText5.Width = 900
        selectedText4.Width = 900
        selectedText3.Width = 900
        selectedText2.Width = 900
        selectedText1.Width = 900
        selectedText.Width = 900

        Dim pShellCode As String
        pShellCode = "$dir = "
        pShellCode += """" + roms + """"
        pShellCode += vbCrLf.ToString
        pShellCode += "Get-ChildItem $dir -Filter *." + ext + " | Foreach-Object{ $_.name} "
        pshellreturn.AppendText(RunScript(pShellCode))



        Dim box1Lines As String() = pshellreturn.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        Dim newLines As String = ""
        For Each line As String In box1Lines
            wheelStr(wheelCtr) = line
            wheelCtr += 1

        Next

        For Each line As String In wheelStr
            selectedText10.Text = wheelStr(10)
            selectedText9.Text = wheelStr(9)
            selectedText8.Text = wheelStr(8)
            selectedText7.Text = wheelStr(7)
            selectedText6.Text = wheelStr(6)
            selectedText5.Text = wheelStr(5)
            selectedText4.Text = wheelStr(4)
            selectedText3.Text = wheelStr(3)
            selectedText2.Text = wheelStr(2)
            selectedText1.Text = wheelStr(1)
            selectedText.Text = wheelStr(1)


        Next
        wheelPos = 11
    End Sub
    Private dpTimer As DispatcherTimer

    Public Sub NewTimer()
        dpTimer = New DispatcherTimer
        dpTimer.Interval = TimeSpan.FromMilliseconds(50)
        AddHandler dpTimer.Tick, AddressOf TickMe
        dpTimer.Start()
    End Sub

    Private Sub TickMe()
        Dim upvalue1 As Integer = 40
        Dim upvalue2 As Integer = 30
        Dim upvalue3 As Integer = 20
        Dim upvalue4 As Integer = 30
        Dim upvalue5 As Integer = 40
        Dim upvalue6 As Integer = 20

        If (goingUp = True) Then
            randomValue1 = randomValue1 + upvalue1
        Else
            randomValue1 = randomValue1 - upvalue1
        End If
        If (goingUp2 = True) Then
            randomValue2 = randomValue2 + upvalue2
        Else
            randomValue2 = randomValue2 - upvalue2
        End If
        If (goingUp3 = True) Then
            randomValue3 = randomValue3 + upvalue3
        Else
            randomValue3 = randomValue3 - upvalue3
        End If
        If (goingUp4 = True) Then
            randomValue4 = randomValue4 + upvalue4
        Else
            randomValue4 = randomValue4 - upvalue4
        End If
        If (goingUp5 = True) Then
            randomValue5 = randomValue5 + upvalue5
        Else
            randomValue5 = randomValue5 - upvalue5
        End If
        If (goingUp6 = True) Then
            randomValue6 = randomValue6 + upvalue6
        Else
            randomValue6 = randomValue6 - upvalue6
        End If


        If (randomValue1 >= 250) Then
            goingUp = False
            randomValue1 = 250
        End If
        If (randomValue2 >= 250) Then
            goingUp2 = False
            randomValue2 = 250
        End If
        If (randomValue3 >= 250) Then
            goingUp3 = False
            randomValue3 = 250
        End If
        If (randomValue4 >= 250) Then
            goingUp4 = False
            randomValue4 = 250
        End If
        If (randomValue5 >= 250) Then
            goingUp5 = False
            randomValue5 = 250
        End If
        If (randomValue6 >= 250) Then
            goingUp6 = False
            randomValue6 = 250
        End If

        If (randomValue1 <= 90) Then
            goingUp = True
            randomValue1 = 90
        End If
        If (randomValue2 <= 90) Then
            goingUp2 = True
            randomValue2 = 90
        End If
        If (randomValue3 <= 90) Then
            goingUp3 = True
            randomValue3 = 90
        End If
        If (randomValue4 <= 90) Then
            goingUp4 = True
            randomValue4 = 90
        End If
        If (randomValue5 <= 90) Then
            goingUp5 = True
            randomValue5 = 90
        End If
        If (randomValue6 <= 90) Then
            goingUp6 = True
            randomValue6 = 90
        End If

        Dim randomPos1 = CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound
        Dim randomPos2 = CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound
        Dim randomPos3 = CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound

        Dim myBrush As New RadialGradientBrush()
        myBrush.GradientOrigin = New Point(0, 0)
        Dim randomPos4 = CInt(Math.Floor((upperbound2 - lowerbound2 + 1) * Rnd())) + lowerbound2
        Dim randomPos5 = CInt(Math.Floor((upperbound2 - lowerbound2 + 1) * Rnd())) + lowerbound2
        Dim randomPos6 = CInt(Math.Floor((upperbound2 - lowerbound2 + 1) * Rnd())) + lowerbound2
        myBrush.GradientStops.Add(New GradientStop(Color.FromRgb(randomPos1, randomPos2, randomPos3), 1.0))
        myBrush.GradientStops.Add(New GradientStop(Color.FromRgb(randomValue1, randomValue2, randomValue3), 0.5))
        myBrush.GradientStops.Add(New GradientStop(Color.FromRgb(randomValue4, randomValue5, randomValue6), 0.0))


        If (rotateTransform0.Angle = 0) Then
            selectedText.Foreground = myBrush
            curLocation = selectedText.Text
        End If
        If (rotateTransform1.Angle = 0) Then
            selectedText1.Foreground = myBrush
            curLocation = selectedText1.Text
        End If
        If (rotateTransform2.Angle = 0) Then
            selectedText2.Foreground = myBrush
            curLocation = selectedText2.Text
        End If
        If (rotateTransform3.Angle = 0) Then
            selectedText3.Foreground = myBrush
            curLocation = selectedText3.Text
        End If
        If (rotateTransform4.Angle = 0) Then
            selectedText4.Foreground = myBrush
            curLocation = selectedText4.Text
        End If
        If (rotateTransform5.Angle = 0) Then
            selectedText5.Foreground = myBrush
            curLocation = selectedText5.Text
        End If
        If (rotateTransform6.Angle = 0) Then
            selectedText6.Foreground = myBrush
            curLocation = selectedText6.Text
        End If
        If (rotateTransform7.Angle = 0) Then
            selectedText7.Foreground = myBrush
            curLocation = selectedText7.Text
        End If
        If (rotateTransform8.Angle = 0) Then
            selectedText8.Foreground = myBrush
            curLocation = selectedText8.Text
        End If
        If (rotateTransform9.Angle = 0) Then
            selectedText9.Foreground = myBrush
            curLocation = selectedText9.Text
        End If
        If (rotateTransform10.Angle = 0) Then
            selectedText10.Foreground = myBrush
            curLocation = selectedText10.Text
        End If



    End Sub

    Private Sub MediaElement_Initialized(sender As Object, e As EventArgs)

    End Sub

    Private Sub RichTextBox_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub MediaElement_MediaEnded(sender As Object, e As RoutedEventArgs)
        mediaBackground.Position = New TimeSpan(0)
        mediaBackground.Play()


    End Sub

    Private Sub mediaBackground_Unloaded(sender As Object, e As RoutedEventArgs) Handles mediaBackground.Unloaded

        mediaBackground.Play()

    End Sub

    Private Sub mediaBackground_Loaded(sender As Object, e As RoutedEventArgs) Handles mediaBackground.Loaded

        mediaBackground.Play()
    End Sub
    ' Takes script text as input and runs it, then converts
    ' the results to a string to return to the user
    Private Function RunScript(ByVal scriptText As String) As String

        ' create Powershell runspace
        Dim MyRunSpace As Runspace = RunspaceFactory.CreateRunspace()

        ' open it
        MyRunSpace.Open()

        ' create a pipeline and feed it the script text
        Dim MyPipeline As Pipeline = MyRunSpace.CreatePipeline()

        MyPipeline.Commands.AddScript(scriptText)

        ' add an extra command to transform the script output objects into nicely formatted strings
        ' remove this line to get the actual objects that the script returns. For example, the script
        ' "Get-Process" returns a collection of System.Diagnostics.Process instances.
        MyPipeline.Commands.Add("Out-String")

        ' execute the script
        Dim results As Collection(Of PSObject) = MyPipeline.Invoke()

        ' close the runspace
        MyRunSpace.Close()

        ' convert the script result into a single string
        Dim MyStringBuilder As New StringBuilder()

        For Each obj As PSObject In results
            MyStringBuilder.AppendLine(obj.ToString())
        Next

        ' return the results of the script that has
        ' now been converted to text
        Return MyStringBuilder.ToString()

    End Function

    ' helper method that takes your script path, loads up the script
    ' into a variable, and passes the variable to the RunScript method
    ' that will then execute the contents
    Private Function LoadScript(ByVal filename As String) As String

        Try

            ' Create an instance of StreamReader to read from our file.
            ' The using statement also closes the StreamReader.
            Dim sr As New StreamReader(filename)

            ' use a string builder to get all our lines from the file
            Dim fileContents As New StringBuilder()

            ' string to hold the current line
            Dim curLine As String = ""

            ' loop through our file and read each line into our
            ' stringbuilder as we go along
            Do
                ' read each line and MAKE SURE YOU ADD BACK THE
                ' LINEFEED THAT IT THE ReadLine() METHOD STRIPS OFF
                curLine = sr.ReadLine()
                fileContents.Append(curLine + vbCrLf)
            Loop Until curLine Is Nothing

            ' close our reader now that we are done
            sr.Close()

            ' call RunScript and pass in our file contents
            ' converted to a string
            Return fileContents.ToString()

        Catch e As Exception
            ' Let the user know what went wrong.
            Dim errorText As String = "The file could not be read:"
            errorText += e.Message + "\n"
            Return errorText
        End Try

    End Function

    Private Sub Image_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Space Then
            err.AppendText("HI")
            RunScript(LoadScript("f:\arcade\fullscreen.ps1"))
            err.AppendText("HI")
            e.Handled = True


        End If


    End Sub

    Private Sub Image_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles fm.PreviewKeyDown
        If e.Key = Key.A Then
            err.AppendText("HI")
            RunScript(LoadScript("f:\arcade\fullscreen.ps1"))
            err.AppendText("HI")
            e.Handled = True

        Else
            err.AppendText("HsI")
        End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles fm.PreviewKeyDown
        If e.Key = Key.Space Then
            e.Handled = True
        End If

    End Sub

    Private Sub err_KeyDown(sender As Object, e As KeyEventArgs) Handles err.PreviewKeyDown
        Dim ObjFdoc As New FlowDocument
        err.Document = ObjFdoc
        If e.Key = Key.A Then
            err.AppendText("HI")
            Dim scriptString As String

            scriptString = "" '"import-Module ""f:\arcade\wasp.dll"""
            scriptString += vbCrLf.ToString
            scriptString += "$processName = """ + proName + """"
            scriptString += vbCrLf.ToString
            scriptString += "$location = ""Resolve-Path " + romLocation + curLocation.Replace(ext, "smc") + """"


            scriptString += vbCrLf.ToString
            scriptString += LoadScript("f:\arcade\fullscreen.ps1")
            pshellreturn.Text = scriptString
            RunScript(scriptString)
            err.AppendText("HI")
            e.Handled = True
        End If
        If e.Key = Key.G Then
            

        End If
        If e.Key = Key.B Then
            rotateTransform0.Angle = rotateTransform0.Angle - 18
            rotateTransform1.Angle = rotateTransform1.Angle - 18
            rotateTransform2.Angle = rotateTransform2.Angle - 18
            rotateTransform3.Angle = rotateTransform3.Angle - 18
            rotateTransform4.Angle = rotateTransform4.Angle - 18
            rotateTransform5.Angle = rotateTransform5.Angle - 18
            rotateTransform6.Angle = rotateTransform6.Angle - 18
            rotateTransform7.Angle = rotateTransform7.Angle - 18
            rotateTransform8.Angle = rotateTransform8.Angle - 18
            rotateTransform9.Angle = rotateTransform9.Angle - 18
            rotateTransform10.Angle = rotateTransform10.Angle - 18


            If (rotateTransform0.Angle = 0) Then

                curLocation = selectedText.Text
            End If
            If (rotateTransform1.Angle = 0) Then

                curLocation = selectedText1.Text
            End If
            If (rotateTransform2.Angle = 0) Then

                curLocation = selectedText2.Text
            End If
            If (rotateTransform3.Angle = 0) Then

                curLocation = selectedText3.Text
            End If
            If (rotateTransform4.Angle = 0) Then

                curLocation = selectedText4.Text
            End If
            If (rotateTransform5.Angle = 0) Then

                curLocation = selectedText5.Text
            End If
            If (rotateTransform6.Angle = 0) Then

                curLocation = selectedText6.Text
            End If
            If (rotateTransform7.Angle = 0) Then

                curLocation = selectedText7.Text
            End If
            If (rotateTransform8.Angle = 0) Then

                curLocation = selectedText8.Text
            End If
            If (rotateTransform9.Angle = 0) Then

                curLocation = selectedText9.Text
            End If
            If (rotateTransform10.Angle = 0) Then

                curLocation = selectedText10.Text
            End If



            If (rotateTransform0.Angle < -90) Then
                rotateTransform0.Angle = 90
                selectedText.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform1.Angle < -90) Then
                rotateTransform1.Angle = 90
                selectedText1.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform2.Angle < -90) Then
                rotateTransform2.Angle = 90
                selectedText2.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform3.Angle < -90) Then
                rotateTransform3.Angle = 90
                selectedText3.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform4.Angle < -90) Then
                rotateTransform4.Angle = 90
                selectedText4.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform5.Angle < -90) Then
                rotateTransform5.Angle = 90
                selectedText5.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform6.Angle < -90) Then
                rotateTransform6.Angle = 90
                selectedText6.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform7.Angle < -90) Then
                rotateTransform7.Angle = 90
                selectedText7.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform8.Angle < -90) Then
                rotateTransform8.Angle = 90
                selectedText8.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform9.Angle < -90) Then
                rotateTransform9.Angle = 90
                selectedText9.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If
            If (rotateTransform10.Angle < -90) Then
                rotateTransform10.Angle = 90
                selectedText10.Text = wheelStr(wheelPos)
                wheelPos = wheelPos + 1
            End If

            vidSample.Source = New Uri(vidLocation + Replace(curLocation, ext, "mp4"))
            whichPulse = whichPulse + 1
            If (whichPulse = 1) Then
                menuSound.Stop()
                menuSound.Play()
            End If
            If (whichPulse = 2) Then
                menuSound2.Stop()
                menuSound2.Play()
            End If

            If (whichPulse = 3) Then
                menuSound3.Stop()
                menuSound3.Play()
            End If
            If (whichPulse = 4) Then
                menuSound4.Stop()
                menuSound4.Play()
                whichPulse = 0
            End If

        End If
    End Sub

    Private Sub menuSound_Loaded(sender As Object, e As RoutedEventArgs) Handles menuSound.Loaded
        menuSound.BeginInit()

    End Sub

    Private Sub menuSound_Unloaded(sender As Object, e As RoutedEventArgs) Handles menuSound.Unloaded
        menuSound.Play()
    End Sub
End Class

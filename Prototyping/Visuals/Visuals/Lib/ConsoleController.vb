Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Rogue.Lib

  ''' <summary>
  ''' Manage all input and output operations for the console. 
  ''' </summary>
  Public Class ConsoleController

#Region "Properties"

#End Region

#Region "Public Methods"

    Public Sub New()
      Initialize()
    End Sub

    Public Sub Initialize()
    End Sub

    ''' <summary>
    ''' Display all the user statistics on the last line of the display
    ''' </summary>
    Public Sub DisplayPlayerStats(ByRef whatUser As PlayerInfo)
      Dim aCurrentData As New StringBuilder
      Dim aCurrentTimeString As String
      Dim aCurrentHour As Integer = 0

      Try
        If Now.Hour > 12 Then
          aCurrentHour = aCurrentHour + 12
        Else
          aCurrentHour = Now.Hour
        End If
        aCurrentTimeString = aCurrentHour.ToString & ":" & Now.Minute.ToString

        'TODO replace info on bottom with test string for now to help figure if random rooms are showing at the correct locations
        aCurrentTimeString = "11:11" 'TODO for prototype just make it 11:11
        aCurrentData.Append("0123456789012345678901234567890123456789012345678901234567890123456789012345678")

        'debug - the following creates a line number list down the side to see positioning during debugging
        'Dim sPtr As Integer = 0
        'For xCtr As Integer = 0 To 22
        '  Console.SetCursorPosition(0, xCtr)
        '  Console.ForegroundColor = ConsoleColor.Yellow
        '  Console.BackgroundColor = ConsoleColor.Black
        '  Console.Write(sPtr.ToString())
        '  Console.ResetColor()
        '  sPtr = sPtr + 1
        '  If sPtr > 9 Then
        '    sPtr = 0
        '  End If
        'Next

        'TODO uncomment this to show correct information when no longer debugging
        'aCurrentData.Append("Level:" & (whatUser.CurrentMapLevel.ToString & Space(5)).Substring(0, 5))
        'aCurrentData.Append("Hits:" & (whatUser.CurrentHitPoints.ToString & "(" & whatUser.HitPoints.ToString & ")" & Space(9)).Substring(0, 9))
        'aCurrentData.Append("Str:" & (whatUser.CurrentStrength.ToString & "(" & whatUser.Strength.ToString & ")" & Space(9)).Substring(0, 9))
        'aCurrentData.Append("Gold:" & (whatUser.Gold.ToString & Space(7)).Substring(0, 7))
        'aCurrentData.Append("Armor:" & (whatUser.Armor.ToString & Space(4)).Substring(0, 4))
        'aCurrentData.Append(whatUser.CharacterClass)
        Console.SetCursorPosition(0, 23)
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write(aCurrentData.ToString)
        Console.ResetColor()

        Console.SetCursorPosition(74, 24)
        Console.ForegroundColor = ConsoleColor.Black
        Console.BackgroundColor = ConsoleColor.DarkGray
        Console.Write(aCurrentTimeString)
        Console.ResetColor()


      Catch ex As Exception

      End Try

    End Sub

    ''' <summary>
    ''' Get the keyboard character from the player
    ''' </summary>
    ''' <returns></returns>
    Public Function GetKeyBoardInput() As Char
      Dim aReturnValue As Char
      Dim aConsoleInfo As New ConsoleKeyInfo
      'TODO this needs work, just returning any character for now
      aConsoleInfo = Console.ReadKey
      aReturnValue = aConsoleInfo.KeyChar

      Return aReturnValue
    End Function

    ''' <summary>
    ''' Put text on the console screen at the indicated location using the specified colors
    ''' </summary>
    ''' <param name="whatXlocation">The top to bottom line of the screen to write at</param>
    ''' <param name="whatYLocation">The left to right location on a line to write at</param>
    ''' <param name="whatForegroundColor">What color to make the displayed text</param>
    ''' <param name="whatBackgroundColor">What color to make the displayed text background</param>
    ''' <param name="whatText">What text to display</param>
    Public Sub WriteAt(ByVal whatXlocation As Integer, ByVal whatYLocation As Integer, ByVal whatForegroundColor As ConsoleColor, ByVal whatBackgroundColor As ConsoleColor, ByVal whatText As String)
      Console.SetCursorPosition(whatXlocation, whatYLocation)
      Console.ForegroundColor = whatForegroundColor
      Console.BackgroundColor = whatBackgroundColor
      Console.Write(whatText)
      Console.ResetColor()
    End Sub

#End Region

#Region "Private Methods"

#End Region

  End Class
End Namespace

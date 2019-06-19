' Purpose: To experiment with attempting to recreate the visual elements of Rogue (Epyx) circa mid-1980's.
Imports System
Imports Visuals.Rogue.Lib


''' <summary>
''' TODO List
''' Start level with visibility set off everywhere and create method making visibility on following character
''' Randomly place objects on level and have character able to pick up or drop them
''' Randomly place creatures on level and have character interact with them
''' save and restore game state/levels
''' Go up and down stairs to change levels
''' </summary>
Module Module1

#Region "Global Variables"
  '==========================================Prevent Resize
  'to remove console menu options to prevent resize which destroys layout
  'NOTE: snapping to edge still is a problem.
  'from https://stackoverflow.com/questions/38175206/how-to-prevent-the-console-window-being-resized-in-vb-net
  Private Const MF_BYCOMMAND As Integer = &H0
  Public Const SC_CLOSE As Integer = &HF060
  Public Const SC_MINIMIZE As Integer = &HF020
  Public Const SC_MAXIMIZE As Integer = &HF030
  Public Const SC_SIZE As Integer = &HF000

  Friend Declare Function DeleteMenu Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
  Friend Declare Function GetSystemMenu Lib "user32.dll" (hWnd As IntPtr, bRevert As Boolean) As IntPtr
  '==========================================end prevent resize

  Public currentLevel As Integer = 0
  Public currentPlayer As New PlayerInfo
  Public gameMap(EnumsAndConsts.MapHeight, EnumsAndConsts.MapWidth, EnumsAndConsts.MapLevelsMax) As String

  Private ReadOnly m_ErrorHandler As New ErrorHandler
  Private m_CurrentObject As String = "Module1"

#End Region

#Region "Local Variables"
  Dim m_consoleController As New ConsoleController
  Dim m_canContinue As Boolean = True
  Dim m_levelMap As New LevelMap()
  Dim m_localRandom As New Random()

#End Region

  Sub Main()

    ''==========================================Prevent Resize
    'Dim handle As IntPtr
    'handle = Process.GetCurrentProcess.MainWindowHandle ' Get the handle to the console window

    'Dim sysMenu As IntPtr
    'sysMenu = GetSystemMenu(handle, False) ' Get the handle to the system menu of the console window

    'If handle <> IntPtr.Zero Then
    '  'DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND) ' To prevent user from closing console window
    '  DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND) 'To prevent user from minimizing console window
    '  DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND) 'To prevent user from maximizing console window
    '  DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND) 'To prevent the use from re-sizing console window
    'End If
    ''==========================================end prevent resize
    'Try
    '  Console.SetBufferSize(80, 25) ' or whatever size you're using...
    'Catch ex As exception
    'End Try
    'Try
    '  Console.SetWindowSize(80, 25)
    'Catch ex As exception
    'End Try
    'Try
    '  Console.BufferWidth = 80
    'Catch ex As Exception
    'End Try
    'Try
    '  Console.BufferHeight = 25
    'Catch ex As Exception
    'End Try

    Dim OrgBufferHeight%, OrgBufferWidth%, OrgWindowHeight%, OrgWindowWidth%

    OrgBufferHeight = Console.BufferHeight
    OrgBufferWidth = Console.BufferWidth
    OrgWindowHeight = Console.WindowHeight
    OrgWindowWidth = Console.WindowWidth

    Console.OutputEncoding = System.Text.Encoding.UTF8
    ConsoleEx.Resize(80, 26)
    ConsoleEx.DisableMinimize()
    ConsoleEx.DisableMaximize()
    ConsoleEx.DisableResize()
    ConsoleEx.DisableQuickEditMode()
    Console.CursorVisible = False

    ConsoleEx.SetColor(ConsoleColor.DarkYellow, 170, 85, 0)

    Try

      ' Challenge #1: The colors in Windows 10 console applications doesn't match exactly with those in MS-DOS.
      '               Specifically, the color orange is actually orange in Windows 10 where the color orange is 
      '               more of a brown color in MS-DOS.  This color is used for the walls in the original Rogue.

      ' Challenge #2: .NET is unicode based; where as original Rogue used ASCII and (possibly) ANSI.
      '               This means that drawing of the walls and other special characters needs to be handled
      '               using the unicode table and there isn't (AFAIK) a 1:1 mapping between ASCII and unicode.


      'Notes:
      '1 - I'm not much of a color person so I just went with DarkYellow for the brown
      '2 - Could not get the unicode to work out but did find the some of the ASCII characters


      InitializeGame("", "")

      While m_canContinue = True
        m_canContinue = ProcessGameLoop()
      End While
      End

    Finally

      ConsoleEx.EnableMinimize()
      ConsoleEx.EnableMaximize()
      ConsoleEx.EnableResize()
      Console.ForegroundColor = ConsoleColor.Gray
      Console.BackgroundColor = ConsoleColor.Black

      Console.SetBufferSize(OrgBufferWidth, OrgBufferHeight)
      Console.SetWindowSize(OrgWindowWidth, OrgWindowHeight)

      ConsoleEx.EnableQuickEditMode()

    End Try

    Console.ResetColor()
    Console.Clear()

  End Sub

  Private Function GetRandomStairLocation() As String
    Dim aReturnValue As String = ""
    Dim xLoc As Integer = 0
    Dim yLoc As Integer = 0
    Dim m_randomNumber As Integer = m_localRandom.Next(0, Now.Second)

    xLoc = 4 + m_localRandom.Next(0, EnumsAndConsts.MapWidth - 6)
    yLoc = 4 + m_localRandom.Next(0, EnumsAndConsts.MapHeight - 6)

    'need to ensure that location is not near edge of a grid cell
    For xPtr As Integer = 1 To EnumsAndConsts.MapLevelGridColumnMax
      If xLoc <= ((xPtr - 1) * EnumsAndConsts.MapGridCellWidth) + 2 Then
        xLoc = ((xPtr - 1) * EnumsAndConsts.MapGridCellWidth) + 2 'too close to left edge
        Exit For
      End If
      If xLoc < ((xPtr) * EnumsAndConsts.MapGridCellWidth) AndAlso xLoc >= ((xPtr) * EnumsAndConsts.MapGridCellWidth) - 2 Then
        xLoc = ((xPtr) * EnumsAndConsts.MapGridCellWidth) - 2 'too close to right edge
        Exit For
      End If
    Next

    For yPtr As Integer = 1 To EnumsAndConsts.MapLevelGridRowMax
      If yLoc <= ((yPtr - 1) * EnumsAndConsts.MapGridCellHeight) + 2 Then
        yLoc = ((yPtr - 1) * EnumsAndConsts.MapGridCellHeight) + 2 'too close to top edge
        Exit For
      End If
      If yLoc < ((yPtr) * EnumsAndConsts.MapGridCellHeight) AndAlso yLoc >= ((yPtr) * EnumsAndConsts.MapGridCellHeight) - 2 Then
        yLoc = ((yPtr) * EnumsAndConsts.MapGridCellHeight) - 2 'too close to bottom edge
        Exit For
      End If
    Next


    If yLoc > 9 Then
      aReturnValue = yLoc.ToString
    Else
      aReturnValue = "0" & yLoc.ToString
    End If
    If xLoc > 9 Then
      aReturnValue = aReturnValue & xLoc.ToString
    Else
      aReturnValue = aReturnValue & "0" & xLoc.ToString
    End If

    Return aReturnValue
  End Function

  Private Sub InitializeGame()

    Dim m_levelMap As New LevelMap
    'm_levelMap.EntryStairGrid = "1831" 'TODO testing only
    'm_levelMap.ExitStairGrid = "0470" 'TODO testing only
    m_levelMap.Initialize(True)
    m_levelMap.DrawScreen()

    NearestConsoleColor.Main()
    Console.ReadLine()
  End Sub

  Private Sub InitializeGame(ByVal whatEntryStairLocation As String, ByVal whatExitStairLocation As String)
    Dim aEntryString As String = "" & whatEntryStairLocation
    Dim aExitString As String = "" & whatExitStairLocation
    Dim xLoc As Integer = 0
    Dim yLoc As Integer = 0
    Dim m_randomNumber As Integer = m_localRandom.Next(0, 100)
    Dim isGoodStair As Boolean = False
    Dim aTryCounter As Integer = 0

    If aEntryString = "" Then
      aEntryString = GetRandomStairLocation()
    End If
    m_levelMap.EntryStairLocation = aEntryString
    m_levelMap.ExitStairLocation = aExitString
    If m_levelMap.EntryStairGrid = m_levelMap.ExitStairGrid Then
      aExitString = ""
    End If
    If aExitString = "" Then
      'Need to ensure that exitstair not in same map grid as entrystair
      Do While isGoodStair = False And aTryCounter < 100
        aExitString = GetRandomStairLocation()
        m_levelMap.ExitStairLocation = aExitString
        aTryCounter = aTryCounter + 1
        If Not m_levelMap.EntryStairGrid = m_levelMap.ExitStairGrid Then
          isGoodStair = True
        End If
      Loop
      'TODO need to handle if comes out of above loop and did not get a good random exitstairlocation

    End If
    m_levelMap = New LevelMap(aEntryString, aExitString)
    'm_levelMap.EntryStairGrid = "1831" 'TODO testing only
    'm_levelMap.ExitStairGrid = "0470" 'TODO testing only
    'm_levelMap.Initialize(True)
    Integer.TryParse(aEntryString.Substring(0, 2), yLoc)
    Integer.TryParse(aEntryString.Substring(2, 2), xLoc)

    currentPlayer = New PlayerInfo()
    currentPlayer.CurrentMapLevel = m_levelMap.CurrentMapLevel
    currentPlayer.CurrentMapX = xLoc
    currentPlayer.CurrentMapY = yLoc
    m_levelMap.MoveCharacter(currentPlayer, -1, -1, xLoc, yLoc)
    m_levelMap.DrawScreen()

  End Sub

  Private Function ProcessGameLoop() As Boolean
    Dim currentMethod As String = "ProcessGameLoop"
    Dim currentData As String = ""
    Dim aReturnValue As Boolean = True
    Dim aChar As New Char
    Dim aNewExitStairLocation As String = GetRandomStairLocation()
    Dim isGood As Boolean = False

    Try
      aChar = m_consoleController.GetKeyBoardInput()
      Select Case aChar
        Case "h" 'left
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX - 1, currentPlayer.CurrentMapY)
        Case "j" 'down
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY + 1)
        Case "l" 'right
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY)
        Case "k" 'up
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY - 1)
        Case "y" 'up and left
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX - 1, currentPlayer.CurrentMapY - 1)
        Case "b" 'down and left
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX - 1, currentPlayer.CurrentMapY + 1)
        Case "u" ' up and right
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY - 1)
        Case "n" 'down and right
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY + 1)
        Case "4" 'left
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX - 1, currentPlayer.CurrentMapY)
        Case "2" 'down
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY + 1)
        Case "6" 'right
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY)
        Case "8" 'up
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY - 1)
        Case "7" 'up and left
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX - 1, currentPlayer.CurrentMapY - 1)
        Case "1" 'down and left
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX - 1, currentPlayer.CurrentMapY + 1)
        Case "9" ' up and right
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY - 1)
        Case "3" 'down and right
          currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY + 1)
        Case "<" 'up stairs
          If currentPlayer.CurrentMapLevel > 1 Then
            isGood = m_levelMap.IsEntryCell(currentPlayer.CurrentMapX, currentPlayer.CurrentMapY)
            If isGood = True Then
              'create a random level above this one unless it is being remembered

              currentData = m_levelMap.MoveCharacter(currentPlayer, currentPlayer.CurrentMapX, currentPlayer.CurrentMapY, currentPlayer.CurrentMapX + 1, currentPlayer.CurrentMapY + 1)
            End If
          Else
            currentPlayer.Message = "Cannot go up out of dungeon!"
          End If
      End Select

      currentPlayer.Message = currentData
      m_levelMap.DrawScreen()
      'TODO just quit for now
      'aReturnValue = False
      'InitializeGame() ' for testing show new random level for each move
      '      InitializeGame(m_levelMap.ExitStairLocation, aNewExitStairLocation) ' for testing show new random level for each move
    Catch ex As Exception
      m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
    End Try


    Return aReturnValue
  End Function

End Module

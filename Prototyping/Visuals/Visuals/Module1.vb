' Purpose: To experiment with attempting to recreate the visual elements of Rogue (Epyx) circa mid-1980's.
Imports System
Imports Visuals.Rogue.Lib


''' <summary>
''' TODO List
''' Create random entry and exit rooms to replace hard coded test locations
''' Create movement actions to character can move around level
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


#End Region

#Region "Local Variables"
  Dim m_consoleController As New ConsoleController
  Dim m_canContinue As Boolean = True

#End Region

  Sub Main()
    '==========================================Prevent Resize
    Dim handle As IntPtr
    handle = Process.GetCurrentProcess.MainWindowHandle ' Get the handle to the console window

    Dim sysMenu As IntPtr
    sysMenu = GetSystemMenu(handle, False) ' Get the handle to the system menu of the console window

    If handle <> IntPtr.Zero Then
      'DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND) ' To prevent user from closing console window
      DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND) 'To prevent user from minimizing console window
      DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND) 'To prevent user from maximizing console window
      DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND) 'To prevent the use from re-sizing console window
    End If
    '==========================================end prevent resize
    Console.SetBufferSize(80, 25) ' or whatever size you're using...
    Console.SetWindowSize(80, 25)
    Console.BufferWidth = 80
    Console.BufferHeight = 25



    ' Challenge #1: The colors in Windows 10 console applications doesn't match exactly with those in MS-DOS.
    '               Specifically, the color orange is actually orange in Windows 10 where the color orange is 
    '               more of a brown color in MS-DOS.  This color is used for the walls in the original Rogue.

    ' Challenge #2: .NET is unicode based; where as original Rogue used ASCII and (possibly) ANSI.
    '               This means that drawing of the walls and other special characters needs to be handled
    '               using the unicode table and there isn't (AFAIK) a 1:1 mapping between ASCII and unicode.


    'Notes:
    '1 - I'm not much of a color person so I just went with DarkYellow for the brown
    '2 - Could not get the unicode to work out but did find the some of the ASCII characters


    InitializeGame()

    While m_canContinue = True
      m_canContinue = ProcessGameLoop()
    End While
    End

  End Sub

  Private Sub InitializeGame()

    Dim m_levelMap As New LevelMap
    'm_levelMap.EntryStairGrid = "1831" 'TODO testing only
    'm_levelMap.ExitStairGrid = "0470" 'TODO testing only
    m_levelMap.Initialize(True)
    m_levelMap.DrawScreen()

  End Sub

  Private Function ProcessGameLoop() As Boolean
    Dim aReturnValue As Boolean = True
    Dim aChar As New Char
    aChar = m_consoleController.GetKeyBoardInput()

    'TODO just quit for now
    'aReturnValue = False
    InitializeGame() ' for testing show new random level for each move


    Return aReturnValue
  End Function

End Module

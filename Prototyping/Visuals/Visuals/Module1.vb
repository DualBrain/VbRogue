' Purpose: To experiment with attempting to recreate the visual elements of Rogue (Epyx) circa mid-1980's.

Module Module1

#Region "Global Variables"

  Public currentLevel As Integer = 0
  Public currentPlayer As New PlayerInfo
  Public gameMap(EnumsAndConsts.MapHeight, EnumsAndConsts.MapWidth, EnumsAndConsts.MapLevelsMax) As String


#End Region

#Region "Local Variables"
  Dim m_consoleController As New ConsoleController
  Dim m_canContinue As Boolean = True

#End Region

  Sub Main()
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
    m_levelMap.EntryStairGrid = "1831" 'TODO testing only
    m_levelMap.ExitStairGrid = "0470" 'TODO testing only
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

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Level

    Public Sub New()
      Depth = 0
      ReDim Map(20, 79)
      ReDim Lights(8)
    End Sub

    Public Sub New(depth%)
      Me.Depth = depth
      ReDim Map(20, 79)
      ReDim Lights(8)
      Generate()
    End Sub

    Public Property Name As String
    Public Property Depth As Integer

    Public Lights As Boolean()
    Public Map As Tile(,)

    Public Property StartCoords As Coordinate = New Coordinate(-1, -1)

    Public Property StairCoords As Coordinate = New Coordinate(-1, -1)
    Public Property StairsFound As Boolean = False

    Private Sub Generate()

      Dim map = New Map() 'Me.Depth)
      Dim level = map.ToDungeonLevel

      Lights = level.Lights

      Me.Map = level.Map

      DetermineCoords()

    End Sub

    Public Sub DetermineCoords()

      For y = 0 To 20
        For x = 0 To 79

          Dim tile = Map(y, x)

          If tile.HeroStart Then
            StartCoords = New Coordinate(x, y)
          ElseIf tile.Type = Core.TileType.Hole Then
            StairCoords = New Coordinate(x, y)
          End If

        Next
      Next
    End Sub

    Public Shared Function DetermineZone(x%, y%) As Integer

      '     |    |
      '   0 | 1  | 2
      '     |    |
      '  ------------
      '     |    |
      '   3 | 4  | 5
      '     |    |
      '  ------------
      '     |    |
      '   6 | 7  | 8
      '     |    |
      '
      '  26 columns per horizontal zone with 2 columns of separation.
      '  Row 1: 6 lines
      '  Row 2: 7 lines
      '  Row 3: 6 lines

      Dim row = -1
      Select Case y
        Case 0 To 5 : row = 0
        Case 7 To 13 : row = 1
        Case 15 To 20 : row = 2
        Case Else
      End Select

      If row > -1 Then
        Select Case x
          Case 0 To 25 ' 0, 1, 2
            Return (row * 3)
          Case 27 To 53 ' 3, 4, 5
            Return (row * 3) + 1
          Case 55 To 80 ' 6, 7, 8
            Return (row * 3) + 2
          Case Else
        End Select
      End If

      Return -1

    End Function

  End Class

End Namespace
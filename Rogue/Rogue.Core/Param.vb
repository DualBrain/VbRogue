Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices

Namespace Global.Rogue.Core

  Public NotInheritable Class Param

    Public Shared Randomizer As New Random()

    Public Const GridRowCount As Integer = 3 ' The highest number of level grid sections height and width
    Public Const GridColumnCount As Integer = 3 ' The highest number of level grid sections height and width

    Private Const GridHeight1 As Integer = 6
    Private Const GridHeight2 As Integer = 7
    Private Const GridHeight3 As Integer = 6

    Public Const MapHeight As Integer = GridHeight1 + 1 + GridHeight2 + 1 + GridHeight3

    Private Const GridRowStart1 As Integer = 0
    Private Const GridRowStop1 As Integer = GridHeight1 - 1
    Private Const GridRowStart2 As Integer = GridRowStop1 + 1
    Private Const GridRowStop2 As Integer = GridRowStart2 + GridHeight2 - 1
    Private Const GridRowStart3 As Integer = GridRowStop2 + 1
    Private Const GridRowStop3 As Integer = GridRowStart3 + GridHeight3 - 1

    Private Const GridWidth1 As Integer = 26
    Private Const GridWidth2 As Integer = 26
    Private Const GridWidth3 As Integer = 26

    Private Const GridColumnStart1 As Integer = 0
    Private Const GridColumnStop1 As Integer = GridWidth1 - 1
    Private Const GridColumnStart2 As Integer = GridColumnStop1 + 1
    Private Const GridColumnStop2 As Integer = GridColumnStart2 + GridWidth2 - 1
    Private Const GridColumnStart3 As Integer = GridColumnStop2 + 1
    Private Const GridColumnStop3 As Integer = GridColumnStart3 + GridWidth3 - 1

    Public Const MapWidth As Integer = GridWidth1 + 1 + GridWidth2 + 1 + GridWidth3

    Public Const MapLevelsMax As Integer = 25 'The maximum number of levels in the dungeon
    Public Const MinHasRoomPercentage As Integer = 70 'Any random number out of 100 less or equal will have room

    Public Shared Function GridRow%(y%)
      Select Case y
        Case GridRowStart1 To GridRowStop1 : Return 1
        Case GridRowStart2 To GridRowStop2 : Return 2
        Case GridRowStart3 To GridRowStop3 : Return 3
        Case Else
          Return 0
      End Select
    End Function

    Public Shared Function GridColumn%(x%)
      Select Case x
        Case GridColumnStart1 To GridColumnStop1 : Return 1
        Case GridColumnStart2 To GridColumnStop2 : Return 2
        Case GridColumnStart3 To GridColumnStop3 : Return 3
        Case Else
          Return 0
      End Select
    End Function

    Public Shared Function CellStartX%(column%)
      Select Case column
        Case 1 : Return GridColumnStart1
        Case 2 : Return GridColumnStart2
        Case 3 : Return GridColumnStart3
        Case Else
          Throw New ArgumentOutOfRangeException(NameOf(column))
      End Select
    End Function

    Public Shared Function CellStartY%(row%)
      Select Case row
        Case 1 : Return GridRowStart1
        Case 2 : Return GridRowStart2
        Case 3 : Return GridRowStart3
        Case Else
          Throw New ArgumentOutOfRangeException(NameOf(row))
      End Select
    End Function

    Public Shared Function CellHeight%(row%)
      Select Case row
        Case 1 : Return GridHeight1
        Case 2 : Return GridHeight2
        Case 3 : Return GridHeight3
        Case Else
          Throw New ArgumentOutOfRangeException(NameOf(row))
      End Select
    End Function

    Public Shared Function CellWidth%(column%)
      Select Case column
        Case 1 : Return GridWidth1
        Case 2 : Return GridWidth2
        Case 3 : Return GridWidth3
        Case Else
          Throw New ArgumentOutOfRangeException(NameOf(column))
      End Select
    End Function

    Public Shared Function GridPositionToIndex%(gridColumn%, gridRow%)
      Return ((gridRow - 1) * Param.GridColumnCount) + gridColumn
    End Function

  End Class

  Friend Module Extensions

    <Extension>
    Public Function Between(value%, min%, max%) As Boolean
      Return value >= min AndAlso value <= max
    End Function

  End Module

End Namespace
﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Tile

    Sub New(c As Char)
      Me.New(CharToTileType(c))
    End Sub

    Sub New(type As TileType)

      Me.Type = type

      ' Is this a hero start point?
      Select Case type
        Case TileType.Hero
          Me.Type = TileType.Floor
          HeroStart = True
        Case Else
      End Select

      ' Is the tile "pass through"...
      Select Case type
        Case TileType.Hero, TileType.Door, TileType.Tunnel, TileType.Floor, TileType.Hole
          PassThrough = True
        Case Else
          PassThrough = False
      End Select

      ' Is the tile "secret"...
      Select Case type
        Case TileType.SecretHorizontal, TileType.SecretVertical
          Secret = True
        Case Else
          Secret = False
      End Select

    End Sub

    Public ReadOnly Property Type As TileType
    Public ReadOnly Property PassThrough As Boolean
    Public Property Explored As Boolean
    Public ReadOnly Property HeroStart As Boolean
    Public ReadOnly Property Secret As Boolean
    Private Property SearchCount As Integer

    'Public Property Gold As Integer

    Private m_objectType As Core.ObjectType = ObjectType.None

    Public Property ObjectType As Core.ObjectType
      Get
        Return m_objectType
      End Get
      Set(value As Core.ObjectType)
        m_objectType = value
        If value = ObjectType.None Then
          ObjectCount = 0
          [Object] = Nothing
        End If
      End Set
    End Property

    Public Property [Object] As Core.ObjectBase

    Public Property ObjectCount As Integer = 0

    Public Function FoundSecret(forced As Boolean) As Boolean

      If Secret Then

        SearchCount += 1

        Dim probMiss As Double

        Select Case SearchCount
          Case 1 : probMiss = 0.8
          Case 2 : probMiss = 0.64
          Case 3 : probMiss = 0.512
          Case 4 : probMiss = 0.41
          Case 5 : probMiss = 0.328
          Case 6 : probMiss = 0.262
          Case 7 : probMiss = 0.21
          Case 8 : probMiss = 0.168
          Case 9 : probMiss = 0.134
          Case 10, 11 : probMiss = 0.107
          Case 12, 13, 14 : probMiss = 0.069
          Case Else : probMiss = 0.035
        End Select

        Dim v = Param.Randomizer.NextDouble

        If forced OrElse v > probMiss Then
          _Secret = False
          _PassThrough = True
          Return True
        End If

      End If

      Return False

    End Function

    Public Overrides Function ToString() As String
      Select Case Type
        Case TileType.Void : Return " "
        Case TileType.Floor : Return "."
        Case TileType.Tunnel : Return "▓"
        Case TileType.WallTopLeft : Return "╔"
        Case TileType.WallTopRight : Return "╗"
        Case TileType.WallHorizontal : Return "═"
        Case TileType.WallVertical : Return "║"
        Case TileType.WallBottomLeft : Return "╚"
        Case TileType.WallBottomRight : Return "╝"
        Case TileType.Door : Return "╬"
        Case TileType.Hole : Return "≡"
        Case TileType.SecretHorizontal
          If Secret Then
            Return "═"
          Else
            Return "╬"
          End If
        Case TileType.SecretVertical
          If Secret Then
            Return "║"
          Else
            Return "╬"
          End If
        Case Else
          Throw New Exception("Unknown tile type.")
      End Select

    End Function

    Private Shared Function CharToTileType(c As Char) As TileType
      Select Case c
        Case " "c : Return TileType.Void
        Case "."c : Return TileType.Floor
        Case "#"c : Return TileType.Tunnel
        Case "["c : Return TileType.WallTopLeft
        Case "]"c : Return TileType.WallTopRight
        Case "-"c : Return TileType.WallHorizontal
        Case "|"c : Return TileType.WallVertical
        Case "{"c : Return TileType.WallBottomLeft
        Case "}"c : Return TileType.WallBottomRight
        Case "+"c : Return TileType.Door
        Case "="c : Return TileType.Hole
        Case "@"c : Return TileType.Hero
        Case "/"c : Return TileType.SecretHorizontal
        Case "\"c : Return TileType.SecretVertical
        Case Else
          Throw New Exception("Unknown tile type.")
      End Select
    End Function

    Public Shared Function TileTypeToCellType(c As TileType) As CellType
      Select Case c
        Case TileType.Void : Return CellType.Void
        Case TileType.Floor : Return CellType.StructureFloor
        Case TileType.Tunnel : Return CellType.StructureTunnel
        Case TileType.WallTopLeft : Return CellType.StructureWallTopLeftCorner
        Case TileType.WallTopRight : Return CellType.StructureWallTopRightCorner
        Case TileType.WallHorizontal : Return CellType.StructureWallTopBottom
        Case TileType.WallVertical : Return CellType.StructureWallSide
        Case TileType.WallBottomLeft : Return CellType.StructureWallBottomLeftCorner
        Case TileType.WallBottomRight : Return CellType.StructureWallBottomRightCorner
        Case TileType.Door : Return CellType.StructureDoor
        Case TileType.Hole : Return CellType.StructureStairsDown
        Case TileType.Hero : Return CellType.Hero
        Case TileType.SecretHorizontal : Return CellType.SecretHorizontal
        Case TileType.SecretVertical : Return CellType.SecretVertical
        Case Else
          Throw New Exception("Unknown tile type.")
      End Select
    End Function

  End Class

End Namespace
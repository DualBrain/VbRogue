Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Level

    Public Sub New()
      Me.Depth = 0
      ReDim Me.Map(20, 79)
      ReDim Me.Lights(8)
    End Sub

    Public Sub New(depth%)
      Me.Depth = depth
      ReDim Me.Map(20, 79)
      ReDim Me.Lights(8)
      Me.Generate()
    End Sub

    Public Property Name As String
    Public Property Depth As Integer
    Public Property Lights As Boolean()
    Public Property Map As Tile(,)

    Private Sub Generate()
      Dim map = New Map() 'Me.Depth)
      Dim level = map.ToDungeonLevel
      Me.Lights = level.Lights
      Me.Map = level.Map
    End Sub

  End Class

End Namespace
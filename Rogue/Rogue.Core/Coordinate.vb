Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public Class Coordinate

    Public Sub New(x%, y%)
      Me.X = x
      Me.Y = y
    End Sub

    Public ReadOnly Property X As Integer
    Public ReadOnly Property Y As Integer

    Public Shared Operator =(c1 As Coordinate, c2 As Coordinate) As Boolean
      Return c1.X = c2.X AndAlso c1.Y = c2.Y
    End Operator

    Public Shared Operator <>(c1 As Coordinate, c2 As Coordinate) As Boolean
      Return Not c1.X = c2.X AndAlso c1.Y = c2.Y
    End Operator

    Public Overrides Function Equals(obj As Object) As Boolean
      If TypeOf obj Is Coordinate Then
        Dim o = DirectCast(obj, Coordinate)
        Return X = o.X AndAlso Y = o.Y
      Else
        Return False
      End If
    End Function

    Public Overrides Function ToString() As String
      Return $"({X},{Y})"
    End Function

  End Class

End Namespace
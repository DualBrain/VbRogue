Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Door
    Inherits Coordinate

    Public Sub New(x%, y%, face As Face)
      MyBase.New(x, y)
      Me.Face = face
      HasConnections = False
    End Sub

    Public ReadOnly Property Face As Face
    Public Property HasConnections As Boolean

  End Class

End Namespace
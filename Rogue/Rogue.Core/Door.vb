Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Door
    Inherits Coordinate

    Public Sub New(x%, y%, face As Face, secret As Boolean)
      MyBase.New(x, y)
      Me.Face = face
      HasConnections = False
      Me.Secret = secret
      Visible = Not Me.Secret
    End Sub

    Public ReadOnly Property Face As Face
    Public ReadOnly Property Secret As Boolean
    Public Property HasConnections As Boolean
    Public ReadOnly Property Visible As Boolean
  End Class

End Namespace
Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public MustInherit Class Entity

    Public Property AC As Integer = 0

    Public Property MaxHP As Integer = 0
    Public Property HP As Integer = 0
    Public Property Level As Integer = 0

    Public Property X As Integer = -1
    Public Property Y As Integer = -1

    Public Property SightDistance As Integer = 2

  End Class

End Namespace
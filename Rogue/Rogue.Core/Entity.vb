Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public MustInherit Class Entity

    Public Property Armor As Integer = 0

    Public Property MaxHitPoints As Integer = 0
    Public Property CurrentHitPoints As Integer = 0
    Public Property Level As Integer = 0

    Public Property X As Integer = -1
    Public Property Y As Integer = -1

    Public Property SightDistance As Integer = 2

  End Class

End Namespace
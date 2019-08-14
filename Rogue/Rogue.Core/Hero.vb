Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Hero
    Inherits Entity

    Public Sub New()

      'for prototype set the variables that will be displayed
      Armor = 5

      Level = 1

      MaxStrength = 16
      CurrentStrength = 16

      MaxHitPoints = 21
      CurrentHitPoints = 21

      Gold = 0

    End Sub

    Public Property MaxStrength As Integer = 0
    Public Property CurrentStrength As Integer = 0
    Public Property Gold As Integer = 0
    Public Property Message As String = ""

  End Class

End Namespace
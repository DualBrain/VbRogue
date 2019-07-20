Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Hero
    Inherits Entity

    Public Sub New()

      'for prototype set the variables that will be displayed
      Me.Armor = 5

      Me.Level = 1

      Me.MaxStrength = 16
      Me.CurrentStrength = 16

      Me.MaxHitPoints = 21
      Me.CurrentHitPoints = 21

      Me.Gold = 0

    End Sub

    Public Property MaxStrength As Integer = 0
    Public Property CurrentStrength As Integer = 0
    Public Property Gold As Integer = 0
    Public Property Message As String = ""

  End Class

End Namespace
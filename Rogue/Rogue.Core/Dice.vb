Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Dice

    Public Sub New(count As Integer, sides As Integer)
      Me.Count = count
      Me.Sides = sides
    End Sub

    Public Property Count As Integer
    Public Property Sides As Integer

    Public Shared Function RollDamage(damage As Dice) As Integer
      Dim result = 0
      For d = 1 To damage.Count ' 4
        Dim amount = Param.Randomizer.Next(1, damage.Sides + 1) ' 6
        result += amount
      Next
      Return result
    End Function

  End Class

End Namespace
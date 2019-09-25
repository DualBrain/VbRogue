Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public MustInherit Class Entity

    Public Property AC As Integer = 0

    Public Property MaxHP As Integer = 0

    Private m_hp As Integer = 0

    Public Property HP As Integer
      Get
        Return m_hp
      End Get
      Set(value As Integer)
        m_hp = value
        If m_hp < 1 Then
          ' DEATH!
          Dead = True
        End If
      End Set
    End Property

    Public Property DungeonLevel As Integer = 0
    Public Property ExperienceLevel As Integer = 1

    Public Property X As Integer = -1
    Public Property Y As Integer = -1

    Public Property SightDistance As Integer = 2

    Public Property Dead As Boolean = False

  End Class

End Namespace
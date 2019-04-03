Namespace Rogue.Lib
  ''' <summary>
  ''' Keep Track of all information about the current player. 
  ''' </summary>
  Public Class PlayerInfo

    Private m_HitPoints As Integer
    Private m_CurrentHitPoints As Integer
    Private m_CurrentMapLevel As Integer
    Private m_CurrentStrength As Integer
    Private m_Strength As Integer
    Private m_Gold As Integer
    Private m_Armor As Integer
    Private m_CharacterClass As String
    Private m_CurrentMapX As Integer
    Private m_CurrentMapY As Integer
    Private m_Message As String
    Private m_SightDistance As Integer

#Region "Properties"

    Public Property Armor As Integer
      Get
        Return Me.m_Armor
      End Get
      Set(value As Integer)
        Me.m_Armor = value
      End Set
    End Property

    Public Property CharacterClass As String
      Get
        Return Me.m_CharacterClass
      End Get
      Set(value As String)
        Me.m_CharacterClass = value
      End Set
    End Property

    Public Property CurrentHitPoints As Integer
      Get
        Return Me.m_CurrentHitPoints
      End Get
      Set(value As Integer)
        Me.m_CurrentHitPoints = value
      End Set
    End Property

    Public Property CurrentMapLevel As Integer
      Get
        Return Me.m_CurrentMapLevel
      End Get
      Set(value As Integer)
        Me.m_CurrentMapLevel = value
      End Set
    End Property

    Public Property CurrentStrength As Integer
      Get
        Return Me.m_CurrentStrength
      End Get
      Set(value As Integer)
        Me.m_CurrentStrength = value
      End Set
    End Property

    Public Property Gold As Integer
      Get
        Return Me.m_Gold
      End Get
      Set(value As Integer)
        Me.m_Gold = value
      Get
        Return Me.m_HitPoints
      End Get
      Set(value As Integer)
        Me.m_HitPoints = value
      End Set
    End Property

    Public Property Strength As Integer
      Get
        Return Me.m_Strength
      End Get
      Set(value As Integer)
        Me.m_Strength = value
      End Set
    End Property

    Public Property CurrentMapX As Integer
      Get
        Return Me.m_CurrentMapX
      End Get
      Set(value As Integer)
        Me.m_CurrentMapX = value
      End Set
    End Property

    Public Property CurrentMapY As Integer
      Get
        Return Me.m_CurrentMapY
      End Get
      Set(value As Integer)
        Me.m_CurrentMapY = value
      End Set
    End Property

    Public Property Message As String
      Get
        Return Me.m_Message
      End Get
      Set(value As String)
        Me.m_Message = value
      End Set
    End Property

    Public Property SightDistance As Integer
      Get
        Return Me.m_SightDistance
      End Get
      Set(value As Integer)
        Me.m_SightDistance = value
      End Set
    End Property



#End Region

#Region "Public Methods"

    Public Sub New()
      Me.Initialize()
    End Sub


#End Region


#Region "Private Methods"

    Private Sub Initialize()
      Me.m_Armor = 0
      Me.m_CharacterClass = ""
      Me.m_CurrentHitPoints = 0
      Me.m_CurrentMapLevel = 0
      Me.m_CurrentStrength = 0
      Me.m_Gold = 0
      Me.m_HitPoints = 0
      Me.m_Strength = 0
      Me.m_CurrentMapX = -1
      Me.m_CurrentMapY = -1
      Me.m_Message = ""
      m_SightDistance = 2

      'for prototype set the variables that will be displayed
      Me.m_Armor = 5
      Me.m_CharacterClass = "Apprentice"
      Me.m_CurrentHitPoints = 21
      Me.m_CurrentMapLevel = 1
      Me.m_CurrentStrength = 16
      Me.m_Gold = 50
      Me.m_HitPoints = 21
      Me.m_Strength = 16


    End Sub


#End Region

  End Class
End Namespace

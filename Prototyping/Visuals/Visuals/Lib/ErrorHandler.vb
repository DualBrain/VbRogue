Namespace Rogue.Lib

  Public Class ErrorHandler

    Private m_ModuleName As String
    Private m_MethodName As String
    Private m_Description As String
    Private m_ErrorDate As Date
    Private m_CurrentError As SystemException


#Region "Properties"

    Public Property ModuleName As String
      Get
        Return Me.m_ModuleName
      End Get
      Set(value As String)
        Me.m_ModuleName = value
      End Set
    End Property

    Public Property MethodName As String
      Get
        Return Me.m_MethodName
      End Get
      Set(value As String)
        Me.m_MethodName = value
      End Set
    End Property

    Public Property Description As String
      Get
        Return Me.m_Description
      End Get
      Set(value As String)
        Me.m_Description = value
      End Set
    End Property

    Public Property ErrorDate As Date
      Get
        Return Me.m_ErrorDate
      End Get
      Set(value As Date)
        Me.m_ErrorDate = value
      End Set
    End Property

    Public Property CurrentError As SystemException
      Get
        Return Me.m_CurrentError
      End Get
      Set(value As SystemException)
        Me.m_CurrentError = value
      End Set
    End Property


#End Region

#Region "Public Methods"

    Public Sub New()
      Me.Initialize()
    End Sub

    Public Sub NotifyError(ByRef whatModule As String, ByRef whatMethod As String, ByRef whatDescription As String, ByRef whatDate As Date, ByRef whatEx As SystemException)
      MsgBox(whatDescription & vbCrLf & vbCrLf & "StackTrace: " & whatEx.StackTrace, MsgBoxStyle.OkOnly, "Error")
    End Sub

#End Region

#Region "Private Methods"

    Private Sub Initialize()
      Me.m_ModuleName = ""
      Me.m_MethodName = ""
      Me.m_Description = ""
      Me.m_ErrorDate = Now
      Me.m_CurrentError = New SystemException

    End Sub


#End Region

  End Class
End Namespace

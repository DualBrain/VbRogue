Imports Microsoft.VisualBasic

Imports System.IO
Imports System.Xml.Serialization
<Serializable> Public Class Monster
    Private mMonsterClass As String
    Public Property MonsterClass() As String
        Get
            Return mMonsterClass
        End Get
        Set(ByVal value As String)
            mMonsterClass = value
        End Set
    End Property

    Private mName As String
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Private mDescription As String
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
        End Set
    End Property

    Private mAsciiChar As String
    Public Property AsciiChar() As String
        Get
            Return mAsciiChar
        End Get
        Set(ByVal value As String)
            mAsciiChar = value
        End Set
    End Property

    Private mAttack1 As String
    Public Property Attack1() As String
        Get
            Return mAttack1
        End Get
        Set(ByVal value As String)
            mAttack1 = value
        End Set
    End Property

    Private mAttack2 As String
    Public Property Attack2() As String
        Get
            Return mAttack2
        End Get
        Set(ByVal value As String)
            mAttack2 = value
        End Set
    End Property

    Private mAttack3 As String
    Public Property Attack3() As String
        Get
            Return mAttack3
        End Get
        Set(ByVal value As String)
            mAttack3 = value
        End Set
    End Property

    Private mDifficulty As String
    Public Property Difficulty() As String
        Get
            Return mDifficulty
        End Get
        Set(ByVal value As String)
            mDifficulty = value
        End Set
    End Property

    Private mBaseLevel As String
    Public Property BaseLevel() As String
        Get
            Return mBaseLevel
        End Get
        Set(ByVal value As String)
            mBaseLevel = value
        End Set
    End Property

    Private mBaseExperience As String
    Public Property BaseExperience() As String
        Get
            Return mBaseExperience
        End Get
        Set(ByVal value As String)
            mBaseExperience = value
        End Set
    End Property

    Private mSpeed As String
    Public Property Speed() As String
        Get
            Return mSpeed
        End Get
        Set(ByVal value As String)
            mSpeed = value
        End Set
    End Property

    Private mBaseAC As String
    Public Property BaseAC() As String
        Get
            Return mBaseAC
        End Get
        Set(ByVal value As String)
            mBaseAC = value
        End Set
    End Property

    Private mBaseMR As String
    Public Property BaseMR() As String
        Get
            Return mBaseMR
        End Get
        Set(ByVal value As String)
            mBaseMR = value
        End Set
    End Property

    Private mAlignment As String
    Public Property Alignment() As String
        Get
            Return mAlignment
        End Get
        Set(ByVal value As String)
            mAlignment = value
        End Set
    End Property

    Private mFrequency As String
    Public Property Frequency() As String
        Get
            Return mFrequency
        End Get
        Set(ByVal value As String)
            mFrequency = value
        End Set
    End Property

    Private mGenoCidable As String
    Public Property GenoCidable() As String
        Get
            Return mGenoCidable
        End Get
        Set(ByVal value As String)
            mGenoCidable = value
        End Set
    End Property

    Private mWeight As String
    Public Property Weight() As String
        Get
            Return mWeight
        End Get
        Set(ByVal value As String)
            mWeight = value
        End Set
    End Property

    Private mNutritionalValue As String
    Public Property NutritionalValue() As String
        Get
            Return mNutritionalValue
        End Get
        Set(ByVal value As String)
            mNutritionalValue = value
        End Set
    End Property

    Private mSize As String
    Public Property Size() As String
        Get
            Return mSize
        End Get
        Set(ByVal value As String)
            mSize = value
        End Set
    End Property

    Private mResistances As String
    Public Property Resistances() As String
        Get
            Return mResistances
        End Get
        Set(ByVal value As String)
            mResistances = value
        End Set
    End Property

    Private mResistancesConveyed As String
    Public Property ResistancesConveyed() As String
        Get
            Return mResistancesConveyed
        End Get
        Set(ByVal value As String)
            mResistancesConveyed = value
        End Set
    End Property

    Private mCharacteristics As List(Of String)
    Public Property Characteristics As List(Of String)
        Get
            Return mCharacteristics
        End Get
        Set(ByVal value As List(Of String))
            mCharacteristics = value
        End Set
    End Property

    Private mNetHackWikiUrl As String
    Public Property NetHackWikiUrl() As String
        Get
            Return mNetHackWikiUrl
        End Get
        Set(ByVal value As String)
            mNetHackWikiUrl = value
        End Set
    End Property

    Public Function SaveXML(ByVal sFile As String, ByRef TheMonster As Monster) As String
        'Works!
        Try
            If Not File.Exists(sFile) Then
                sFile = Application.StartupPath & "\" & Me.Name & ".xml"
            End If

            Dim xsSerializer As New XmlSerializer(GetType(Monster))
            Dim oStreamWriter As New StreamWriter(sFile)

            xsSerializer.Serialize(oStreamWriter, TheMonster)
            oStreamWriter.Close()
            xsSerializer = Nothing
            'Me.XMLFilePath = sFile
            Return "Serialized to : " & sFile
        Catch ex As Exception
            'RaiseEvent PerError("Error in: SaveXML: " & ex.Message, ex)
            Return "Error in serialization of " & sFile & " " & ex.Message
        End Try
    End Function

    Public Function LoadXML(ByVal sFile As String) As Monster
        Try
            'Given a filename, deserialize the xml file...
            If Not System.IO.File.Exists(sFile) Then
                'RaiseEvent MError("File: " & sFile & " does not exist.", New Exception)
                Return Nothing
            End If

            Dim TheMonster As New Monster
            Dim xsDeserializer As New XmlSerializer(GetType(Monster))
            Dim oStreamReader As New StreamReader(sFile)

            TheMonster = xsDeserializer.Deserialize(oStreamReader)

            oStreamReader.Close()

            If TheMonster Is Nothing Then
                'RaiseEvent MError("XML load failed for file: " & sFile, New Exception)
                Return Nothing
            End If

            Return TheMonster

        Catch ex As Exception
            'RaiseEvent MError("Error in: LoadXML: " & ex.Message, ex)
            Return Nothing
        End Try
    End Function
End Class


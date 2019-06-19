Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml.Serialization
<Serializable> Public Class MonsterList
    Public Monsters As List(Of Monster)

    Public Function SaveXML(ByVal sFile As String, ByRef TheMonsters As MonsterList) As String
        'Works!
        Try
            If Not File.Exists(sFile) Then
                sFile = Application.StartupPath & "\" & "MonsterList" & ".xml"
            End If

            Dim xsSerializer As New XmlSerializer(GetType(MonsterList))
            Dim oStreamWriter As New StreamWriter(sFile)

            xsSerializer.Serialize(oStreamWriter, TheMonsters)
            oStreamWriter.Close()
            xsSerializer = Nothing
            'Me.XMLFilePath = sFile
            Return "Serialized to : " & sFile
        Catch ex As Exception
            'RaiseEvent PerError("Error in: SaveXML: " & ex.Message, ex)
            Return "Error in serialization of " & sFile & " " & ex.Message
        End Try
    End Function

End Class


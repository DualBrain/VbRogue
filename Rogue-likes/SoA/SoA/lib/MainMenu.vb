Public Class MainMenu

    'Trida s uvitaci obrazovkou

	Dim cCon As New cCon
	Dim WindowControls As New WindowControls
	
	Sub MainMenu()
		
		cCon.WriteMainMenu()
		
		While True
			WindowControls.MainMenuInput
			
			If WindowOpen = False Then
				Exit Sub
			End If
		End While
	End Sub
End Class

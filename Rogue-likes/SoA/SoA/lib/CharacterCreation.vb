Public Class CharacterCreation
	
	Dim cCon As New cCon
	Dim WindowControls As New WindowControls
	
	Sub CreatingCharacter()
		
		cCon.WriteChoosingName()
		Player.Name = Console.ReadLine()
		
		cCon.WriteChoosingGender()
		WindowControls.ChoosingGenderInput()
		
		cCon.WriteChoosingClass()
		WindowControls.ChoosingClassInput()
		
		cCon.WriteChoosingRace()
		WindowControls.ChoosingRaceInput()
		
	End Sub
	
End Class

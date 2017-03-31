Public Class MainMenuHelp
	
	Dim cCon As New cCon
	
	Sub MMHelp()
		
		cCon.WriteMMHelp1()
		Console.ReadKey(True)
		cCon.WriteMMHelp2()
		Console.ReadKey(True)
		
		cCon.WriteMainMenu()
		
	End Sub
	
End Class

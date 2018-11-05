Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Visuals.Rogue.Lib


<TestClass()> Public Class LevelMapTest


  <TestMethod()> Public Sub TestGetCoordinateString()
    Dim testItem As New LevelMap
    Dim actual As String = ""
    Dim expected As String = ""
    Dim xPtr As Integer = 0
    Dim yPtr As Integer = 0
    Dim xStartingPtr As Integer = 0
    Dim yStartingPtr As Integer = 0


    'set up test to wrap back to same room
    yPtr = 18
    xPtr = 28
    expected = "1828"
    actual = testItem.CallGetCoordinateString(yPtr, xPtr)
    Assert.AreEqual(expected, actual)

    'set up test to find different room
    yPtr = 7
    xPtr = 8
    expected = "0708"
    actual = testItem.CallGetCoordinateString(yPtr, xPtr)
    Assert.AreEqual(expected, actual)


  End Sub

  <TestMethod()> Public Sub TestGetRoomNumberFromXY()
    Dim testItem As New LevelMap
    Dim actual As Integer = 0
    Dim expected As Integer = 0
    Dim xPtr As Integer = 0
    Dim yPtr As Integer = 0

    'set up test
    xPtr = 5
    yPtr = 1
    expected = 1
    actual = testItem.CallGetRoomNumberFromXY(xPtr, yPtr)
    Assert.AreEqual(expected, actual)

    xPtr = 111
    yPtr = 1
    expected = 0
    actual = testItem.CallGetRoomNumberFromXY(xPtr, yPtr)
    Assert.AreEqual(expected, actual)


  End Sub

  <TestMethod()> Public Sub TestUpdateMap()
    Dim testItem As New LevelMap
    Dim actual As String = ""
    Dim expected As String = ""
    Dim aRoom As New LevelRoom

    'set up test
    aRoom = aRoom.CreateRandomRoom(1, 1, 4, 4)
    expected = aRoom.ToString
    actual = testItem.CallUpdateMap(aRoom)
    Assert.AreEqual(actual, expected)


  End Sub

End Class
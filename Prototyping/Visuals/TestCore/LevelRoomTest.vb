Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Visuals.Rogue.Lib


<TestClass()> Public Class LevelRoomTest

  <TestMethod()> Public Sub TestCreateRandomEntryRoom()
    Dim testItem As New LevelRoom
    Dim actual As New LevelRoom()
    Dim expected As New LevelRoom()
    Dim aRoom As New LevelRoom
    Dim aGridRow As Integer = 0
    Dim aGridColumn As Integer = 0
    Dim aEntryRow As Integer = 0
    Dim aEntryColumn As Integer = 0
    Dim aHeight As Integer = 0
    Dim aWidth As Integer = 0

    'set up test
    aGridColumn = 1
    aGridRow = 1
    aEntryColumn = 2
    aEntryRow = 6
    aHeight = 5
    aWidth = 4
    actual = testItem.CallCreateRandomEntryRoom(aGridRow, aGridColumn, aEntryRow, aEntryColumn, aHeight, aWidth)
    'Assert.AreEqual(actual, expected)
    Assert.IsTrue(actual.RoomNumber > 0)
    Assert.IsTrue(actual.MapLeftLocation < aEntryColumn)
    Assert.IsTrue(actual.MapTopLocation < aEntryRow)
    Assert.IsTrue(actual.MapLeftLocation + actual.CurrentWidth > aEntryColumn)
    Assert.IsTrue(actual.MapTopLocation + actual.CurrentHeight > aEntryRow)



    aGridColumn = 1
    aGridRow = 1
    aEntryColumn = 2
    aEntryRow = 4
    aHeight = 5
    aWidth = 4
    actual = testItem.CallCreateRandomEntryRoom(aGridRow, aGridColumn, aEntryRow, aEntryColumn, aHeight, aWidth)
    'Assert.AreEqual(actual, expected)
    Assert.IsTrue(actual.RoomNumber > 0)
    Assert.IsTrue(actual.MapLeftLocation < aEntryColumn)
    Assert.IsTrue(actual.MapTopLocation < aEntryRow)
    Assert.IsTrue(actual.MapLeftLocation + actual.CurrentWidth > aEntryColumn)
    Assert.IsTrue(actual.MapTopLocation + actual.CurrentHeight > aEntryRow)

  End Sub


End Class
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Orders

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub Get_ListingMirror_EmptyUrl()

        Dim returnedString As String = DexelOrder.GetRequest("", "", "")

        Assert.IsTrue(String.IsNullOrEmpty(returnedString))

    End Sub

    <TestMethod()> Public Sub Get_DummyAPI_WithOutUserAndPassword()

        Dim returnedString As String = DexelOrder.GetRequest("https://reqres.in/api/users?page=2", "", "")

        Assert.IsFalse(String.IsNullOrEmpty(returnedString))

    End Sub


    <TestMethod()> Public Sub Post_DummyAPI_WithOutUserAndPassword()

        Dim jsonRequest As String = "{""name"":""morpheus"",""job"":""leader""}"
        Dim returnedString As String = DexelOrder.PostRequest("https://reqres.in/api/users", "", "", jsonRequest)

        Assert.IsFalse(String.IsNullOrEmpty(returnedString))

    End Sub

    <TestMethod()> Public Sub Post_DummyAPI_EmptyUrl()

        Dim jsonRequest As String = "{""name"":""morpheus"",""job"":""leader""}"
        Dim returnedString As String = DexelOrder.PostRequest("", "", "", jsonRequest)

        Assert.IsTrue(String.IsNullOrEmpty(returnedString))

    End Sub

End Class
Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Net
Imports System.Text

Public NotInheritable Class DexelOrder

    <Microsoft.SqlServer.Server.SqlFunction>
    Public Shared Function GetRequest(url As String, username As String, password As String) As String
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls

        If String.IsNullOrEmpty(url) Then
            Return String.Empty
        End If

        Try
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"


            If Not String.IsNullOrEmpty(username) Then
                Dim authInfo As String = Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}"))
                request.Headers("Authorization") = "Basic " + authInfo
            End If

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using dataStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(dataStream)
                        Dim responseFromServer As String = reader.ReadToEnd()
                        Return responseFromServer
                    End Using
                End Using
            End Using
        Catch ex As FormatException
            Return String.Empty
        End Try
    End Function

    <Microsoft.SqlServer.Server.SqlFunction>
    Public Shared Function PostRequest(url As String, username As String, password As String, jsonData As String) As String
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls

        If String.IsNullOrEmpty(url) Then
            Return String.Empty
        End If

        Dim responseFromServer As String = ""
        Dim request As HttpWebRequest = Nothing
        Dim response As WebResponse = Nothing
        Dim dataStream As Stream = Nothing
        Dim reader As StreamReader = Nothing
        Try
            request = DirectCast(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Timeout = 10000

            If Not String.IsNullOrEmpty(username) Then
                Dim authInfo As String = Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}"))
                request.Headers("Authorization") = "Basic " + authInfo
            End If

            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(jsonData)
            request.ContentLength = byteArray.Length

            dataStream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            response = request.GetResponse()
            dataStream = response.GetResponseStream()

            reader = New StreamReader(dataStream)
            responseFromServer = reader.ReadToEnd()

        Catch ex As WebException
            ' Handle web exceptions here
        Catch ex As Exception
            ' Handle other types of exceptions here
        Finally
            ' Close all the resources
            If reader IsNot Nothing Then
                reader.Close()
            End If
            If dataStream IsNot Nothing Then
                dataStream.Close()
            End If
            If response IsNot Nothing Then
                response.Close()
            End If
        End Try
        Return responseFromServer
    End Function




End Class



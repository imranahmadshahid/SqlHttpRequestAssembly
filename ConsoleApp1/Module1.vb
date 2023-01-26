Imports System.Net

Module Module1

    Sub Main()
        Try
            Dim fr As System.Net.HttpWebRequest
            Dim targetURI As New Uri("http://whatever.you.want.to.get/file.html")

            fr = DirectCast(HttpWebRequest.Create(targetURI), System.Net.HttpWebRequest)
            If (fr.GetResponse().ContentLength > 0) Then
                Dim str As New System.IO.StreamReader(fr.GetResponse().GetResponseStream())
                Response.Write(str.ReadToEnd())
                str.Close(); 
        End If
        Catch ex As System.Net.WebException
            'Error in accessing the resource, handle it
        End Try
    End Sub

End Module

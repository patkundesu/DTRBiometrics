﻿Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Module DBFunctions
    Sub Test()
        Dim connection As SqlConnection = New SqlConnection()
        connection.ConnectionString = "Data Source=KABIR-DESKTOP;Initial Catalog=testDB;Integrated Security=True"
        connection.Open()

    End Sub

    Public Function ValidateCredentials(usrn As String, encr As String) As Boolean
        Dim filter = String.Format("usrn = '{0}' AND pssw = '{1}'", usrn, encr)
        Return tblAdminAdapter.GetData.Select(filter).Count > 0
    End Function
    Public Function CreateGenericPassword() As String
        Dim pass = "password"
        Return HashPassword(pass)
    End Function

    Public Function HashPassword(pssw As String)
        Dim bytHashedData As Byte()

        Dim encoder As New UTF8Encoding()

        Dim md5Hasher As New MD5CryptoServiceProvider

        bytHashedData = md5Hasher.ComputeHash(encoder.GetBytes(pssw))
        Dim sBuilder As New StringBuilder
        Dim i As Integer
        For i = 0 To bytHashedData.Length - 1
            sBuilder.Append(bytHashedData(i).ToString("x2"))
        Next i
        Return sBuilder.ToString
    End Function
End Module

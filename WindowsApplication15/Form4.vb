Imports MetroFramework.Forms
Public Class Form4
    Inherits MetroForm
    Public errors(0) As String
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 0 To UBound(errors)
            RichTextBox1.Text = RichTextBox1.Text & errors(i) & vbCrLf
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\errors.txt") Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\errors.txt")
        End If
        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\errors.txt", RichTextBox1.Text, False)
    End Sub
End Class
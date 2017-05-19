Imports MySql.Data.MySqlClient
Imports MetroFramework.Forms
Imports OpenQA.Selenium
Imports System.Net.Mail
Imports System.Net

Public Class Form1
    Inherits MetroForm
    Public inmateID() As String = {"00000"}
    Public messages() As String
    Public send As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If My.Settings.fb = "" Then
        Else
            Try
                MetroGrid2.Rows.Clear()
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(My.Settings.fb)
                For Each line As String In RichTextBox1.Lines
                    MetroGrid2.Rows.Add()
                    Dim info() As String
                    Try
                        info = line.Split(":")
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(0).Value = info(0)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(1).Value = info(1)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(2).Value = info(2)
                    Catch ex As Exception
                        MetroGrid2.Rows.RemoveAt(MetroGrid2.Rows.Count - 2)
                    End Try
                Next
                RichTextBox1.Text = ""
            Catch ex As Exception

            End Try
        End If

        'Auto()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Dim cn As New MySqlConnection
        Dim cmd As MySqlCommand

        'Debug: localhost database
        'http://www.mailmypix.com:2082/
        'mailmypi *={f@eLg1OZe
        'cn.ConnectionString = "server=localhost;Port=3306;user id=root;password=;database=mailmypi_messages;Character Set=utf8;"
        cn.ConnectionString = "server=mailmypix.com;Port=3306;user id=mailmypi_mail;password=12345;database=mailmypi_messages;Character Set=utf8;"
        Dim reader As MySqlDataReader
        MetroGrid1.Rows.Clear()
        Try
            cn.Open()
            Dim query As String
            query = "SELECT * FROM facebook WHERE id > " & My.Settings.LastId
            'Debug: localhost database
            'query = "SELECT * FROM facebook"
            cmd = New MySqlCommand(query, cn)
            reader = cmd.ExecuteReader
            Dim i As Integer
            i = 0
            While reader.Read()
                'Les champs à recup. Les tables commencent à 0.
                MetroGrid1.Rows.Add()
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(0).Value = ((reader.GetString(1)))
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(1).Value = ((reader.GetString(2)))
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(2).Value = ((reader.GetString(3)))
                TextBox2.Text = reader.GetInt16(0).ToString
                ReDim Preserve messages(i)
                messages(i) = ((reader.GetString(4)))
                i = i + 1
            End While
            cn.Close()
        Catch ex As Exception

        End Try
        cn.Dispose()
        OpenFileDialog1.ShowDialog()
        Dim s As String
        s = OpenFileDialog1.FileName
        Try
            s = s.Substring(s.LastIndexOf("\") + 1)
        Catch ex As Exception
            MsgBox("Please try again ...")
            GoTo 2
        End Try

        Select Case s.ToLower
            Case "fb.db"
                MetroGrid2.Rows.Clear()
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
                For Each line As String In RichTextBox1.Lines
                    MetroGrid2.Rows.Add()
                    Dim info() As String
                    Try
                        info = line.Split(":")
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(0).Value = info(0)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(1).Value = info(1)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(2).Value = info(2)
                    Catch ex As Exception
                        MetroGrid2.Rows.RemoveAt(MetroGrid2.Rows.Count - 2)
                    End Try
                Next
                My.Settings.fb = OpenFileDialog1.FileName
                My.Settings.Save()
                RichTextBox1.Text = ""
        End Select
2:
        TextBox1.Text = My.Settings.LastId.ToString
        If TextBox2.Text = "" Then
            TextBox2.Text = TextBox1.Text
        End If
        MetroButton2.Enabled = True

    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Start()
    End Sub

    Sub Auto()
        Dim cn As New MySqlConnection
        Dim cmd As MySqlCommand

        'Debug: localhost database
        'cn.ConnectionString = "server=localhost;Port=3306;user id=root;password=;database=mailmypi_messages;Character Set=utf8;"
        cn.ConnectionString = "server=mailmypix.com;Port=3306;user id=mailmypi_mail;password=12345;database=mailmypi_messages;Character Set=utf8;"
        Dim reader As MySqlDataReader
        Try
            cn.Open()
            Dim query As String
            query = "SELECT * FROM facebook WHERE id > " & My.Settings.LastId
            'Debug: localhost database
            'query = "SELECT * FROM facebook"
            cmd = New MySqlCommand(query, cn)
            reader = cmd.ExecuteReader
            Dim i As Integer
            i = 0
            While reader.Read()
                MetroGrid1.Rows.Add()
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(0).Value = ((reader.GetString(1)))
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(1).Value = ((reader.GetString(2)))
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(2).Value = ((reader.GetString(3)))
                ReDim Preserve messages(i)
                messages(i) = ((reader.GetString(4)))
                i = i + 1
            End While
            cn.Close()
        Catch ex As Exception

        End Try
        cn.Dispose()
        Dim s As String
        s = "fb.db"

        Select Case s.ToLower
            Case "fb.db"
                MetroGrid2.Rows.Clear()
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(s)
                For Each line As String In RichTextBox1.Lines
                    MetroGrid2.Rows.Add()
                    Dim info() As String
                    Try
                        info = line.Split(":")
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(0).Value = info(0)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(1).Value = info(1)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(2).Value = info(2)
                    Catch ex As Exception
                        MetroGrid2.Rows.RemoveAt(MetroGrid2.Rows.Count - 2)
                    End Try
                Next
                My.Settings.fb = OpenFileDialog1.FileName
                My.Settings.Save()
                RichTextBox1.Text = ""
        End Select
2:
        Threading.Thread.Sleep(20000)
        Start()
    End Sub

    Sub Start()


        Dim i As Long
            i = 0
        For i = 0 To MetroGrid1.Rows.Count - 2
            Dim j As Long
            j = 0
            For j = 0 To MetroGrid2.Rows.Count - 2
                If MetroGrid2.Rows(j).Cells(0).Value.trim = MetroGrid1.Rows(i).Cells(0).Value.trim Then
                    Select Case MetroGrid1.Rows(i).Cells(1).Value.ToString.ToLower.Trim
                        Case "inbox"
                            Dim navigator As New Firefox.FirefoxDriver
                            msg(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                        Case "wall"
                            If MetroGrid1.Rows(i).Cells(2).Value.ToString.ToLower.Trim = "me" Then
                                Dim navigator As New Firefox.FirefoxDriver
                                Post(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, messages(i))
                            Else
                                Dim navigator As New Firefox.FirefoxDriver
                                PostOw(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                            End If
                        Case "friend"
                            Dim navigator As New Firefox.FirefoxDriver
                            addFriend(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                        Case "accept"
                            Dim navigator As New Firefox.FirefoxDriver
                            acceptFriend(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                        Case "photo"
                            Dim navigator As New Firefox.FirefoxDriver
                            Photo(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value)
                    End Select
                    Exit For
                End If
            Next
            TextBox1.Text = My.Settings.LastId.ToString
        Next
        MetroButton2.Enabled = False
    End Sub
    Sub Post(ByRef navigator As Firefox.FirefoxDriver, ByVal id As String, ByVal username As String, ByVal password As String, ByVal message As String)
        navigator.Navigate.GoToUrl("https://www.facebook.com/logout.php")
        navigator.Navigate.GoToUrl("https://www.facebook.com")
        Threading.Thread.Sleep(5000)
        Dim sucess As Boolean
        sucess = False
        Try
            navigator.FindElementById("email").SendKeys(username)
            navigator.FindElementById("pass").SendKeys(password)
            navigator.FindElementById("loginbutton").Click()
            Threading.Thread.Sleep(5000)
            navigator.Navigate.GoToUrl("https://www.facebook.com")
            Threading.Thread.Sleep(10000)

            'Creat a Post button Click
            navigator.FindElementByXPath("//span[@class='_sg1']").Click()
            Threading.Thread.Sleep(5000)
            sucess = True
            'Input message
            navigator.FindElementByXPath("//div[@class='_1mf _1mj']").SendKeys(message)
            Threading.Thread.Sleep(5000)
            'Public
            navigator.FindElementByXPath("//div[@class='_5dd8']").Click()
            Threading.Thread.Sleep(5000)
            navigator.FindElementByXPath("//a[@class='_54nc _54nu _48t_']").Click()
            Threading.Thread.Sleep(5000)
            'Post button Click
            navigator.FindElementByXPath("//button[@class='_1mf7 _4jy0 _4jy3 _4jy1 _51sy selected _42ft']").Click()
            Threading.Thread.Sleep(5000)
            'DeleteDataBase(id, message, "me")
        Catch ex As Exception
            Form4.errors(UBound(Form4.errors)) = "Error At : Facebook :" & username & " Password : " & password
            ReDim Preserve Form4.errors(UBound(Form4.errors) + 1)
        End Try

        navigator.Quit()

        If sucess = False Then
            sendMail(id, username, "wall")
            SaveDataBase(id, "wall", "me", message)
        End If

        My.Settings.LastId = My.Settings.LastId + 1
        My.Settings.Save()
    End Sub

    Sub PostOw(ByRef navigator As Firefox.FirefoxDriver, ByVal id As String, ByVal username As String, ByVal password As String, ByVal friends As String, ByVal message As String)
        navigator.Navigate.GoToUrl("https://www.facebook.com/logout.php")
        navigator.Navigate.GoToUrl("https://www.facebook.com")
        Threading.Thread.Sleep(2000)
        navigator.FindElementById("email").SendKeys(username)
        navigator.FindElementById("pass").SendKeys(password)
        navigator.FindElementById("loginbutton").Click()
        Threading.Thread.Sleep(1000)
        Dim sucess As Boolean
        sucess = False
        Try
            navigator.Navigate.GoToUrl("https://web.facebook.com/search/people/?q=" & friends.Trim)
            Threading.Thread.Sleep(4000)
            sucess = True
        Catch ex As Exception
            Form3.Show()
            ReDim Preserve Form4.errors(UBound(Form4.errors) + 1)
            Form4.errors(UBound(Form4.errors)) = "Error At : Facebook : " & username & " Password : " & password
            GoTo 23
        End Try

        Try
            navigator.Navigate.GoToUrl(navigator.FindElementByXPath("//div[@class='_51xa']").FindElement(By.XPath("..")).FindElement(By.XPath("//div[@class='_gll']//a")).GetAttribute("href"))
            sucess = True
        Catch ex As Exception
            GoTo 23
        End Try
        Threading.Thread.Sleep(1000)
        Try

            navigator.FindElementByTagName("textarea").SendKeys(message)
            Threading.Thread.Sleep(4000)
            navigator.FindElementByXPath("//button[@class='_1mf7 _4jy0 _4jy3 _4jy1 _51sy selected _42ft']").Click()
            Threading.Thread.Sleep(4000)
            sucess = True
            'DeleteDataBase(id, message, "")
        Catch ex As Exception

        End Try

23:
        navigator.Quit()
        If sucess = False Then
            sendMail(id, username, "wall")
            SaveDataBase(id, "wall", friends, message)
        End If

        My.Settings.LastId = My.Settings.LastId + 1
        My.Settings.Save()
    End Sub
    Sub msg(ByRef navigator As Firefox.FirefoxDriver, ByVal id As String, ByVal username As String, ByVal password As String, ByVal freind As String, ByVal message As String)
        navigator.Navigate.GoToUrl("https://www.facebook.com/logout.php")
        navigator.Navigate.GoToUrl("https://www.facebook.com")
        Threading.Thread.Sleep(2000)
        navigator.FindElementById("email").SendKeys(username)
        navigator.FindElementById("pass").SendKeys(password)
        navigator.FindElementById("loginbutton").Click()
        Threading.Thread.Sleep(4000)
        Dim sucess As Boolean
        sucess = False
        Dim i As Integer
        i = 0
        navigator.Navigate.GoToUrl("https://www.facebook.com/search/top/?q=" + freind.Trim)
        Threading.Thread.Sleep(2000)
        Try
            'navigator.FindElementByXPath("//input[@class='_1frb']").SendKeys(freind.Trim)
            'Threading.Thread.Sleep(500)
            'navigator.FindElementByXPath("//input[@class='_1frb']").SendKeys(Keys.Enter)
            'Threading.Thread.Sleep(2000)
            navigator.FindElementByXPath("//img[@class='_2yeu img']").Click()
            Threading.Thread.Sleep(2000)
        Catch ex As Exception

        End Try


        Try
            navigator.FindElementByXPath("//a[@class='_42ft _4jy0 _4jy4 _517h _51sy']").Click()
            Threading.Thread.Sleep(2000)
            navigator.FindElementByXPath("//div[@class='_5rpb']").SendKeys(message)
            Threading.Thread.Sleep(2000)
            navigator.FindElementByXPath("//div[@class='_5rpb']").SendKeys(Keys.Enter)
            Threading.Thread.Sleep(2000)
            sucess = True
            'DeleteDataBase(id, message, freind)
        Catch ex As Exception

        End Try

        navigator.Quit()
        If sucess = False Then
            sendMail(id, username, "inbox")
            SaveDataBase(id, "inbox", freind, message)
        End If

        My.Settings.LastId = My.Settings.LastId + 1
        My.Settings.Save()
    End Sub

    Sub addFriend(ByRef navigator As Firefox.FirefoxDriver, ByVal id As String, ByVal username As String, ByVal password As String, ByVal freind As String, ByVal message As String)
        navigator.Navigate.GoToUrl("https://www.facebook.com/logout.php")
        navigator.Navigate.GoToUrl("https://www.facebook.com")
        Threading.Thread.Sleep(2000)
        navigator.FindElementById("email").SendKeys(username)
        navigator.FindElementById("pass").SendKeys(password)
        navigator.FindElementById("loginbutton").Click()
        Threading.Thread.Sleep(4000)
        Dim sucess As Boolean
        sucess = False
        Dim strarr() As String
        strarr = freind.Split(":")

        Try
            navigator.FindElementById("findFriendsNav").Click()
            Threading.Thread.Sleep(4000)
            sucess = True
        Catch ex As Exception

        End Try

        Try
            navigator.FindElementByXPath("//input[@class='inputtext friendBrowserSearchInput textInput']").SendKeys(strarr(0))
            Threading.Thread.Sleep(1000)
            'navigator.FindElementByXPath("//input[@class='inputtext friendBrowserSearchInput textInput']").SendKeys(Keys.Enter)
            'Threading.Thread.Sleep(4000)
            navigator.FindElementByXPath("(//input[@class='inputtext textInput'])[3]").SendKeys(strarr(1))
            Threading.Thread.Sleep(1000)
            navigator.FindElementByXPath("(//input[@class='inputtext textInput'])[3]").SendKeys(Keys.Enter)
            Threading.Thread.Sleep(4000)
        Catch ex As Exception

        End Try
        Try
            navigator.FindElementByXPath("//div[@class='FriendButton']").Click()
            Threading.Thread.Sleep(10000)
            sucess = True
            'DeleteDataBase(id, message, freind)
            navigator.FindElement(By.ClassName("layerConfirm")).Click()
            Threading.Thread.Sleep(5000)
        Catch ex As Exception

        End Try
        navigator.Quit()
        If sucess = False Then
            sendMail(id, username, "friend")
            SaveDataBase(id, "friend", freind, message)
        End If

        My.Settings.LastId = My.Settings.LastId + 1
        My.Settings.Save()
    End Sub

    Sub acceptFriend(ByRef navigator As Firefox.FirefoxDriver, ByVal id As String, ByVal username As String, ByVal password As String, ByVal freind As String, ByVal message As String)
        navigator.Navigate.GoToUrl("https://www.facebook.com/logout.php")
        navigator.Navigate.GoToUrl("https://www.facebook.com")
        Threading.Thread.Sleep(2000)
        navigator.FindElementById("email").SendKeys(username)
        navigator.FindElementById("pass").SendKeys(password)
        navigator.FindElementById("loginbutton").Click()
        Threading.Thread.Sleep(10000)
        Dim sucess As Boolean
        sucess = False

        Try
            navigator.FindElementByXPath("//div[@class='uiToggle _4962 _3nzl _24xk hasNew']").Click()
            Threading.Thread.Sleep(5000)
            sucess = True
        Catch ex As Exception

        End Try

        Try
            navigator.FindElementByXPath("//div[@class='uiToggle _4962 _3nzl _24xk']").Click()
            Threading.Thread.Sleep(5000)
            sucess = True
        Catch ex As Exception

        End Try

        Try

            For i = 1 To 20
                If navigator.FindElementByXPath("(//span[@class='title fsl fwb fcb']//a)[" + i.ToString + "]").Text.ToUpper.Contains(freind.ToUpper) Then
                    Dim str As String
                    str = navigator.FindElementByXPath("(//span[@class='title fsl fwb fcb']//a)[" + i.ToString + "]").GetAttribute("href")
                    navigator.Navigate.GoToUrl(str)
                    Threading.Thread.Sleep(5000)
                    navigator.FindElementByXPath("//a[@class='_42ft _4jy0 _4jy4 _4jy1 selected _51sy']").Click()
                    Threading.Thread.Sleep(1000)
                    GoTo 25
                End If
            Next
        Catch ex As Exception

        End Try
25:
        navigator.Quit()
        If sucess = False Then
            sendMail(id, username, "accept")
            SaveDataBase(id, "accept", freind, message)
        End If

        My.Settings.LastId = My.Settings.LastId + 1
        My.Settings.Save()
    End Sub

    Sub Photo(ByRef navigator As Firefox.FirefoxDriver, ByVal id As String, ByVal username As String, ByVal password As String)
        navigator.Navigate.GoToUrl("https://www.facebook.com/logout.php")
        navigator.Navigate.GoToUrl("https://www.facebook.com")
        Threading.Thread.Sleep(2000)
        Dim sucess As Boolean
        sucess = False
        Try
            navigator.FindElementById("email").SendKeys(username)
            navigator.FindElementById("pass").SendKeys(password)
            navigator.FindElementById("loginbutton").Click()
            Threading.Thread.Sleep(10000)
        Catch ex As Exception

        End Try
        Dim count As Integer

        Try
            'get new message count
            count = Val(navigator.FindElementByXPath("//span[@class='_51lp _3z_5 _5ugh']").Text) - 1
            'new message button click  uiToggle _4962 _1z4y _330i hasNew
            navigator.FindElementByXPath("//div[@class='uiToggle _4962 _1z4y _330i _4kgv hasNew']").Click()
            Threading.Thread.Sleep(4000)
            sucess = True
            navigator.FindElementById("MercuryJewelFooter").Click()
            Threading.Thread.Sleep(10000)
        Catch ex As Exception

        End Try

        Try
            'get new message count
            count = Val(navigator.FindElementByXPath("//span[@class='_51lp _3z_5 _5ugh']").Text) - 1
            'new message button click  uiToggle _4962 _1z4y _330i hasNew
            navigator.FindElementByXPath("//div[@class='uiToggle _4962 _1z4y _330i hasNew']").Click()
            Threading.Thread.Sleep(4000)
            sucess = True
            navigator.FindElementById("MercuryJewelFooter").Click()
            Threading.Thread.Sleep(10000)
        Catch ex As Exception

        End Try

        Dim info As String
        Dim message As String
        Dim url, url1, url2, url3, url4, url5, url6, url7, url8 As String
        Try
            Dim i As Integer

            For i = 0 To count
                info = navigator.FindElementByXPath("//li[@class='_5l-3 _1ht1 _1ht3']//div[@class='_5l-3 _1ht5']//a[@class='_1ht5 _2il3 _5l-3']//div[@class='_1qt4 _5l-m']//div[@class='_1qt5 _5l-3']//span[@class='_1ht6']").Text
                message = navigator.FindElementByXPath("//li[@class='_5l-3 _1ht1 _1ht3']//div[@class='_5l-3 _1ht5']//a[@class='_1ht5 _2il3 _5l-3']//div[@class='_1qt4 _5l-m']//div[@class='_1qt5 _5l-3']//span[@class='_1htf']").Text
                navigator.FindElementByXPath("//li[@class='_5l-3 _1ht1 _1ht3']").Click()
                Threading.Thread.Sleep(4000)
                Dim pos As Integer
                pos = InStr(message, "sent a photo.")


                If Not pos = 0 Then
                    '1 
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[1]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '2
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[2]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '3
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[3]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '4
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[4]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '5
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[5]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '6
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[6]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '7
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[7]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '8
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[8]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '9
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[9]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    '10
                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[10]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try

                    Try
                        message = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]").Text
                        url1 = ""
                        url2 = ""
                        url3 = ""
                        url4 = ""
                        url5 = ""
                        url6 = ""
                        url7 = ""
                        url8 = ""
                        url1 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[1]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url2 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[2]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url3 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[3]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url4 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[4]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url5 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[5]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url6 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[6]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url7 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[7]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    Try
                        url8 = navigator.FindElementByXPath(" (//div[@class='_41ud'])[11]//div[8]//div//div//div//div//img").GetAttribute("src")
                    Catch ex As Exception
                    End Try
                    If Not url1 = "" Then
                        url = url1
                    End If
                    If Not url2 = "" Then
                        url = url + "%%" + url2
                    End If
                    If Not url3 = "" Then
                        url = url + "%%" + url3
                    End If
                    If Not url4 = "" Then
                        url = url + "%%" + url4
                    End If
                    If Not url5 = "" Then
                        url = url + "%%" + url5
                    End If
                    If Not url6 = "" Then
                        url = url + "%%" + url6
                    End If
                    If Not url7 = "" Then
                        url = url + "%%" + url7
                    End If
                    If Not url8 = "" Then
                        url = url + "%%" + url8
                    End If
                End If

                If message = "" Then
                    message = "Friends name  sent image  instead of typed out message.   Unfortunately corrlinks does not allow you to view images."
                End If
                'save url
                imageSave(id, info, url, message)
                Threading.Thread.Sleep(2000)
                'send email
                sendPhoto(id, username, info, url, message)
                Threading.Thread.Sleep(2000)

            Next
        Catch ex As Exception

        End Try

        navigator.Quit()

        If sucess = False Then
            sendMail(id, username, "photo")
            'SaveDataBase(id, "wall", "me", Message)
        End If
    End Sub


    Sub sendMail(ByVal id As String, ByVal username As String, ByVal type As String)

        send = True
        For index = 0 To inmateID.Length - 1
            If inmateID(index) = id Then
                send = False
                GoTo 6
            End If
        Next
6:
        If send = True Then
            Array.Resize(inmateID, inmateID.Length + 1)
            inmateID(inmateID.Length - 1) = id
            Try
                Dim Smtp_Server As New SmtpClient
                Dim e_mail As New MailMessage()
                Smtp_Server.UseDefaultCredentials = True
                Smtp_Server.Credentials = New Net.NetworkCredential("Pushtrutalk@gmail.com", "titan111")
                Smtp_Server.Port = 587
                Smtp_Server.EnableSsl = True
                Smtp_Server.Host = "smtp.gmail.com"

                e_mail = New MailMessage()
                e_mail.From = New MailAddress("Pushtrutalk@gmail.com")
                e_mail.To.Add("darnellbarber2@gmail.com")
                'e_mail.To.Add("choesad0808@gmail.com")
                Dim customer As String
                customer = id + "@trutalkconnect.com"
                e_mail.To.Add(customer)
                e_mail.Subject = "Message from Trutalknation.com"
                e_mail.IsBodyHtml = False

                Dim message As String
                message = My.Computer.FileSystem.ReadAllText("message.txt")
                e_mail.Body = message + "(ID  " + id + ") & " + username
                Smtp_Server.Send(e_mail)
            Catch error_t As Exception

            End Try
        End If

    End Sub

    Sub sendPhoto(ByVal id As String, ByVal username As String, ByVal info As String, ByVal url As String, ByVal msg As String)
        Try
            Dim photoUrl() As String
            photoUrl = Split(url, "%%")
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = True
            Smtp_Server.Credentials = New Net.NetworkCredential("Pushtrutalk@gmail.com", "titan111")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("Pushtrutalk@gmail.com")
            e_mail.To.Add("darnellbarber2@gmail.com")
            e_mail.To.Add("choesad0808@gmail.com")
            Dim customer As String
            customer = id + "@trutalkconnect.com"
            e_mail.To.Add(customer)
            e_mail.Subject = "Message from Trutalknation.com"
            e_mail.IsBodyHtml = True

            Dim img As String
            img = ""
            For i = 0 To photoUrl.Length - 1
                img = img + "<img src='" + photoUrl(i) + "'>"
            Next

            e_mail.Body = "<html><body>" + img + "<p>" + info + ":" + msg + "</p></body></html>"
            Smtp_Server.Send(e_mail)
        Catch error_t As Exception
        End Try
    End Sub

    Sub imageSave(ByVal id As String, ByVal from As String, ByVal url As String, ByVal msg As String)
        If url = "" Then
            url = "Nothing"
        End If
        Dim cn As New MySqlConnection
        Dim cmd As MySqlCommand
        'Debug: localhost database
        'cn.ConnectionString = "server=localhost;Port=3306;user id=root;password=;database=mailmypi_messages;Character Set=utf8;"
        cn.ConnectionString = "server=mailmypix.com;Port=3306;user id=mailmypi_mail;password=12345;database=mailmypi_messages;Character Set=utf8;"
        Try
            cn.Open()
            Dim query As String
            query = "INSERT INTO fbextract ( `inmateid`, `from`, `url`, `msg`) VALUES ('" + id + "','" + from + "','" + url + "','" + msg + "' )"
            cmd = New MySqlCommand(query, cn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        Try
            cn.Close()
            cn.Dispose()
        Catch ex As Exception
        End Try

    End Sub


    Sub SaveDataBase(ByVal id As String, ByVal type As String, ByVal username As String, ByVal message As String)


        Dim cn As New MySqlConnection
            Dim cmd As MySqlCommand

            'Debug: localhost database
            'cn.ConnectionString = "server=localhost;Port=3306;user id=root;password=;database=mailmypi_messages;Character Set=utf8;"
            cn.ConnectionString = "server=mailmypix.com;Port=3306;user id=mailmypi_mail;password=12345;database=mailmypi_messages;Character Set=utf8;"
            Try
                cn.Open()
                Dim query As String
                query = "INSERT INTO fbcatchall ( inmateid, post_type, to_, message) VALUES ('" + id + "','" + type + "','" + username + "','" + message + "')"
                cmd = New MySqlCommand(query, cn)
                cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try
            cn.Close()
            cn.Dispose()


    End Sub

    Sub DeleteDataBase(ByVal id As String, ByVal message As String, ByVal to_ As String)
        Dim cn As New MySqlConnection
        Dim cmd As MySqlCommand

        'Debug: localhost database
        'cn.ConnectionString = "server=localhost;Port=3306;user id=root;password=;database=mailmypi_messages;Character Set=utf8;"
        cn.ConnectionString = "server=mailmypix.com;Port=3306;user id=mailmypi_mail;password=12345;database=mailmypi_messages;Character Set=utf8;"
        Try
            cn.Open()
            Dim query As String
            query = "DELETE FROM fbcatchall WHERE inmateid =" + id + " AND message = '" + message + "' AND to_ = '" + to_ + "'"
            cmd = New MySqlCommand(query, cn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        cn.Close()
        cn.Dispose()
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        Me.Close()
    End Sub

    Private Sub MetroButton6_Click(sender As Object, e As EventArgs) Handles MetroButton6.Click
        Form4.Show()
    End Sub


    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Auto()

    End Sub

    Private Sub Resend_Click(sender As Object, e As EventArgs) Handles Resend.Click
        'Process.Start("resend\resend.exe")
        Dim cn As New MySqlConnection
        Dim cmd As MySqlCommand

        'Debug: localhost database
        'cn.ConnectionString = "server=localhost;Port=3306;user id=root;password=;database=mailmypi_messages;Character Set=utf8;"
        cn.ConnectionString = "server=mailmypix.com;Port=3306;user id=mailmypi_mail;password=12345;database=mailmypi_messages;Character Set=utf8;"
        Dim reader As MySqlDataReader
        Try
            cn.Open()
            Dim query As String
            query = "SELECT * FROM fbcatchall"
            cmd = New MySqlCommand(query, cn)
            reader = cmd.ExecuteReader
            Dim k As Integer
            k = 0
            While reader.Read()
                'Les champs à recup. Les tables commencent à 0.
                MetroGrid1.Rows.Add()
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(0).Value = ((reader.GetString(1)))
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(1).Value = ((reader.GetString(2)))
                MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(2).Value = ((reader.GetString(3)))


                ReDim Preserve messages(k)
                messages(k) = ((reader.GetString(4)))
                k = k + 1
            End While
            cn.Close()
        Catch ex As Exception

        End Try
        cn.Dispose()
        OpenFileDialog1.ShowDialog()
        Dim s As String
        s = OpenFileDialog1.FileName
        Try
            s = s.Substring(s.LastIndexOf("\") + 1)
        Catch ex As Exception
            MsgBox("Please try again ...")
            GoTo 2
        End Try

        Select Case s.ToLower
            Case "fb.db"
                MetroGrid2.Rows.Clear()
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
                For Each line As String In RichTextBox1.Lines
                    MetroGrid2.Rows.Add()
                    Dim info() As String
                    Try
                        info = line.Split(":")
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(0).Value = info(0)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(1).Value = info(1)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(2).Value = info(2)
                    Catch ex As Exception
                        MetroGrid2.Rows.RemoveAt(MetroGrid2.Rows.Count - 2)
                    End Try
                Next
                My.Settings.fb = OpenFileDialog1.FileName
                My.Settings.Save()
                RichTextBox1.Text = ""
        End Select
        Dim i As Long
        i = 0
        For i = 0 To MetroGrid1.Rows.Count - 2
            Dim j As Long
            j = 0
            For j = 0 To MetroGrid2.Rows.Count - 2
                If MetroGrid2.Rows(j).Cells(0).Value.trim = MetroGrid1.Rows(i).Cells(0).Value.trim Then
                    Dim driverService = Firefox.FirefoxDriverService.CreateDefaultService
                    driverService.HideCommandPromptWindow = True
                    Dim navigator As New Firefox.FirefoxDriver(driverService)
                    DeleteDataBase(MetroGrid2.Rows(j).Cells(0).Value, messages(i), MetroGrid1.Rows(i).Cells(2).Value)
                    Select Case MetroGrid1.Rows(i).Cells(1).Value.ToString.ToLower.Trim
                        Case "inbox"
                            'Dim navigator As New Firefox.FirefoxDriver
                            msg(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                        Case "wall"
                            If MetroGrid1.Rows(i).Cells(2).Value.ToString.ToLower.Trim = "me" Then
                                'Dim navigator As New Firefox.FirefoxDriver
                                Post(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, messages(i))
                            Else
                                'Dim navigator As New Firefox.FirefoxDriver
                                PostOw(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                            End If
                        Case "friend"
                            'Dim navigator As New Firefox.FirefoxDriver
                            addFriend(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                        Case "accept"
                            'Dim navigator As New Firefox.FirefoxDriver
                            acceptFriend(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value, MetroGrid1.Rows(i).Cells(2).Value, messages(i))
                        Case "photo"
                            'Dim navigator As New Firefox.FirefoxDriver
                            Photo(navigator, MetroGrid2.Rows(j).Cells(0).Value, MetroGrid2.Rows(j).Cells(1).Value, MetroGrid2.Rows(j).Cells(2).Value)

                    End Select
                    Exit For
                End If
            Next
            TextBox1.Text = My.Settings.LastId.ToString
        Next

        MetroButton2.Enabled = False
2:

    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        OpenFileDialog1.ShowDialog()
        Dim s As String
        s = OpenFileDialog1.FileName
        Try
            s = s.Substring(s.LastIndexOf("\") + 1)
        Catch ex As Exception
            MsgBox("Please try again ...")
            GoTo 2
        End Try

        Select Case s.ToLower
            Case "image.db"
                MetroGrid1.Rows.Clear()
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
                For Each line As String In RichTextBox1.Lines
                    MetroGrid1.Rows.Add()
                    Dim info() As String
                    Try
                        info = line.Split(":")
                        MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(0).Value = info(0)
                        MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(1).Value = info(1)
                        MetroGrid1.Rows(MetroGrid1.Rows.Count - 2).Cells(2).Value = info(2)
                    Catch ex As Exception
                        MetroGrid1.Rows.RemoveAt(MetroGrid1.Rows.Count - 2)
                    End Try
                Next
                'My.Settings.fb = OpenFileDialog1.FileName
                'My.Settings.Save()
                RichTextBox1.Text = ""
        End Select

        OpenFileDialog1.ShowDialog()
        s = OpenFileDialog1.FileName
        Try
            s = s.Substring(s.LastIndexOf("\") + 1)
        Catch ex As Exception
            MsgBox("Please try again ...")
            GoTo 2
        End Try

        Select Case s.ToLower
            Case "fb.db"
                MetroGrid2.Rows.Clear()
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
                For Each line As String In RichTextBox1.Lines
                    MetroGrid2.Rows.Add()
                    Dim info() As String
                    Try
                        info = line.Split(":")
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(0).Value = info(0)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(1).Value = info(1)
                        MetroGrid2.Rows(MetroGrid2.Rows.Count - 2).Cells(2).Value = info(2)
                    Catch ex As Exception
                        MetroGrid2.Rows.RemoveAt(MetroGrid2.Rows.Count - 2)
                    End Try
                Next
                My.Settings.fb = OpenFileDialog1.FileName
                My.Settings.Save()
                RichTextBox1.Text = ""
        End Select
2:
        MetroButton2.Enabled = True
    End Sub

End Class

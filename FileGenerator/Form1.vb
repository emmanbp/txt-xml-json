Imports System.IO
Imports System.Text
Imports System.Xml
Imports Newtonsoft.Json

Public Class Form1

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Address_TextChanged(sender As Object, e As EventArgs) Handles Address.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        createText()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim timeStamp As DateTime = DateTime.Now
        Dim current As String = timeStamp.ToString("MM-yyyy-HH-mm-ss")
        Dim username As String = TextBox1.Text
        Dim useraddress As String = Address.Text

        Dim xmlpath As String = "c:\Generated Files\" + current + ".xml"

        Dim writer As New XmlTextWriter(xmlpath, System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = System.Xml.Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("UserInfo")
        createElement(username, useraddress, writer)
        writer.WriteEndElement()
        writer.WriteEndDocument()
        writer.Close()

        TextBox1.Text = ""
        Address.Text = ""

        MessageBox.Show("Check c:\Generated Files\", ".XML FILE CREATED", MessageBoxButtons.OKCancel)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim timeStamp As DateTime = DateTime.Now
        Dim current As String = timeStamp.ToString("MM-yyyy-HH-mm-ss")
        Dim username As String = TextBox1.Text
        Dim useraddress As String = Address.Text

        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Using writer As JsonWriter = New JsonTextWriter(sw)
            writer.Formatting = Newtonsoft.Json.Formatting.Indented
            writer.WriteStartObject()
            writer.WritePropertyName("Name")
            writer.WriteValue(username)
            writer.WritePropertyName("Address")
            writer.WriteValue(useraddress)
            writer.WriteEndObject()
        End Using

        Dim path As String = "c:\Generated Files\" + current + ".json"

        Dim fs As FileStream = File.Create(path)

        Dim info As Byte() = New UTF8Encoding(True).GetBytes(sw.ToString())
        fs.Write(info, 0, info.Length)
        fs.Close()

        TextBox1.Text = ""
        Address.Text = ""

        MessageBox.Show("Check c:\Generated Files\", ".JSON FILE CREATED", MessageBoxButtons.OKCancel)
    End Sub

    Private Sub createText()
        Dim timeStamp As DateTime = DateTime.Now
        Dim current As String = timeStamp.ToString("MM-yyyy-HH-mm-ss")
        Dim username As String = TextBox1.Text
        Dim useraddress As String = Address.Text

        Dim path As String = "c:\Generated Files\" + current + ".txt"

        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path)

        ' Add text to the file.
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("Name: " + username + Environment.NewLine + "Address: " + useraddress + "")
        fs.Write(info, 0, info.Length)
        fs.Close()

        TextBox1.Text = ""
        Address.Text = ""

        MessageBox.Show("Check c:\Generated Files\", ".TXT FILE CREATED", MessageBoxButtons.OKCancel)

    End Sub
    Private Sub createElement(ByVal username As String, ByVal address As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement("Name")
        writer.WriteString(username)
        writer.WriteEndElement()
        writer.WriteStartElement("Address")
        writer.WriteString(address)
        writer.WriteEndElement()
    End Sub
End Class
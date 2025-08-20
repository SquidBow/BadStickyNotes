namespace BadStickyNotes;

public partial class Form1 : Form
{

    private List<string> notes = new List<string>();
    private string notesFilePath = "notes.txt";
    public Form1()
    {
        InitializeComponent();
        LoadNotes();
    }

    private void LoadNotes()
    {
        if (!File.Exists(notesFilePath))
        {
            return;
        }
        //Add all notes to the list
        foreach (var note in File.ReadAllLines(notesFilePath))
        {
            notes.Add(note);
        }
        
        RenderNotes();
    }

    private void SaveNotes()
    {
        File.WriteAllLines(notesFilePath, notes);
    }


    //Change colors for fun
    private void NewNoteButton_MouseEnter(object sender, EventArgs e)
    {
        NewNoteButton.BackColor = Color.Aqua;
    }
    
    private void NewNoteButton_MouseLeave(object sender, EventArgs e)
    {
        NewNoteButton.BackColor = Color.White;
    }

    private void NewNoteButton_Click(object sender, EventArgs e)
    {
        //Create new TextBox with required parameters
        TextBox newNote = new TextBox();

        newNote.Multiline = true;
        newNote.Location = new Point(NewNoteButton.Location.X, NewNoteButton.Location.Y + NewNoteButton.Height + 10);
        newNote.Size = new Size(322, 30);

        //Connect to function that will add the note and remove the TextBox
        newNote.Leave += NewNote_Leave;
        
        //Add it
        this.Controls.Add(newNote);
    }

    private void NewNote_Leave(object sender, EventArgs e)
    {
        //Get the TextBox
        TextBox textBox = (TextBox)sender;
        //Extract the text
        string newNote = textBox.Text;
        
        //Add the text to all notes
        notes.Add(newNote);
        
        //Save the notes
        SaveNotes();
        
        //Render all the notes
        RenderNotes();
        
        //Delete the TextBox
        this.Controls.Remove(textBox);
    }

    private void Form1_Click(object sender, EventArgs e)
    {
        this.ActiveControl = null;
    }

    private void RenderNotes()
    {
        //How far down to create each node
        int offset = 85;

        for (int i = notes.Count - 1; i > -1; i--)
        {
            string noteText = notes[i];
            
            //Add and modify TextBoxes for notes
            TextBox note = new TextBox();
            note.Multiline = true;
            note.Text = noteText;
            note.WordWrap = true;

            //note.Size = new Size(322,100);
            note.Location = new Point(12, offset);

            int noteWidth = 322;
            
            Size requiredSize = TextRenderer.MeasureText(noteText, note.Font, new Size(322, 0), TextFormatFlags.WordBreak);

            if (requiredSize.Height % 28 != 0)
            {
                //Round to the nearest 28 units
                requiredSize.Height = (requiredSize.Height / 28) * 28 + 28;
            }

            offset += requiredSize.Height + 5;

            note.Size = new Size(322, requiredSize.Height);

            this.Controls.Add(note);
        }
        
    }
}
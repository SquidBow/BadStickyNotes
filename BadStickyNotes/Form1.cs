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
        RenderNotes(118);
        
        //Create new TextBox with required parameters
        TextBox newNote = new TextBox();

        newNote.Multiline = true;
        newNote.Location = new Point(NewNoteButton.Location.X, NewNoteButton.Location.Y + NewNoteButton.Height + 10);
        newNote.Size = new Size(322, 28);
        newNote.Tag = "note";

        newNote.TextChanged += updateHeightOnTextChanged;

        //Connect to function that will add the note and remove the TextBox
        newNote.Leave += NewNote_Leave;
        
        //Add it
        this.Controls.Add(newNote);
        newNote.Focus();
    }

    private void NewNote_Leave(object sender, EventArgs e)
    {
        //Get the TextBox
        TextBox textBox = (TextBox)sender;
        //Extract the text
        string newNote = textBox.Text;
        
        //Add the text to all notes
        
        //Save the notes
        if (newNote != "")
        {
            notes.Add(newNote);
            SaveNotes();
        }
        
        //Render all the notes
        RenderNotes();
        
        //Delete the TextBox
        this.Controls.Remove(textBox);
    }

    private void Form1_Click(object sender, EventArgs e)
    {
        this.ActiveControl = null;
    }

    private void RenderNotes(int offset = 85)
    {
        var removeNotes = this.Controls.OfType<TextBox>()
            .Where(textBox => textBox.Tag as string == "note")
            .ToList();

        foreach (var note in removeNotes)
        {
            this.Controls.Remove(note);
        }

        for (int i = notes.Count - 1; i > -1; i--)
        {
            string noteText = notes[i];
            
            //Add and modify TextBoxes for notes
            TextBox note = new TextBox();
            note.Multiline = true;
            note.Text = noteText;
            note.WordWrap = true;
            note.Tag = "note";

            //note.Size = new Size(322,100);
            note.Location = new Point(12, offset);

            int noteWidth = 322;
            
            int requiredHeight = TextRenderer.MeasureText(noteText, note.Font, new Size(noteWidth, 0), TextFormatFlags.WordBreak).Height + 8;

            /*if (requiredSize.Height % 28 != 0)
            {
                //Round to the nearest 28 units
                requiredSize.Height = (requiredSize.Height / 28) * 28 + 28;
            }*/

            if (requiredHeight < 28)
            {
                requiredHeight = 28;
            }
            
            offset += requiredHeight + 3;

            note.Size = new Size(noteWidth, requiredHeight);

            this.Controls.Add(note);
        }
    }

    private void updateHeightOnTextChanged(object sender, EventArgs e)
    {
        var note = (TextBox)sender;
        var noteText = note.Text;
        TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        
        int requiredHeight = TextRenderer.MeasureText(noteText, note.Font, new Size(322, 0), flags)
            .Height + 8;

        if (requiredHeight < 28)
        {
            requiredHeight = 28;
        }

        if (note.Height != requiredHeight)
        {
            note.Height = requiredHeight;
            updateHeight();
        }
    }

    private void updateHeight()
    {
        var updateNotes = this.Controls.OfType<TextBox>()
            .Where(note => note.Tag as string == "note")
            .OrderBy(note => note.Top)
            .ToList();

        foreach (var note in updateNotes.Skip(1))
        {
            note.Location = new Point(note.Location.X, note.Location.Y + 28);
        }
    }
}
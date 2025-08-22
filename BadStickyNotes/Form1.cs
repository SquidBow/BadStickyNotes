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
            .Where(textBox => textBox.Tag is int)
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
            note.Tag = i;
            note.TextChanged += updateHeightOnTextChanged;
            note.Leave += updateNoteInTheList;

            //note.Size = new Size(322,100);
            note.Location = new Point(12, offset);

            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        
            int requiredHeight = TextRenderer.MeasureText(noteText, note.Font, new Size(318, 0), flags)
                .Height + 8;

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

            note.Size = new Size(322, requiredHeight);

            this.Controls.Add(note);
        }
    }

    private void updateHeightOnTextChanged(object sender, EventArgs e)
    {
        var note = (TextBox)sender;
        var noteText = note.Text;
        TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        
        int requiredHeight = TextRenderer.MeasureText(noteText, note.Font, new Size(318, 0), flags)
            .Height + 8;

        if (requiredHeight < 28)
        {
            requiredHeight = 28;
        }

        if (note.Height != requiredHeight)
        {
            note.Height = requiredHeight;
            
            UpdateNotePositions();
        }
    }

    private void UpdateNotePositions()
    {
        // 1. Get all the note TextBoxes that are currently on the form
        var notesOnScreen = this.Controls.OfType<TextBox>()
                                       .Where(tb => tb.Tag is int || (tb.Tag == null && tb.Focused)) // Find all saved notes plus the new note if it's focused
                                       .OrderBy(tb => tb.Top)
                                       .ToList();

        // 2. Set the starting Y position
        int currentY = 85; 

        // 3. Loop through the notes and set their Top position one after another
        foreach (var note in notesOnScreen)
        {
            note.Top = currentY;
            currentY += note.Height + 3; // Update the Y for the next note
        }
    }

    private void updateNoteInTheList(object sender, EventArgs e)
    {
        TextBox editNote = (TextBox)sender;

        int noteId = (int)editNote.Tag;

        notes[noteId] = editNote.Text;
        
        SaveNotes();
        RenderNotes();
    }
}